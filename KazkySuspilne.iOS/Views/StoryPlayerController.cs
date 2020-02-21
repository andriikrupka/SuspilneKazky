using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CoreGraphics;
using FFImageLoading;
using Foundation;
using KazkySuspilne.iOS.Utilities;
using KazkySuspilne.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace KazkySuspilne.iOS.Views
{
    [MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.FormSheet, WrapInNavigationController = true)]
    public partial class StoryPlayerController : MvxViewController<PlayerViewModel>
    {
        private string imageUrl;

        public StoryPlayerController()
            : base("StoryPlayerController", null)
        {
        }

        public string ImageUrl
        {
            get => this.imageUrl;
            set
            {
                this.imageUrl = value;
                this.UpdateImage();
            }
        }

        public override void WillMoveToParentViewController(UIViewController parent)
        {
            base.WillMoveToParentViewController(parent);
            parent.PresentationController.Delegate = new CustomPresentationControllerDelegate(DismissModalAsync);
        }

        private void DismissModalAsync()
        {
            this.ViewModel.CloseCommand?.Execute();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.NavigationController?.SetNavigationBarHidden(true, true);
        }

        private void UpdateImage()
        {
            this.BackgroundImageView.Image = null;
            ImageService.Instance.LoadUrl(this.ImageUrl)
                                 .Into(this.BackgroundImageView);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Slider.Continuous = false;

            var bindingSet = this.CreateBindingSet<StoryPlayerController, PlayerViewModel>();
            bindingSet.Bind().For(v => v.ImageUrl).To(vm => vm.CurrentMediaItem.ImageUri);
            bindingSet.Bind(Slider).For(v => v.MaxValue).To(v => v.CurrentDurationSeconds);
            bindingSet.Bind(Slider).To(v => v.CurrentPositionSeconds);
            bindingSet.Bind(NextButton).To(vm => vm.PlayNextCommand);
            bindingSet.Bind(PreviousButton).To(vm => vm.PlayPreviousCommand);
            bindingSet.Bind(PlayButton).To(vm => vm.PlayPauseCommand);
            bindingSet.Bind(PauseButton).To(vm => vm.PlayPauseCommand);
            bindingSet.Bind(CurrentTimeLabel).To(vm => vm.CurrentPositionSeconds)
                .WithConversion(new SecondsToStringConverter());
            bindingSet.Bind(DurationTimeLabel).To(vm => vm.CurrentDurationSeconds)
                .WithConversion(new SecondsToStringConverter());

            bindingSet.Bind(PlayButton).For(v => v.Hidden).To(vm => vm.IsPlaying);
            bindingSet.Bind(PauseButton).For(v => v.Hidden).To(vm => vm.IsPlaying)
                .WithDictionaryConversion(new Dictionary<bool, bool>
                {
                    {true, false},
                    {false, true}
                });

            bindingSet.Apply();
        }
    }

    public class UIViewBounceGestureRecognizerBehavior : UIGestureRecognizer
    {
        
        public UIViewBounceGestureRecognizerBehavior()
        {
            
        }

        public ICommand TapCommand { get; set; }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            UIView.Animate(0.2, 0, UIViewAnimationOptions.BeginFromCurrentState, () =>
            {
                this.View.Transform = CGAffineTransform.MakeScale(0.85f, 0.85f);
            }, null);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {

            UIView.Animate(0.2, 0, UIViewAnimationOptions.BeginFromCurrentState, () =>
            {
                this.View.Transform = CGAffineTransform.MakeScale(1f, 1f);
            }, null);

            var touch = touches.Take(1).OfType<UITouch>().FirstOrDefault();
            if (touch == null)
            {
                return;
            }

            var bounds = touch.LocationInView(View);
            if (base.View.Bounds.Contains(bounds))
            {
                if (TapCommand?.CanExecute(null) ?? false)
                {
                    TapCommand?.Execute(null);
                }
            }
        }
    }
}

