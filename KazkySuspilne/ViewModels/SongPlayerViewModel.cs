using System;
using System.Threading.Tasks;
using KazkySuspilne.Models;
using KazkySuspilne.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace KazkySuspilne.ViewModels
{
    public class StoryPlayerViewModel : MvxViewModel<Models.StorySong>
    {
        private double _totalDuration;
        private double _currentPosition;
        private IAudioService _audioService;
        private StorySong _storySong;
        private bool _isPlaying;
        public StoryPlayerViewModel(IAudioService audioService)
        {
            _audioService = audioService;
            _audioService.PositionChanged += _audioService_PositionChanged;
            _audioService.SongChanged += _audioService_SongChanged;
            _audioService.SongEnded += _audioService_SongEnded;
            _audioService.StatusChanged += _audioService_StatusChanged;
            PlayNextCommand = new MvxCommand(PlayNext);
            PlayCommand = new MvxCommand(Play);
            PauseCommand = new MvxCommand(Pause);
            PlayPreviousCommand = new MvxCommand(PlayPrevious);
        }

        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        private void _audioService_StatusChanged(object sender, AudioModeEventArgs e)
        {
            
        }

        private void PlayPrevious()
        {
            _audioService.PlayPrevious();
        }

        private void Pause()
        {
            IsPlaying = false;
            _audioService.Stop();
        }

        private void Play()
        {
            IsPlaying = true;
            if (_audioService.CurrentSong == null)
            {
                _audioService.Play(StorySong);
            }
            else
            {
                _audioService.Play();
            }
        }

        private void PlayNext()
        {
            _audioService.PlayNext();
        }

        public MvxCommand PlayNextCommand { get; set; }
        public MvxCommand PlayCommand { get; set; }
        public MvxCommand PauseCommand { get; set; }
        public MvxCommand PlayPreviousCommand { get; set; }

        private void _audioService_SongEnded(object sender, EventArgs e)
        {
            PlayNext();
        }

        private void _audioService_SongChanged(object sender, EventArgs e)
        {
            StorySong = _audioService.CurrentSong;
        }

        public double CurrentPosition
        {
            get => _currentPosition;
            set
            {
                SetProperty(ref _currentPosition, value);
                _audioService.Seek(value);
            }
        }

        public double TotalDuration
        {
            get => _totalDuration;
            set
            {
                SetProperty(ref _totalDuration, value);
            }
        }

        public StorySong StorySong
        {
            get => _storySong;
            set
            {
                SetProperty(ref _storySong, value);
            }
        }

        public override void Prepare(StorySong parameter)
        {
            StorySong = parameter;
            Play();
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private void _audioService_PositionChanged(object sender, PositionEventArgs e)
        {
            TotalDuration = e.TotalDuration;

            _currentPosition = e.CurrentPosition;
            RaisePropertyChanged(nameof(CurrentPosition));
        }
    }
}
