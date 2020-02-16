using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace KazkySuspilne
{
    public class KazkyvxAppStart : MvxAppStart
    {
        public KazkyvxAppStart(IMvxApplication application, IMvxNavigationService navigationService)
            : base(application, navigationService)
        {
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            // Never do this until Cross team gets this issue fixed
            // await NavigationService.Navigate<LoginViewModel>();
            // https://github.com/MvvmCross/MvvmCross/pull/3222
            // https://github.com/MvvmCross/MvvmCross/issues/3209 - workaround was taken from here
            // https://github.com/MvvmCross/MvvmCross/issues/2953
            // https://github.com/MvvmCross/MvvmCross/issues/3221
            NavigationService.Navigate<MainViewModel>();
        }
    }
}