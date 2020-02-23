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

            PlayCommand = new MvxCommand(Play);
            PlayerViewModel = playerViewModel;

            
        }

        private void Play()
        {
            if (PlayerViewModel.CurrentMediaItem != radioItem)
            {
                PlayerViewModel.CurrentMediaItem = radioItem;
            }

            PlayerViewModel.MediaManager.Play(radioItem);
        }


        public MvxCommand PlayCommand { get; }
        public PlayerViewModel PlayerViewModel { get; }
        public MvxCommand PauseCommand { get; }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            CrossMediaManager.Current.Play("https://radio.nrcu.gov.ua:8443/kazka-mp3");
        }
    }
}
