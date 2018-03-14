using System;
using FFImageLoading;
using FFImageLoading.Work;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using SuspilneKazky.Models;
using UIKit;

namespace SuspilneKazky.iOS.ViewCells
{
    public partial class StorySongViewCell : MvxCollectionViewCell
    {
        public static readonly NSString Key = new NSString("StorySongViewCell");
        public static readonly UINib Nib = UINib.FromName("StorySongViewCell", NSBundle.MainBundle);


        public static StorySongViewCell Create()
        {
            return (StorySongViewCell)Nib.Instantiate(null, null)[0];
        }

        private string _imageUrl;
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
            get { return _imageUrl; }
        }

        protected StorySongViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<StorySongViewCell, StorySong>();
                set.Bind().For(x => x.ImageUrl).To(k => k.ImageUri);
                set.Apply();
            });
        }

        protected void UpdateContent()
        {
            PreviewImage.Image = null;
            ImageService.Instance.LoadUrl(ImageUrl)
                        ////.LoadingPlaceholder("news_placeholder.png", ImageSource.ApplicationBundle)
                        ////.ErrorPlaceholder("news_placeholder.png", ImageSource.ApplicationBundle)
                        .Into(PreviewImage);
        }
    }
}
