using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using KazkySuspilne.Models;
using Newtonsoft.Json;

namespace KazkySuspilne.Services
{
    public interface ISuspilneService
    {
        Task<List<StorySong>> GetStories();
        Task<List<ArtistInfo>> GetArtists();
    }


    public class SuspilneService : ISuspilneService
    {
        private HttpClient _httpClient;

        public SuspilneService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<StorySong>> GetStories()
        {
            var url = $"{Constatns.BaseUrl}/index.json";
            var content = await _httpClient.GetStringAsync(url);
            var dict = JsonConvert.DeserializeObject<Dictionary<int, StorySong>>(content);
            return dict?.Values?.ToList() ?? new List<StorySong>();
        }

        public async Task<List<ArtistInfo>> GetArtists()
        {
            var data = await ReadFile<List<ArtistInfo>>("artists.json").ConfigureAwait(false);
            return data;
        }

        private async Task<T> ReadFile<T>(string fileName)
        {
            T obj;
            var assembly = this.GetType().GetTypeInfo().Assembly;
             var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Data.{fileName}");
             var reader = new StreamReader(stream);
            var jsonString = await reader.ReadToEndAsync().ConfigureAwait(false);
            obj = JsonConvert.DeserializeObject<T>(jsonString);

            return obj;
        }
    }
}
