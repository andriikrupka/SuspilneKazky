using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace KazkySuspilne.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModelsExecute);
        }

        public MvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }

        private async Task ShowInitialViewModelsExecute()
        {
            await NavigationService.Navigate<StoriesViewModel>();
            await NavigationService.Navigate<RadioViewModel>();
        }
    }
}
