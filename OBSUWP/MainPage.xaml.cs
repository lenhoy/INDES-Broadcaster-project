using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.DataClasses;
using System;
using System.Diagnostics;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OBSUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        internal MainPageViewModel VM { get; }

        // rightclicked scene as context for scene deleted flyout click handler
        private Scene rightClickedScene { get; set; }

        // selected Scene in the Playlist UI
        private Scene playlistSelectedScene; // TODO: methods to save and update this

        // Animation of the infopanel
        private Storyboard infopanelStoryboard = new Storyboard();

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = VM = new MainPageViewModel();
            InitAnimateInfopanel();

        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            VM.PreviewScene = (Scene)e.ClickedItem;
            SendToLiveButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Scene gridview item rightclick. Set rightClicked scene and show the flyout
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

        private void SceneGridViewFlyoutItem_ChangeName_Click(object sender, RoutedEventArgs e)
        {
            VM.ChangeSceneNameCommand.Execute(rightClickedScene);
        }

        #region Playlist UI

        private void PlaylistListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Toggle visibility of the PlayList info panel
            if (PlaylistInfopanel.Visibility.Equals(Visibility.Collapsed))
            {
                // Show and animate
                PlaylistInfopanel.Visibility = Visibility.Visible;
                infopanelStoryboard.Begin();
            } else
            {
                PlaylistInfopanel.Visibility = Visibility.Collapsed;
            }

        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox tb = (TextBox)sender;
            string inputString = tb.Text;

            try
            {
                int inputInt = int.Parse(inputString);
                VM.SetTimeCommand.Execute(new Tuple<Scene, int?>(playlistSelectedScene, inputInt));

            }
            catch (FormatException)
            {
                Debug.WriteLine("Cannot parse string to integer in textbox");
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            InputTimeTextbox.IsEnabled = false;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            InputTimeTextbox.IsEnabled = true;
        }

        #endregion

        #region animation
        private void InitAnimateInfopanel()
        {
            // Slide animation
            DoubleAnimation slideAnimation = new DoubleAnimation();
            slideAnimation.From = -20;
            slideAnimation.To = 0;
            slideAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            var ease = new ExponentialEase();
            ease.Exponent = -2;
            ease.EasingMode = EasingMode.EaseIn;
            slideAnimation.EasingFunction = ease;


            infopanelStoryboard.Children.Add(slideAnimation);

            Storyboard.SetTarget(slideAnimation, PanelCompositeTransform);
            Storyboard.SetTargetProperty(slideAnimation, "TranslateX");

            // Opacity animation
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = 0;
            opacityAnimation.To = 1;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            infopanelStoryboard.Children.Add(opacityAnimation);

            Storyboard.SetTarget(opacityAnimation, PlaylistInfopanel);
            Storyboard.SetTargetProperty(opacityAnimation, "Opacity");
        }
        #endregion
    }
}
