using System;
using System.Collections.Generic;
using KazkySuspilne.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace KazkySuspilne.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = false, TabName = "Радіо", TabIconName = "radio-icon", TabSelectedIconName = "radio-icon")]
    public partial class RadioViewController : MvxViewController<RadioViewModel>
    {
        UIViewBounceGestureRecognizerBehavior playButtonBehavior;
        public RadioViewController() : base("RadioViewController", null)
        {
            this.playButtonBehavior = new UIViewBounceGestureRecognizerBehavior();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var image = UIImage.FromBundle("background-img");
            this.View.BackgroundColor = UIColor.FromPatternImage(image);

            this.ButtonWrapView.AddGestureRecognizer(this.playButtonBehavior);


            var bindingSet = this.CreateBindingSet<RadioViewController, RadioViewModel>();
            bindingSet.Bind(playButtonBehavior).For(v => v.TapCommand).To(vm => vm.PlayPauseCommand);
            bindingSet.Bind(PlayButton).For(v => v.Hidden).To(vm => vm.IsRadioPlaying);
            bindingSet.Bind(PauseButton).For(v => v.Hidden).To(vm => vm.IsRadioPlaying)
                .WithConversion(
                new BoolInverterConverter())
                .WithDictionaryConversion(
                new Dictionary<bool, bool>
                {
                    {true, false},
                    {false, true}
                });

            bindingSet.Apply();
        }
    }
}

