using System;
using MediaManager.Queue;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace KazkySuspilne.ViewModels
{
    public class SongPlayerViewModel : MvxNavigationViewModel<MediaQueue>
    {
        private MediaQueue _mediaQueue;

        public SongPlayerViewModel(PlayerViewModel playerViewModel, IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this.PlayerViewModel = playerViewModel;
            CloseCommand = new MvxCommand(() => NavigationService.Close(this));
        }

        public PlayerViewModel PlayerViewModel { get; }
        public MvxCommand CloseCommand { get; set; }


        public override async void Prepare(MediaQueue parameter)
        {
            _mediaQueue = parameter;
            PlayerViewModel.CurrentMediaItem = _mediaQueue.Current;
            PlayerViewModel.MediaManager.Queue = _mediaQueue;
            await PlayerViewModel.MediaManager.PlayQueueItem(PlayerViewModel.CurrentMediaItem);
        }
    }
}
