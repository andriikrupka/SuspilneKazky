// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace KazkySuspilne.iOS.Cells
{
	[Register ("StoryViewCell")]
	partial class StoryViewCell
	{
		[Outlet]
		UIKit.UILabel AuthorLabel { get; set; }

		[Outlet]
		UIKit.UIView AuthorView { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UIView NameView { get; set; }

		[Outlet]
		UIKit.UIImageView StoryImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (StoryImageView != null) {
				StoryImageView.Dispose ();
				StoryImageView = null;
			}

			if (NameView != null) {
				NameView.Dispose ();
				NameView = null;
			}

			if (AuthorView != null) {
				AuthorView.Dispose ();
				AuthorView = null;
			}

			if (AuthorLabel != null) {
				AuthorLabel.Dispose ();
				AuthorLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}
		}
	}
}
