
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using SuspilneKazky.DataAccess;
using SuspilneKazky.ViewModels;

namespace SuspilneKazky
{
    public class BaseApp : MvxApplication
    {
        public override void Initialize()
        {

            OnInitialazing();

            Mvx.ConstructAndRegisterSingleton<IMediaProvider, MediaProvider>();

            Mvx.RegisterType<RadioViewModel, RadioViewModel>();
            Mvx.RegisterType<StoriesViewModel, StoriesViewModel>();
            Mvx.RegisterType<MainViewModel, MainViewModel>();

            RegisterSpecificServices();

            RegisterNavigationServiceAppStart<MainViewModel>();
        }

        protected virtual void OnInitialazing()
        {

        }


        protected virtual void RegisterSpecificServices()
        {

        }
    }
}