using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using SuspilneKazky.DataAccess;
using SuspilneKazky.Models;

namespace SuspilneKazky.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class StoriesViewModel : MvxViewModel
    {
        private readonly IMediaProvider _mediaProvider;
        private readonly IAudioManager _audioManager;

        public StoriesViewModel(IMediaProvider mediaProvider, IAudioManager audioManager)
        {
            _mediaProvider = mediaProvider;
            _audioManager = audioManager;
            PlaySongCommand = new MvxCommand<StorySong>(PlaySoundExecute);
        }

		private void PlaySoundExecute(StorySong storySong)
        {
            if (storySong != null)
            {
                _audioManager.Play(storySong);
            }
        }

        public override Task Initialize()
		{
            this.LoadData();
            return base.Initialize();
		}

        public List<StorySong> Items { get; set; }
        public MvxCommand<StorySong> PlaySongCommand { get; set; }
        private async void LoadData()
        {
            try
            {
                Items = await _mediaProvider.LoadStoriesAsync();
                _audioManager.SetupItems(Items);
            }
            catch
            {
                
            }
        }
	}
}
