using Foundation;
using KazkySuspilne.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace KazkySuspilne.iOS
{
    [MvxFromStoryboard("Main")]
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class ViewController : UIViewController
    {
        private bool _isPresentedFirstTime = true;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        
    }
}