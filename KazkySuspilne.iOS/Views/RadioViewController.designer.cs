// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace KazkySuspilne.iOS.Views
{
	[Register ("RadioViewController")]
	partial class RadioViewController
	{
		[Outlet]
		UIKit.UIView ButtonWrapView { get; set; }

		[Outlet]
		UIKit.UIButton PauseButton { get; set; }

		[Outlet]
		UIKit.UIButton PlayButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ButtonWrapView != null) {
				ButtonWrapView.Dispose ();
				ButtonWrapView = null;
			}

			if (PauseButton != null) {
				PauseButton.Dispose ();
				PauseButton = null;
			}

			if (PlayButton != null) {
				PlayButton.Dispose ();
				PlayButton = null;
			}
		}
	}
}
