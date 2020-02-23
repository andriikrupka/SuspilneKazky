using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace KazkySuspilne.ViewModels
{
    public class RadioViewModel : MvxViewModel
    {
        private MediaItem radioItem;

        public RadioViewModel(PlayerViewModel playerViewModel)
        {
            radioItem = new MediaItem("https://radio.nrcu.gov.ua:8443/kazka-mp3");
            radioItem.Title = "UA:Казки";
            PlayPauseCommand = new MvxCommand(PlayPause);
            PlayerViewModel = playerViewModel;
            PlayerViewModel.PropertyChanged += PlayerViewModel_PropertyChanged;
        }

        private void PlayerViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PlayerViewModel.IsPlaying))
            {
                RaisePropertyChanged(nameof(IsRadioPlaying));
            }
        }

        public PlayerViewModel PlayerViewModel { get; }

        public MvxCommand PlayPauseCommand { get; }

        private void PlayPause()
        {
            if (PlayerViewModel.CurrentMediaItem == radioItem)
            {
                PlayerViewModel.MediaManager.PlayPause();
                RaisePropertyChanged(nameof(IsRadioPlaying));
                return;
            }

            if (PlayerViewModel.CurrentMediaItem != radioItem)
            {
                PlayerViewModel.CurrentMediaItem = radioItem;
            }

            PlayerViewModel.MediaManager.Play(radioItem);
        }

        public bool IsRadioPlaying => PlayerViewModel.IsPlaying && PlayerViewModel.CurrentMediaItem == radioItem;
    }
}
