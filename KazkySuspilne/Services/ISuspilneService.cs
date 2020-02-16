using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using KazkySuspilne.Models;
using Newtonsoft.Json;

namespace KazkySuspilne.Services
{
    public interface ISuspilneService
    {
        Task<List<StorySong>> GetStories();
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
    }
}
