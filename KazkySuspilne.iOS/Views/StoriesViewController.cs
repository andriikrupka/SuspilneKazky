using CoreGraphics;
using KazkySuspilne.iOS.Cells;
using KazkySuspilne.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace KazkySuspilne.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true,
        TabName = "Казки",
        TabIconName = "story-icon",
        TabSelectedIconName = "story-icon")]
    public partial class StoriesViewController : MvxViewController<StoriesViewModel>
    {
        public StoriesViewController() : base("StoriesViewController", null)
        {
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);


            var imageView = new UIImageView()
            {
                Image = UIImage.FromBundle("info-icon")
            };

            var infoButton = new UIBarButtonItem(imageView);

            NavigationItem.RightBarButtonItem = infoButton;



            NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationController.NavigationBar.ShadowImage = new UIImage();
            NavigationController.NavigationBar.BackgroundColor = UIColor.Clear;
            NavigationController.NavigationBar.TintColor = UIColor.White;
            NavigationController.NavigationBar.BarTintColor = UIColor.Clear;
            NavigationController.NavigationBar.Translucent = true;
        }

        

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            StoriesCollectionView.AlwaysBounceVertical = true;
            StoriesCollectionView.RegisterNibForCell(StoryViewCell.Nib, StoryViewCell.Key);
            var source = new NewsFlowLayoutCollectionViewSource(StoriesCollectionView, StoryViewCell.Key);
            StoriesCollectionView.Source = source;
            var set = this.CreateBindingSet<StoriesViewController, StoriesViewModel>();
            set.Bind(source).To(vm => vm.Stories);
            set.Bind(source).For(x => x.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Apply();

            StoriesCollectionView.ReloadData();
            StoriesCollectionView.AllowsSelection = true;

            var image = UIImage.FromBundle("background-img");
            this.View.BackgroundColor = UIColor.FromPatternImage(image);
        }
    }
}

