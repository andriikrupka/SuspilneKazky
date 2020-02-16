using Newtonsoft.Json;

namespace KazkySuspilne.Models
{
    public class StorySong
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("auth")]
        public string Auth { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("song")]
        public string Song { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        public string FullImageUrl => $"{Constatns.BaseUrl}/{Image}";

        public string FullSongUrl => $"{Constatns.BaseUrl}/{Song}";
    }
}
