using System;
using AVFoundation;
using Foundation;
using KazkySuspilne.Models;
using KazkySuspilne.Services;
using MediaPlayer;
using UIKit;

namespace KazkySuspilne.iOS.Services
{
    public class AudioService : IAudioService
    {
        private AVPlayer _avPlayer;
        private MPMediaItemArtwork artwork;
        private bool _isPrepared;

        public AudioService()
        {
            _avPlayer = new AVPlayer();
        }

        public void Play(StorySong story)
        {
            Stop();
            var avasset = AVAsset.FromUrl(NSUrl.FromString(story.FullSongUrl));
            var avplayerItem = new AVPlayerItem(avasset);
             _avPlayer.ReplaceCurrentItemWithPlayerItem(avplayerItem);
            _avPlayer.Play();

            UpdateNowPlaying(story);
        }

        public void Stop()
        {
            _avPlayer.Pause();
        }

        public void UpdateNowPlaying(StorySong storySong)
        {
            var newInfo = CreateInfo(storySong);
            MPNowPlayingInfoCenter.DefaultCenter.NowPlaying = newInfo;
        }

        private MPNowPlayingInfo CreateInfo(StorySong item)
        {
            var newInfo = new MPNowPlayingInfo();
            newInfo.Artwork = artwork;
            newInfo.Title = item.Name;
            newInfo.AssetUrl = NSUrl.FromString(item.FullImageUrl);
            return newInfo;
        }
    }

    public class NowPlayingCenterHelper
    {

        
    }
}
