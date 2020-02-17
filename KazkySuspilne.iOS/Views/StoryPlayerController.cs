using System.Collections.Generic;
using FFImageLoading;
using KazkySuspilne.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace KazkySuspilne.iOS.Views
{


    [MvxChildPresentation()]
    public partial class StoryPlayerController : MvxViewController<PlayerViewModel>
    {
        private string _imageUrl;

        public StoryPlayerController()
            : base("StoryPlayerController", null)
        {
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                UpdateImage();
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(false, true);
        }

        private void UpdateImage()
        {
            BackgroundImageView.Image = null;
            ImageService.Instance.LoadUrl(ImageUrl)
                                 .Into(BackgroundImageView);
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
}

