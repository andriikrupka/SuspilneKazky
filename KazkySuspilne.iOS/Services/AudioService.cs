using System;
using System.Linq;
using System.Collections.Generic;
using AVFoundation;
using CoreMedia;
using Foundation;
using KazkySuspilne.Models;
using KazkySuspilne.Services;
using MediaPlayer;

namespace KazkySuspilne.iOS.Services
{
    public class AudioService : IAudioService
    {
        private AVPlayer _avPlayer;
        private MPMediaItemArtwork artwork;
        private int _timeScale;
        private List<StorySong> _playList;
        private int _currentIndex = -1;
        private StorySong _currentSong;
        private AudioMode _audioMode;
        private NSObject _token;

        public PlaybackMode PlaybackMode { get; private set; }

        public StorySong CurrentSong
        {
            get => _currentSong;
            set
            {
                _currentSong = value;
                SongChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public AudioMode AudioMode
        {
            get => _audioMode;
            set
            {
                _audioMode = value;
                StatusChanged?.Invoke(this, new AudioModeEventArgs(_audioMode));
            }
        }

        public event EventHandler SongEnded;
        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler SongChanged;
        public event EventHandler<AudioModeEventArgs> StatusChanged;

        public AudioService()
        {
            _avPlayer = new AVPlayer();
            var timeScale = new CMTimeScale(1);
            var time = CMTime.FromSeconds(1, timeScale.Value);
            _avPlayer.AddPeriodicTimeObserver(time, CoreFoundation.DispatchQueue.MainQueue, PlayerHandler);
        }

        private void PlayerHandler(CMTime obj)
        {
            var duration = _avPlayer.CurrentItem.Duration.Seconds;
            var position = _avPlayer.CurrentTime.Seconds;

            PositionChanged?.Invoke(this, new PositionEventArgs(position, duration));
        }

        public void Play(StorySong story)
        {
            Stop();
            CurrentSong = story;
            var avasset = AVAsset.FromUrl(NSUrl.FromString(story.FullSongUrl));
            
            _timeScale = avasset.Duration.TimeScale;
            var avplayerItem = new AVPlayerItem(avasset);
            RemoveToken();
            _token = NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification, DidFinishPlaying, avplayerItem);
            _avPlayer.ReplaceCurrentItemWithPlayerItem(avplayerItem);
            _avPlayer.Play();
            UpdateNowPlaying(story);
        }

        private void DidFinishPlaying(NSNotification obj)
        {
            SongEnded?.Invoke(this, EventArgs.Empty);
            RemoveToken();
        }

        private void RemoveToken()
        {
            if (_token != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_token);
                _token.Dispose();
                _token = null;
            }
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

        public void Seek(double position)
        {
            _avPlayer.Seek(CMTime.FromSeconds(position, _timeScale));
        }

        public void SetPlaylist(List<StorySong> playlist)
        {
            _currentIndex = -1;
            _playList = playlist;
        }

        public bool PlayNext()
        {
            var newIndex = _currentIndex + 1;
            if (newIndex >= _playList.Count)
            {
                newIndex = 0;
            }
            _currentIndex = newIndex;

            Play(_playList[_currentIndex]);

            return true;
        }

        public bool PlayPrevious()
        {
            var newIndex = _currentIndex - 1;
            if (newIndex < 0)
            {
                newIndex = _playList.Count - 1;
            }

            _currentIndex = newIndex;

            Play(_playList[_currentIndex]);
            return true;
        }

        public void SetPlaybackMode(PlaybackMode playbackMode)
        {
            PlaybackMode = playbackMode;
        }

        public void Play()
        {
            _avPlayer.Play();
        }
    }
}
