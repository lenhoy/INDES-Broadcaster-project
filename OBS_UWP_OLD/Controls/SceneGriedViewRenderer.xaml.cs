using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OBS_UWP.Classes;
using Windows.Media.Core;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OBS_UWP.Controls
{
    public sealed partial class SceneGriedViewRenderer : UserControl
    {
        private Scene _inputScene;
        public Scene InputScene { get => this._inputScene; set { _inputScene = value; SetInputScene(); } }


        public SceneGriedViewRenderer()
        {
            this.InitializeComponent();

        }
        
        private void SetInputScene()
        {
            mediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri(InputScene.Sources[0].getOutput()));
        }
    }
}
