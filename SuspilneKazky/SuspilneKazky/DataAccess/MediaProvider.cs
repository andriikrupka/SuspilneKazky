using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using SuspilneKazky.Models;

namespace SuspilneKazky.DataAccess
{
    public class MediaProvider : IMediaProvider
    {
        private HttpClient _httpClient;

        public MediaProvider()
        {
            _httpClient = new HttpClient();
        }

        public string OnlineUrl
        {
            get => "http://nrcu.gov.ua:8000/kazka-mp3";
        }

        const string BASE_URL = "https://kazky.suspilne.media";
        readonly Uri BASE_URI = new Uri(BASE_URL);

        public async Task<List<StorySong>> LoadStoriesAsync()
        {
            const string LIST_STORIES_URL = "https://kazky.suspilne.media/list";
            var list = new List<StorySong>();
            try
            {
                var content = await _httpClient.GetStringAsync(LIST_STORIES_URL);
                var parser = new HtmlParser();
                var document = parser.Parse(content);

                var blocks = document.QuerySelectorAll("a.tales-list__tale").OfType<IHtmlAnchorElement>().ToList();
                foreach (var block in blocks)
                {
                    var storySongItem = new StorySong();
                    storySongItem.SongUri = new Uri(BASE_URI, block.PathName);

                    var caption = block.QuerySelector("div.tales-list__tale__tale-caption");
                    storySongItem.Name = caption?.TextContent?.Trim() ?? string.Empty;

                    var author = block.QuerySelector("div.tales-list__tale__tale-time");
                    storySongItem.Author = author?.TextContent?.Trim() ?? string.Empty;

                    var image = block.QuerySelector("img") as IHtmlImageElement;
                    var imageUrl = image?.Source;
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        storySongItem.ImageUri = new Uri(imageUrl);
                        if (imageUrl.StartsWith("", StringComparison.OrdinalIgnoreCase))
                        {
                            var relative = imageUrl.Substring("about:///".Length);
                            storySongItem.ImageUri = new Uri(BASE_URI, relative);
                        }
                    }

                    list.Add(storySongItem);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return list;
        }
    }
}
