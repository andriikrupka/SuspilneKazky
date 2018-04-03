using System;
using System.Collections.Generic;
using System.Linq;
using AVFoundation;
using FFImageLoading;
using Foundation;
using MediaPlayer;
using SuspilneKazky.DataAccess;
using SuspilneKazky.Models;
using UIKit;

namespace SuspilneKazky.iOS
{
    public class AudioManager : IAudioManager
    {
        private AVPlayer _avPlayer;
        private int _currentIndex = -1;
        private List<StorySong> _items;
        private bool _isPrepared;
        public bool IsPlaying => true;

        public StorySong CurrentSong
            => _items?.ElementAtOrDefault(_currentIndex);

        public void Pause()
        {
            _avPlayer?.Pause();
        }

        public void Play()
        {
            if (_avPlayer == null)
            {
                _currentIndex = _currentIndex > 0 ? _currentIndex : 0;
                Play(_currentIndex);
            }
            else
            {
                _avPlayer?.Play();
            }
        }

        public void Play(StorySong song)
        {
            _currentIndex = _items.IndexOf(song);
            Play(_currentIndex);
        }

        public void PlayNext()
        {
            var newIndex = _currentIndex + 1;
            if (newIndex >= _items.Count)
            {
                newIndex = 0;
            }
            _currentIndex = newIndex;
            Play(_currentIndex);
        }

        public void PlayPrevious()
        {
            var newIndex = _currentIndex - 1;
            if (newIndex < 0)
            {
                newIndex = _items.Count - 1;
            }
            _currentIndex = newIndex;
            Play(_currentIndex);
        }

        public void SetupItems(List<StorySong> items)
        {
            _items = items;
        }

        public void Stop()
        {
            _avPlayer?.Pause();
            _avPlayer = null;
        }

        private void Play(int index)
        {
            Stop();
            Prepare();
            var songToPlay = _items.ElementAt(index);
            UpdateNowPlaying(songToPlay);
            var avasset = AVAsset.FromUrl(NSUrl.FromString(songToPlay.SongUri.OriginalString));
            var avplayerItem = new AVPlayerItem(avasset);
            _avPlayer = new AVPlayer(avplayerItem);
            _avPlayer.Play();
        }

        protected MPMediaItemArtwork artwork;
        protected string artworkUrl;

        private MPNowPlayingInfo CreateInfo(StorySong item)
        {
            var newInfo = new MPNowPlayingInfo();
            newInfo.Artwork = artwork;
            newInfo.Title = item.Name;
            newInfo.AssetUrl = NSUrl.FromString(item.ImageUri.OriginalString);
            return newInfo;
        }

        private void UpdateNowPlaying(StorySong item)
        {
            var newInfo = CreateInfo(item);
            UpdateInfo(newInfo);
            if (artworkUrl != item.ImageUri.OriginalString)
            {
                artworkUrl = item.ImageUri.OriginalString;
                artwork = null;
                ImageService.Instance.LoadUrl(item.ImageUri.OriginalString).AsUIImageAsync().ContinueWith((task) => {
                    artwork = new MPMediaItemArtwork(task.Result);
                    newInfo.Artwork = artwork;
                    UpdateInfo(newInfo);
                });
            }
        }

        private void UpdateInfo(MPNowPlayingInfo newInfo)
        {
            var nowPlayingInfoCenter = MPNowPlayingInfoCenter.DefaultCenter;
            nowPlayingInfoCenter.NowPlaying = newInfo;
        }

        public void Seek(double position)
        {
            if (_avPlayer == null)
            {
                return;
            }
        }

        private void Prepare()
        {
            if (!_isPrepared)
            {
                _isPrepared = true;
                var session = AVAudioSession.SharedInstance();
                NSError error;
                session.SetCategory(new NSString("AVAudioSessionCategoryPlayback"), AVAudioSessionCategoryOptions.DefaultToSpeaker, out error);
                if (error != null)
                {
                    Console.WriteLine(error);
                }
                session.SetActive(true);
                UIApplication.SharedApplication.BeginReceivingRemoteControlEvents();
                var commandCenter = MPRemoteCommandCenter.Shared;
                commandCenter.PlayCommand.Enabled = true;
                commandCenter.PauseCommand.Enabled = true;
                commandCenter.NextTrackCommand.Enabled = _items.Count > 1;
                commandCenter.PreviousTrackCommand.Enabled = _items.Count > 1;

                commandCenter.PlayCommand.AddTarget(args =>
                {
                    this.Play();
                    return MPRemoteCommandHandlerStatus.Success;
                });

                commandCenter.PauseCommand.AddTarget(args =>
                {
                    this.Pause();
                    return MPRemoteCommandHandlerStatus.Success;
                });

                commandCenter.NextTrackCommand.AddTarget(args =>
                {
                    this.PlayNext();
                    return MPRemoteCommandHandlerStatus.Success;
                });

                commandCenter.PreviousTrackCommand.AddTarget(args =>
                {
                    this.PlayPrevious();
                    return MPRemoteCommandHandlerStatus.Success;
                });
            }
        }
    }
}