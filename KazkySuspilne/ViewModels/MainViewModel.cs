using System;
using System.Linq;
using System.Threading.Tasks;
using KazkySuspilne.Services;
using KazkySuspilne.ViewModels;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace KazkySuspilne
{
    public class MainViewModel : MvxNavigationViewModel
    {
        private readonly ISuspilneService _suspilneService;


        public MainViewModel(ISuspilneService suspilneService, IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            _suspilneService = suspilneService;
            Stories = new MvxObservableCollection<StorySongItemViewModel>();
        }

        public MvxObservableCollection<StorySongItemViewModel> Stories { get; set; }


        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override async void ViewAppeared()
        {
            var data = await _suspilneService.GetStories();
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
