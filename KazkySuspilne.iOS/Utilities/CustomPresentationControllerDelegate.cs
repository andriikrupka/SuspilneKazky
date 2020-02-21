using System;
using Foundation;
using UIKit;

namespace KazkySuspilne.iOS.Utilities
{
    public class CustomPresentationControllerDelegate : UIAdaptivePresentationControllerDelegate
    {
        private Action _dismissModal;

        public CustomPresentationControllerDelegate(Action dismissModal)
        {
            _dismissModal = dismissModal;
        }

        [Export("presentationControllerDidDismiss:")]
        public override void DidDismiss(UIPresentationController presentationController)
        {
            _dismissModal?.Invoke();
        }
    }

}
