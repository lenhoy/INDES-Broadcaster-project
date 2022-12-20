using OBSUWP.DataClasses;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OBSUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        internal MainPageViewModel VM { get; }

        // save rightclicked scene as context for scene deleted flyout click handler
        private Scene rightClickedScene { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = VM = new MainPageViewModel();

        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            VM.PreviewScene = (Scene)e.ClickedItem;
        }

        /// <summary>
        /// Scene gridview item rightclick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            // save the rightclicked scene so that the SceneGridViewFlyoutItem_RemoveClick can use it
            rightClickedScene = (e.OriginalSource as FrameworkElement)?.DataContext as Scene;

            GridView gridView = (GridView)sender;
            // show the rightclick-meny at the clicked item
            gridRightClickflyout.ShowAt(gridView, e.GetPosition(gridView));
        }

        /// <summary>
        /// Add-button menuflyout click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            // Get the tag from the clicked MenuFlyoutItem in the XAML
            var item = sender as MenuFlyoutItem;
            string typeString = (string)item.Tag;

            // switch to convert tag to SceneType
            SceneType? typeEnum = null;
            switch (typeString)
            {
                case "OnlineStream":
                    typeEnum = SceneType.OnlineStream;
                    break;
                case "IPCamera":
                    typeEnum = SceneType.IPCamera;
                    break;
                case "LocalFrontCamera":
                    typeEnum = SceneType.LocalFrontCamera;
                    break;
                case "LocalBackCamera":
                    typeEnum = SceneType.LocalBackCamera;
                    break;
                case "LocalFile":
                    typeEnum = SceneType.LocalFile;
                    break;
                default:
                    break;
            }

            if (typeEnum != null)
            {
                VM.AddSceneCommand.Execute(typeEnum);
            }
        }

        private void SceneGridViewFlyoutItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            VM.RemoveSceneCommand.Execute(rightClickedScene);
        }

        private void SceneGridViewFlyoutItem_AddToPlayList_Click(object sender, RoutedEventArgs e)
        {
            // Using a touple to pass 2 arguments to command, as RelayCommands only can take 1 argument
            var inputTuple = new Tuple<Scene, int?>(rightClickedScene, null);

            VM.AddToPlaylistCommand.Execute(inputTuple);
        }
    }
}
