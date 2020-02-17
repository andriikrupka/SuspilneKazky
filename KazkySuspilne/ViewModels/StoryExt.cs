using KazkySuspilne.Models;
using MediaManager.Library;

namespace KazkySuspilne.ViewModels
{
    public static class StoryExt
    {
        public static MediaItem AsMediaItem(this StorySong storySong)
        {
            var mediaItem = new MediaItem();
            mediaItem.MediaUri = storySong.FullSongUrl;
            mediaItem.Title = storySong.Name;
            mediaItem.Artist = storySong.Auth;
            mediaItem.ImageUri = storySong.FullImageUrl;
            return mediaItem;
        }
    }
}
