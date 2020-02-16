using System;
using FFImageLoading;
using Foundation;
using KazkySuspilne.iOS.Utilities;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace KazkySuspilne.iOS.Cells
{
    public partial class StoryViewCell : MvxCollectionViewCell
    {
        public static readonly NSString Key = new NSString("StoryViewCell");
        public static readonly UINib Nib;

        static StoryViewCell()
        {
            Nib = UINib.FromName("StoryViewCell", NSBundle.MainBundle);
        }

        private string _imageUrl;

        protected StoryViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public string ImageUrl
        {
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    UpdateContent();
                }
            }
            get => _imageUrl;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            NameView.ApplyCornerRadius(2);
            AuthorView.ApplyCornerRadius(2);
            this.DelayBind(Bind);

            this.BackgroundColor = UIColor.Orange;

            AddBlurView(NameView);
            AddBlurView(AuthorView);
        }

        private void AddBlurView(UIView view)
        {
            var effect = UIBlurEffect.FromStyle(UIBlurEffectStyle.ExtraLight);
            var blurEffectView = new UIVisualEffectView(effect);
            blurEffectView.Frame = view.Bounds;
            blurEffectView.Alpha = 0.5f;
            blurEffectView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
            view.InsertSubview(blurEffectView, 0);
        }

        private void Bind()
        {
            var bindingSet = this.CreateBindingSet<StoryViewCell, ViewModels.StorySongItemViewModel>();
            bindingSet.Bind(NameLabel).To(vm => vm.StoryName);
            bindingSet.Bind(AuthorLabel).To(vm => vm.StoryAuthor);
            bindingSet.Bind(this).For(v => v.ImageUrl).To(vm => vm.StorySong.FullImageUrl);
            bindingSet.Apply();
        }

        protected void UpdateContent()
        {
            StoryImageView.Image = null;
            ImageService.Instance.LoadUrl(ImageUrl)
                                 .DownSample(600, 600)
                                 .Into(StoryImageView);
        }
    }
}
