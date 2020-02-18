using System;
using KazkySuspilne.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace KazkySuspilne.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxRootPresentation()]
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

            TabBar.Translucent = false;
            TabBar.BackgroundColor = UIColor.Clear;
            TabBar.TintColor = UIColor.Green;
            TabBar.BarTintColor = UIColor.Clear;
            TabBar.SelectedImageTintColor = UIColor.Green;
            TabBar.BackgroundImage = null;
        }
    }
}