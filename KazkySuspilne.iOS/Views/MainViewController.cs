using System;
using CoreGraphics;
using Foundation;
using KazkySuspilne.iOS.Cells;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace KazkySuspilne.iOS.Views
{
    public partial class MainViewController : MvxViewController<MainViewModel>
    {
        private MvxSimpleTableViewSource _source;

        public MainViewController()
            : base("MainViewController", null)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(true, true);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            StoriesCollectionView.AlwaysBounceVertical = true;
            StoriesCollectionView.RegisterNibForCell(StoryViewCell.Nib, StoryViewCell.Key);
            var source = new NewsFlowLayoutCollectionViewSource(StoriesCollectionView, StoryViewCell.Key);
            StoriesCollectionView.Source = source;
            var set = this.CreateBindingSet<MainViewController, MainViewModel>();
            set.Bind(source).To(vm => vm.Stories);
            set.Bind(source).For(x => x.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Apply();

            StoriesCollectionView.ReloadData();
            StoriesCollectionView.AllowsSelection = true;

            var image = UIImage.FromBundle("background-img");
            this.View.BackgroundColor = UIColor.FromPatternImage(image);
        }

    }

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

