using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Controls;
using OBSUWP.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture.Frames;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OBSUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.VM = new MainPageViewModel();
            //VM.PropertyChanged += VM_PropertyChanged;

        }
        internal MainPageViewModel VM { get; set; }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //VM.SelectedScene = (Scene) e.ClickedItem;
            //preView.InputScene = (Scene) e.ClickedItem;
            VM.PreviewScene = (Scene)e.ClickedItem;
        }

        private void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // If the SelectedScene has changed update the liveView
            if (e.PropertyName == nameof(VM.LiveScene))
            {
                liveView.InputScene = (Scene) VM.LiveScene;
            }

            if (e.PropertyName == nameof(VM.PreviewScene))
            {
                preView.InputScene = (Scene) VM.PreviewScene;
            }
        }
    }
}
