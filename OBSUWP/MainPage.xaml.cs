using OBSUWP.Controls;
using OBSUWP.DataClasses;
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

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = VM = new MainPageViewModel();
            // Subscribe to redraw scenes when the sources inside the scenes change
            // Hacky solution as I couldn't make it work 'normally'
            VM.SourcesChanged += ReDrawScenes; 

        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            VM.PreviewScene = (Scene)e.ClickedItem;
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

        }        

        public void ReDrawScenes()
        {

            //preView.DrawUI();
        }

    }
}
