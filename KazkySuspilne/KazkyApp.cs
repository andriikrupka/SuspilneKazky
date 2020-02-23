using System;
using KazkySuspilne.Services;
using KazkySuspilne.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;

namespace KazkySuspilne
{
    public class KazkyApp : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton<ISuspilneService>(new SuspilneService());

            Mvx.IoCProvider.RegisterSingleton<PlayerViewModel>(Mvx.IoCProvider.IoCConstruct<PlayerViewModel>());

            RegisterAppStart<MainViewModel>();
        }

        
    }
}
