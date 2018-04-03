using System;

namespace SuspilneKazky.Models
{
    public class StorySong
    {
        public Uri ImageUri { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public Uri SongUri { get; set; }
        public Uri DetailsUri { get; set; }
    }
}
