using System;
using System.Linq;
using System.Threading.Tasks;
using KazkySuspilne.Models;
using KazkySuspilne.Services;
using KazkySuspilne.ViewModels;
using MediaManager.Queue;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace KazkySuspilne.ViewModels
{
    public class StoriesViewModel : MvxNavigationViewModel
    {
        private readonly ISuspilneService _suspilneService;

        public StoriesViewModel(ISuspilneService suspilneService, IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            _suspilneService = suspilneService;
            Stories = new MvxObservableCollection<StorySongItemViewModel>();
            ItemSelectedCommand = new MvxCommand<StorySongItemViewModel>(ItemSelected);
        }

        private void ItemSelected(StorySongItemViewModel storySongItemViewModel)
        {
            var queue = new MediaQueue();
            for (var i = 0; i < Stories.Count; i++)
            {
                var item = Stories[i];
                queue.Add(item.StorySong.AsMediaItem());
                if (item == storySongItemViewModel)
                {
                    queue.CurrentIndex = i;
                }
            }
            

            NavigationService.Navigate<SongPlayerViewModel, MediaQueue>(queue);
        }

        public MvxObservableCollection<StorySongItemViewModel> Stories { get; set; }
        public MvxCommand<StorySongItemViewModel> ItemSelectedCommand { get; }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewCreated()
        {
            base.ViewCreated();
            LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                var storySongs = await _suspilneService.GetStories();
                Stories.AddRange(storySongs.Select(x => new StorySongItemViewModel(x)));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
