using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Logging;
using SuspilneKazky.DataAccess;
using UIKit;

namespace SuspilneKazky.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override void FillValueConverters(MvvmCross.Platform.Converters.IMvxValueConverterRegistry registry)
        {
            
        }

        protected override MvxLogProviderType GetDefaultLogProviderType()
        {
            return MvxLogProviderType.None;
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            var presenter = new MvxIosViewPresenter(ApplicationDelegate, Window);

            return presenter;
        }
    }

    public class App :  BaseApp
    {
        protected override void RegisterSpecificServices()
        {
            base.RegisterSpecificServices();
            Mvx.LazyConstructAndRegisterSingleton<IAudioManager, AudioManager>();
        }

        protected override void OnInitialazing()
        {
         
        }

    }
}