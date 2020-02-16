using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace KazkySuspilne.iOS.Views
{
    public class NewsFlowLayoutCollectionViewSource : MvxCollectionViewSource, IUICollectionViewDelegateFlowLayout
    {
        public NewsFlowLayoutCollectionViewSource(UICollectionView collectionView, NSString defaultCellIdentifier) : base(collectionView, defaultCellIdentifier)
        {

        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var padding = 25;
            var collectionCellSize = collectionView.Frame.Size.Width - padding;
            return new CGSize(width: collectionCellSize / 1, height: collectionCellSize / 1);
        }
    }

}

