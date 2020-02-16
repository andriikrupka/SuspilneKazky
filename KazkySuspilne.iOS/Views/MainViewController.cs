using System;
using KazkySuspilne.iOS.Cells;
using KazkySuspilne.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace KazkySuspilne.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = false)]
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        private bool _isPresentedFirstTime = true;

        public MainViewController()
            : base("MainViewController", null)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (ViewModel != null && _isPresentedFirstTime)
            {
                _isPresentedFirstTime = false;
                ViewModel.ShowInitialViewModelsCommand.ExecuteAsync(null);
            }
        }
    }
}