using System;
using UIKit;

namespace KazkySuspilne.iOS.Utilities
{
    public static class UIHelper
    {
        public static void ApplyCornerRadius(this UIView view, float cornerRadius)
        {
            view.Layer.CornerRadius = cornerRadius;
            view.Layer.MasksToBounds = true;
        }
    }
}
