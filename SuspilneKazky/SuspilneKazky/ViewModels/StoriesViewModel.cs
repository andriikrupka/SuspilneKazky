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

        public StoriesViewModel(IMediaProvider mediaProvider)
        {
            _mediaProvider = mediaProvider;
            PlaySongCommand = new MvxCommand<StorySong>(PlaySoundExecute);
        }

		private void PlaySoundExecute(StorySong storySong)
        {
            if (storySong != null)
            {
                Debug.WriteLine(storySong.Name);
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
            }
            catch
            {
                
            }
        }
	}
}
