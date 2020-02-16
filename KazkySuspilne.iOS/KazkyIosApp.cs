using KazkySuspilne.Services;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;

namespace KazkySuspilne.iOS
{
    public class KazkyIosApp : MvxIosSetup<KazkyApp>
    {
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            Mvx.IoCProvider.RegisterSingleton<IAudioService>(new Services.AudioService());
        }
    }
}