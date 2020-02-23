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
using MvvmCross.Converters;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace KazkySuspilne.iOS.Views
{
    [MvxModalPresentation(ModalPresentationStyle = UIModalPresentationStyle.FormSheet, WrapInNavigationController = true)]
    public partial class StoryPlayerController : MvxViewController<SongPlayerViewModel>
    {
        private string imageUrl;
        private UIViewBounceGestureRecognizerBehavior playPauseViewRecognizer;
        private UIViewBounceGestureRecognizerBehavior nextRecognizer;
        private UIViewBounceGestureRecognizerBehavior previousViewRecognizer;

        public StoryPlayerController()
            : base("StoryPlayerController", null)
        {
            this.playPauseViewRecognizer = new UIViewBounceGestureRecognizerBehavior();
            this.nextRecognizer = new UIViewBounceGestureRecognizerBehavior();
            this.previousViewRecognizer = new UIViewBounceGestureRecognizerBehavior();
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

            this.PlayPauseView.AddGestureRecognizer(this.playPauseViewRecognizer);
            this.PreviousButton.AddGestureRecognizer(this.previousViewRecognizer);
            this.NextButton.AddGestureRecognizer(this.nextRecognizer);

            var bindingSet = this.CreateBindingSet<StoryPlayerController, SongPlayerViewModel>();
            bindingSet.Bind().For(v => v.ImageUrl).To(vm => vm.PlayerViewModel.CurrentMediaItem.ImageUri);
            bindingSet.Bind(Slider).For(v => v.MaxValue).To(v => v.PlayerViewModel.CurrentDurationSeconds);
            bindingSet.Bind(Slider).To(v => v.PlayerViewModel.CurrentPositionSeconds);
            bindingSet.Bind(NextButton).To(vm => vm.PlayerViewModel.PlayNextCommand);
            bindingSet.Bind(PreviousButton).To(vm => vm.PlayerViewModel.PlayPreviousCommand);
            
            bindingSet.Bind(playPauseViewRecognizer).For(v => v.TapCommand).To(vm => vm.PlayerViewModel.PlayPauseCommand);
            bindingSet.Bind(nextRecognizer).For(v => v.TapCommand).To(vm => vm.PlayerViewModel.PlayNextCommand);
            bindingSet.Bind(previousViewRecognizer).For(v => v.TapCommand).To(vm => vm.PlayerViewModel.PlayPreviousCommand);
            bindingSet.Bind(CurrentTimeLabel).To(vm => vm.PlayerViewModel.CurrentPositionSeconds)
                .WithConversion(new SecondsToStringConverter());
            bindingSet.Bind(DurationTimeLabel).To(vm => vm.PlayerViewModel.CurrentDurationSeconds)
                .WithConversion(new SecondsToStringConverter());

            bindingSet.Bind(NameLabel).To(vm => vm.PlayerViewModel.CurrentMediaItem.Title);
            bindingSet.Bind(AuthorLabel).To(vm => vm.PlayerViewModel.CurrentMediaItem.Artist);

            bindingSet.Bind(PlayButton).For(v => v.Hidden).To(vm => vm.PlayerViewModel.IsPlaying);
            bindingSet.Bind(PauseButton).For(v => v.Hidden).To(vm => vm.PlayerViewModel.IsPlaying)
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
    public class BoolInverterConverter : MvxValueConverter
    {

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

