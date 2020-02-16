using System;
using System.Linq;
using System.Threading.Tasks;
using KazkySuspilne.Services;
using KazkySuspilne.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace KazkySuspilne
{
    public class MainViewModel : MvxNavigationViewModel
    {
        private readonly ISuspilneService _suspilneService;
        private readonly IAudioService _audioService;

        public MainViewModel(ISuspilneService suspilneService, IAudioService audioService, IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            _suspilneService = suspilneService;
            _audioService = audioService;
            Stories = new MvxObservableCollection<StorySongItemViewModel>();
            ItemSelectedCommand = new MvxCommand<StorySongItemViewModel>(ItemSelected);
        }

        private void ItemSelected(StorySongItemViewModel item)
        {
            _audioService.Play(item.StorySong);
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
