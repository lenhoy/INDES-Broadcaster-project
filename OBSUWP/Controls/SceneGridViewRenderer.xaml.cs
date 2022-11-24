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
using OBSUWP.DataClasses;
using OBSUWP.Inferfaces;
using System.Collections.ObjectModel;
using Windows.Media.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OBSUWP.Controls
{
    public sealed partial class SceneGridViewRenderer : UserControl
    {
        private Scene _inputScene;
        public Scene InputScene 
        {
            get => _inputScene; 
            set
            {
                _inputScene = value;
                GenerateXAML(_inputScene);
            }
        }

        public SceneGridViewRenderer()
        {
            this.InitializeComponent();
        }
        internal SceneGridViewRenderer(Scene scene)
        {
            this.InitializeComponent();

            GenerateXAML(scene);
        }

        /// <summary>
        /// Generates the XAML interface used by the template in the gridview
        /// </summary>
        /// <param name="scene"></param>
        private void GenerateXAML(Scene scene)
        {
            // Set the size of the XAML clipping mask to the given size this control
            clippingGeometry.Rect = new Rect(0, 0, this.Width, this.Height);

            //TODO: does this copy the list or pass it by reference?
            ObservableCollection<ISource> sources = scene.Sources;

            // Adds source to the canvas using the apropriate XAML for the type
            foreach(ISource source in sources)
            {
                // determine the type of ISource implementation
                switch (source)
                {
                    case VideoSource videoSource:
                        MediaPlayerElement mediaPlayerElement = new MediaPlayerElement();
                        mediaPlayerElement.AutoPlay = true;
                        mediaPlayerElement.Height = this.Height;
                        mediaPlayerElement.HorizontalAlignment = HorizontalAlignment.Center;
                        
                        mediaPlayerElement.Stretch = Stretch.Uniform;
                        try
                        {
                            mediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri(source.Output));
                        }
                        catch (Exception)
                        {
                            new MessageDialog("Uri is Wrong");
                        }
                        myCanvas.Children.Add(mediaPlayerElement);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
