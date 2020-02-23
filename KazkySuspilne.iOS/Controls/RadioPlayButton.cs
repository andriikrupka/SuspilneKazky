using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace KazkySuspilne.iOS.Controls
{
    public class RadioPlayButton : UIButton
    {
        public RadioPlayButton()
        {
        }

        public RadioPlayButton(UIButtonType type) : base(type)
        {
            Init();
        }

        public RadioPlayButton(NSCoder coder) : base(coder)
        {
            Init();
        }

        public RadioPlayButton(CGRect frame) : base(frame)
        {
            Init();
        }

        protected RadioPlayButton(NSObjectFlag t) : base(t)
        {
            Init();
        }

        protected internal RadioPlayButton(IntPtr handle) : base(handle)
        {
            Init();
        }

        private void Init()
        {
            var imageView = new UIImageView(UIImage.FromBundle("online-radio-button"));
            imageView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.AddSubview(imageView);

        }
    }
}
