using System;
using MvvmCross.Core.ViewModels;

namespace SuspilneKazky.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        public MainViewModel()
        {
            ShowInitialViewModelsCommand = new MvxCommand(ShowInitialViewModelsExecute);
        }

        public MvxCommand ShowInitialViewModelsCommand { get; private set; }

        private void ShowInitialViewModelsExecute()
        {
            ShowViewModel<StoriesViewModel>();
            ShowViewModel<RadioViewModel>();
        }
    }
}
