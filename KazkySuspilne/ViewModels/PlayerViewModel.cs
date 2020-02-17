using System;
using MediaManager;
using MediaManager.Library;
using MediaManager.Queue;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace KazkySuspilne.ViewModels
{
    public class PlayerViewModel : MvxViewModel<MediaQueue>
    {
        private double _currentDurationSeconds;
        private double _currentPositionSeconds;
        private bool _isPlaying;
        private MediaQueue _mediaQueue;
        private IMediaItem _currentMediaItem;

        public PlayerViewModel()
        {
            MediaManager.StateChanged += MediaManager_StateChanged;
            MediaManager.PositionChanged += MediaManager_PositionChanged;
            MediaManager.MediaItemChanged += MediaManager_MediaItemChanged;
            MediaManager.MediaItemFailed += MediaManager_MediaItemFailed;
            MediaManager.MediaItemFinished += MediaManager_MediaItemFinished;
            
            PlayPauseCommand = new MvxCommand(() => MediaManager.PlayPause());
            PlayNextCommand = new MvxCommand(() =>
            {
                _currentPositionSeconds = 0;
                RaisePropertyChanged(nameof(CurrentPositionSeconds));
                MediaManager.PlayNext();
            });
            PlayPreviousCommand = new MvxCommand(() =>
            {
                _currentPositionSeconds = 0;
                RaisePropertyChanged(nameof(CurrentPositionSeconds));
                MediaManager.PlayPrevious();
            });
        }

        private void MediaManager_MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
        }

        private void MediaManager_MediaItemFailed(object sender, MediaManager.Media.MediaItemFailedEventArgs e)
        {
            
        }

        public MvxCommand PauseCommand { get; }
        public MvxCommand PlayPauseCommand { get; }
        public MvxCommand PlayNextCommand { get; }
        public MvxCommand PlayPreviousCommand { get; }

        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        public double CurrentPositionSeconds
        {
            get => _currentPositionSeconds;
            set
            {
                SetProperty(ref _currentPositionSeconds, value);
                MediaManager.SeekTo(TimeSpan.FromSeconds(_currentPositionSeconds));
            }
        }

        public double CurrentDurationSeconds
        {
            get => _currentDurationSeconds;
            set => SetProperty(ref _currentDurationSeconds, value);
        }

        public IMediaManager MediaManager => CrossMediaManager.Current;

        public IMediaItem CurrentMediaItem
        {
            get => _currentMediaItem;
            set => SetProperty(ref _currentMediaItem, value);
        }

        public override async void Prepare(MediaQueue parameter)
        {
            _mediaQueue = parameter;
            CurrentMediaItem = _mediaQueue.Current;
            await MediaManager.Play(_mediaQueue);
        }

        private void MediaManager_StateChanged(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            IsPlaying = MediaManager.IsPlaying() || MediaManager.IsBuffering();
        }

        private void MediaManager_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            _currentPositionSeconds = e.Position.TotalSeconds;
            RaisePropertyChanged(nameof(CurrentPositionSeconds));

            CurrentDurationSeconds = MediaManager.Duration.TotalSeconds;
        }

        private void MediaManager_MediaItemChanged(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
            CurrentMediaItem = e.MediaItem;

        }
    }
}
