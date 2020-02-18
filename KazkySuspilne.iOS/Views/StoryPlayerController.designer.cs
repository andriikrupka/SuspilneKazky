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
	[Register ("StoryPlayerController")]
	partial class StoryPlayerController
	{
		[Outlet]
		UIKit.UIImageView BackgroundImageView { get; set; }

		[Outlet]
		UIKit.UILabel CurrentTimeLabel { get; set; }

		[Outlet]
		UIKit.UILabel DurationTimeLabel { get; set; }

		[Outlet]
		UIKit.UIButton NextButton { get; set; }

		[Outlet]
		UIKit.UIButton PauseButton { get; set; }

		[Outlet]
		UIKit.UIButton PlayButton { get; set; }

		[Outlet]
		UIKit.UIButton PreviousButton { get; set; }

		[Outlet]
		UIKit.UISlider Slider { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BackgroundImageView != null) {
				BackgroundImageView.Dispose ();
				BackgroundImageView = null;
			}

			if (CurrentTimeLabel != null) {
				CurrentTimeLabel.Dispose ();
				CurrentTimeLabel = null;
			}

			if (DurationTimeLabel != null) {
				DurationTimeLabel.Dispose ();
				DurationTimeLabel = null;
			}

			if (NextButton != null) {
				NextButton.Dispose ();
				NextButton = null;
			}

			if (PauseButton != null) {
				PauseButton.Dispose ();
				PauseButton = null;
			}

			if (PlayButton != null) {
				PlayButton.Dispose ();
				PlayButton = null;
			}

			if (PreviousButton != null) {
				PreviousButton.Dispose ();
				PreviousButton = null;
			}

			if (Slider != null) {
				Slider.Dispose ();
				Slider = null;
			}
		}
	}
}
