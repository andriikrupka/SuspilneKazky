using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace KazkySuspilne.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseArtists();
            Console.ReadKey();
        }

        private class ArtistInfo
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string Url { get; set; }
        }

        private static async Task ParseArtists()
        {
            using var htmlClient = new HttpClient();
            var content = await htmlClient.GetStringAsync("https://kazky.suspilne.media/list.php").ConfigureAwait(false);

            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);

            var blocks = document.QuerySelectorAll("div.reader-line").OfType<IHtmlDivElement>().ToList();

            var list = new List<ArtistInfo>();

            foreach (var block in blocks)
            {
                var id = block.QuerySelector("div.reader-img").Id;
                var imgElement = (block.QuerySelector("img.circle") as IHtmlImageElement);
                var baseUri = imgElement.BaseUri;
                var source = imgElement.Source.Replace("about://", "https://kazky.suspilne.media");
                var artist = block.QuerySelector("div.reader").TextContent.Trim();

                list.Add(new ArtistInfo
                {
                    Id = int.Parse(id),
                    Text = artist,
                    Url = source
                });

                var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(list);

                string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "artists.json");
                File.WriteAllText(destPath, serialized);
            }
        }
    }
}
