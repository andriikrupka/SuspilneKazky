using System;
namespace KazkySuspilne.Models
{
    public class ArtistInfo
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Image { get; set; }

        public string FullImageUrl => $"{Constatns.BaseUrl}/{Image}";
    }
}
