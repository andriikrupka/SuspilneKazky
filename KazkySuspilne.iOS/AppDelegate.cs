using Foundation;
using MediaManager;
using MvvmCross.Platforms.Ios.Core;
using UIKit;

namespace KazkySuspilne.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<KazkyIosApp, KazkyApp>
    {
        public override void FinishedLaunching(UIApplication application)
        {
            base.FinishedLaunching(application);
            CrossMediaManager.Current.Play();
        }
    }
}

