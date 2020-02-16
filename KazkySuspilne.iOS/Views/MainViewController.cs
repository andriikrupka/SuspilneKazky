using System;
using KazkySuspilne.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace KazkySuspilne.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        private bool _isPresentedFirstTime = true;

        public MainViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (ViewModel != null && _isPresentedFirstTime)
            {
                _isPresentedFirstTime = false;
                ViewModel.ShowInitialViewModelsCommand.Execute(null);
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBar.Hidden = true;
            TabBar.SelectedImageTintColor = UIColor.FromRGB(220, 83, 67);
            TabBar.TintColor = UIColor.White;
        }
    }
}