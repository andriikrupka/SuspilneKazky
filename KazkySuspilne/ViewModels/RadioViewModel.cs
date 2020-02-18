using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;
using MvvmCross.ViewModels;

namespace KazkySuspilne.ViewModels
{
    public class RadioViewModel : MvxViewModel
    {
        private Radio radioItem;

        public RadioViewModel()
        {
            radioItem = new Radio();
            radioItem.Title = "UA:Казки";
            radioItem.Description = "Суспільне";
            radioItem.Uri = "https://radio.nrcu.gov.ua:8443/kazka-mp3";
            radioItem.PropertyChanged += RadioItem_PropertyChanged;
            radioItem.MediaItems = new List<IMediaItem> {  };
            CrossMediaManager.Current.StateChanged += Current_StateChanged;
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.MediaItemFailed += Current_MediaItemFailed;
        }

        private void Current_MediaItemFailed(object sender, MediaManager.Media.MediaItemFailedEventArgs e)
        {
            
        }

        private void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            
        }

        private void Current_StateChanged(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            Console.WriteLine(e.State);
            if (e.State == MediaManager.Player.MediaPlayerState.Failed)
            {

            }
        }

        private void RadioItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        public override Task Initialize()
        {
            
            return base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            CrossMediaManager.Current.Play("https://radio.nrcu.gov.ua:8443/kazka-mp3");
        }
    }
}
