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
using Windows.Media.Capture.Frames;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OBSUWP.Controls
{
    [ObservableObject]
    public sealed partial class SceneRenderer : UserControl
    {
        [ObservableProperty]
        private Scene inputScene;

        public SceneRenderer()
        {
            this.InitializeComponent();
        }

        partial void OnInputSceneChanged(Scene value)
        {
            GenerateXAML(value);
        }

        /// <summary>
        /// Generates the XAML interface used by the template in the gridview
        /// </summary>
        /// <param name="scene"></param>
        private void GenerateXAML(Scene scene)
        {
            // Set the size of the clipping mask
            //InitializeClippingMask();

            // Hide default content
            defaultText.Visibility = Visibility.Collapsed;

            //TODO: does this copy the list or pass it by reference?
            ObservableCollection<ISource> sources = scene.Sources;

            // Adds source to the canvas using the apropriate XAML for the type
            foreach(ISource source in sources)
            {
                // determine the type of ISource implementation
                switch (source)
                {
                    case VideoSource videoSource:
                        // Create playback element and set the source
                        MediaPlayerElement mediaPlayerElement = new MediaPlayerElement();
                        MediaSource _ms = MediaSource.CreateFromUri(new Uri(source.Output));
                        mediaPlayerElement.Source = _ms;
                        

                        // Settings for the playerelement layout and behaviour
                        if (double.IsNaN(this.Width))
                        {
                            mediaPlayerElement.Width = this.ActualWidth;
                        }
                        else
                        {
                            mediaPlayerElement.Width = this.Width;
                        }
                        mediaPlayerElement.HorizontalContentAlignment = HorizontalAlignment.Center;
                        mediaPlayerElement.Stretch = Stretch.Uniform;
                        mediaPlayerElement.AutoPlay = true;
                        
                        // Add the UIelement to the canvas
                        myCanvas.Children.Add(mediaPlayerElement);

                        break;

                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Clipping mask to hide the content that overflows outside the bounds of the control
        /// </summary>
        private void InitializeClippingMask()
        {
            // Set the size of the XAML clipping mask to the given size this control
            clippingBorder.Clip = new RectangleGeometry();

            // If Height&Width not explicitly given use Actual
            if (Double.IsNaN(this.Height) && Double.IsNaN(this.Width))
            {
                // LiveView implementaiton
                clippingBorder.Clip.Rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
                return;
            }
            // GridView implementation
            clippingBorder.Clip.Rect = new Rect(0, 0, this.Width, this.Height);
        }
    }
}
