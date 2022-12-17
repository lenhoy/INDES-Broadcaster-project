﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using OBSUWP.DataClasses;
using OBSUWP.Inferfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Media.Capture.Frames;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OBSUWP.Controls
{
    [ObservableObject]
    public sealed partial class SceneRenderer : UserControl
    {

        [ObservableProperty]
        private ObservableCollection<ISource> sources;

        public bool AreTransportControlsVisible { get; set; } = false;
        public bool IsMuted { get; set; } = true;

        // Get the singleton service
        private readonly ICamerahelperService camerahelperService = App.Current.Services.GetService<ICamerahelperService>();

        public SceneRenderer()
        {
            this.InitializeComponent();
        }

        partial void OnSourcesChanged(ObservableCollection<ISource> value)
        {
            DrawUI(); //TODO consider DrawUI(value)?
        }

        /// <summary>
        /// Remove old XAML and generate new
        /// </summary>
        public void DrawUI()
        {
            RemoveXAML();
            GenerateXAML();
        }

        /// <summary>
        /// Remove any XAML UI generated by previous rendered scene
        /// </summary>
        private void RemoveXAML()
        {
            myCanvas.Children.Clear();
        }

        /// <summary>
        /// Generates the XAML interface used by the template in the gridview
        /// </summary>
        /// <param name="scene"></param>
        private async void GenerateXAML()
        {

            // Adds source to the canvas using the apropriate XAML for the type
            foreach (ISource source in sources)
            {
                // Set the size of the clipping mask
                InitializeClippingMask();

                // Hide default content
                defaultText.Visibility = Visibility.Collapsed;

                // determine the type of ISource implementation
                switch (source)
                {
                    #region VideoSource
                    case VideoSource videoSource:
                        // Create playback element and set the source
                        MediaPlayerElement mediaPlayerElement = new MediaPlayerElement();
                        MediaSource _ms = MediaSource.CreateFromUri(new Uri((string)videoSource.Output));
                        mediaPlayerElement.Source = _ms;


                        // Settings for the playerelement layout and behaviour
                        ApplyScaling(mediaPlayerElement);
                        mediaPlayerElement.Stretch = Stretch.Uniform;

                        mediaPlayerElement.AutoPlay = true;
                        if (AreTransportControlsVisible)
                        {
                            mediaPlayerElement.AreTransportControlsEnabled = true;
                            mediaPlayerElement.TransportControls.IsCompact = true;
                        }
                        if (IsMuted)
                        {
                            mediaPlayerElement.MediaPlayer.IsMuted = true;
                        }

                        // Add the UIelement to the canvas
                        myCanvas.Children.Add(mediaPlayerElement);

                        break;
                    #endregion
                    #region LocalCameraSource
                    case LocalCameraSource localCameraSource:

                        // Get the sources camerahelper
                        var sourceCameraHelper = camerahelperService.GetCameraHelper((MediaFrameSourceGroup)localCameraSource.Output);

                        // Init the CameraPreview UI Control and subscribe to events
                        var camPreviewControl = new CameraPreview();
                        camPreviewControl.PreviewFailed += CamPreviewControl_PreviewFailed;
                        await camPreviewControl.StartAsync(sourceCameraHelper);
                        camPreviewControl.CameraHelper.FrameArrived += camPreviewControl_FrameArrived;

                        // Configure the UI element part of the CameraPreview
                        ApplyScaling(camPreviewControl);
                        camPreviewControl.IsFrameSourceGroupButtonVisible = false;

                        // Add control to XAML view
                        myCanvas.Children.Add(camPreviewControl);

                        break;
                    #endregion
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Helper method to set the scaling of the generated UI Controls
        /// </summary>
        /// <param name="control"></param>
        private void ApplyScaling(Control control)
        {
            if (double.IsNaN(this.Width))
            {
                // The live and preview windows don't have a set width/height
                control.Height = this.ActualHeight;
                control.Width = this.ActualWidth;
            }
            else
            {
                // Controls in gridview have a defined width/height
                control.Height = this.Height;
                control.Width = this.Width;
            }
            //control.HorizontalContentAlignment = HorizontalAlignment.Center;


        }

        #region helper methods for Local camera
        private void camPreviewControl_FrameArrived(object sender, Microsoft.Toolkit.Uwp.Helpers.FrameEventArgs e)
        {
            //var videoFrame = e.VideoFrame;
        }
        private void CamPreviewControl_PreviewFailed(object sender, PreviewFailedEventArgs e)
        {
            Debug.WriteLine("Local Camera preview failed\n", e);
        }
        #endregion

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
