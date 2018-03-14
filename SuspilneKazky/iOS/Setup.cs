using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
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

        }

        protected override void OnInitialazing()
        {
         
        }

    }
}