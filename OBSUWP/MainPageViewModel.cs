using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.Helpers;
using OBSUWP.Controls;
using OBSUWP.DataClasses;
using OBSUWP.Inferfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using static System.Net.Mime.MediaTypeNames;
using System.ServiceModel.Channels;
using Windows.UI.Popups;
using System.Data;
using Windows.Media.Capture.Frames;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Collections.Generic;

namespace OBSUWP
{
    enum SceneType
    {
        LocalFrontCamera,
        LocalBackCamera,
        IPCamera,
        LocalFile,
        OnlineStream
    }

    internal partial class MainPageViewModel : ObservableObject
    {
        // Static ViewModel to access from controls
        public static MainPageViewModel Current { get; private set; }

        // observable list of scenes
        public ObservableCollection<Scene> Scenes { get; set; } = new ObservableCollection<Scene>();

        // The selected Live Scene
        [ObservableProperty]
        private Scene liveScene;

        // The selected preview scene
        [ObservableProperty]
        private Scene previewScene;

        // PlayList Dictionary
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PlaylistScenes))]
        private Dictionary<Scene, int?> playlist = new();

        // Playlist list of scenes for ListView
        // Just for displaying in xaml
        public ObservableCollection<Scene> PlaylistScenes { get; } = new();

        // Is the playlist playing
        [ObservableProperty]
        private bool playlistActive;

        /// <summary>
        /// Construct the viewmodel
        /// </summary>
        public MainPageViewModel()
        {
            //initialize scenes
            InitializeScenes();
            Current = this;

        }
        #region Commands

        // Add Scene
        [RelayCommand] // TODO return task instead of void
        private async Task AddScene(SceneType type)
        {
            Scene scene = new Scene();
            switch (type)
            {
                case SceneType.LocalBackCamera:

                    // Set scene name
                    scene.Name = "New Back Camera Scene";

                    // Get framesourcegroups and add camera source
                    var availableFrameSourceGroups1 = await CameraHelper.GetFrameSourceGroupsAsync();
                    var inputFrameSourceGroup1 = availableFrameSourceGroups1.ToArray()[1];
                    scene.AddSource(new LocalCameraSource(inputFrameSourceGroup1));
                    break;

                case SceneType.LocalFrontCamera:

                    // Set scene name
                    scene.Name = "New Front Camera Scene";

                    // Get framesourcegroups and add camera source
                    var availableFrameSourceGroups2 = await CameraHelper.GetFrameSourceGroupsAsync();
                    var inputFrameSourceGroup2 = availableFrameSourceGroups2.ToArray()[0];
                    scene.AddSource(new LocalCameraSource(inputFrameSourceGroup2));
                    break;

                case SceneType.IPCamera:
                    
                    // Set scene name
                    scene.Name = "New IP Camera Scene";

                    Debug.Write("IPCamera not implemented");
                    break;

                case SceneType.LocalFile:

                    // Set scene name
                    scene.Name = "New Local File Scene";

                    // Create MediaPlayer for the LocalVideoSource
                    // Get a storage file
                    var openPicker = new FileOpenPicker();
                    openPicker.FileTypeFilter.Add(".mp4");
                    StorageFile file = await openPicker.PickSingleFileAsync();

                    if (file == null)
                    {
                        return;
                    }

                    var _mp = new MediaPlayer();
                    _mp.Source = MediaSource.CreateFromStorageFile(file);

                    // add new LocalVideoSource with the created MediaPlayer to the new scene
                    scene.AddSource(new LocalVideoSource(_mp));

                    break;

                case SceneType.OnlineStream:
                    try
                    {
                        // Set scene name
                        scene.Name = "New Online Stream Scene";

                        string inputURI = await ShowTextInputDialogAsync("Enter stream URI");
                        var _URICheck = new Uri(inputURI);
                        scene.AddSource(new OnlineStreamSource(inputURI));
                        
                    }
                    catch (Exception)
                    {
                        var dialog = new MessageDialog("URI probably wrong");
                        await dialog.ShowAsync();
                        return;
                    }
                    break;
                default:
                    break;
            }
            this.Scenes.Add(scene);
        }

        // Delete Scene
        [RelayCommand]
        private void RemoveScene(Scene scene)
        {

            if (scene == this.previewScene)
            {
                this.previewScene = null;
            }

            if (scene == this.liveScene)
            {
                this.liveScene = null;
            }


            this.Scenes.Remove(scene);
            
            // Dispose of the MediaPlayer so that it doesnt keep playing/existing in background
            switch (scene.Sources[0])
            {
                case LocalVideoSource lvs:
                    lvs.SourceMediaPlayer.Source = null;
                    break;

                case LocalCameraSource lcs:
                    // Don't need to implement any extra disposing here as it's only 2 
                    // camerahelpers managed by the camerahelper service
                    break;
                default:
                    break;
            }
        }

        // TODO: Add Scene to Playlist
        [RelayCommand]
        private async Task AddToPlaylist(Tuple<Scene,int?> input)
        {
            var scene = input.Item1;
            var time = input.Item2;

            try
            {
                Playlist.Add(scene, time);
                PlaylistScenes.Add(scene);
            }
            catch (ArgumentException)
            {
                Debug.WriteLine("Scene already is in Playlist");
                var dialog = new MessageDialog("Can't add Scene because it already is in Playlist");
                await dialog.ShowAsync();
                return; // exit
            }

        }
        // TODO: Remove Scene from playlist
        [RelayCommand]
        private void RemoveFromPlaylist(Scene scene)
        {

        }

        // TODO: Start Playlist

        // TODO: Stop Playlist

        // Add Source
        [RelayCommand]
        private void AddSourceToPreviewScene(ISource source)
        {

        }

        // Remove Source
        [RelayCommand]
        private void RemoveSourceFromPreviewScene(ISource source)
        {

            // remove the source from the version of the Scene in the gridView
            Scene gridScene = Scenes.Where(s => s.Equals(previewScene)).First();
            Scenes[Scenes.IndexOf(gridScene)].Sources.Remove(source);
            foreach (Scene scene in Scenes)
            {
                if (scene.Equals(previewScene))
                {
                    // get the sources and remove the argument source
                    scene.Sources.Remove(source);
                }
            }

            // Remove the source from the previewScene
            previewScene.Sources.Remove(source);           
        }

        #endregion



        #region helpers
        /// <summary>
        /// Dialog for getting text input from user
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static async Task<string> ShowTextInputDialogAsync(string title)
        {
            var inputTextBox = new TextBox { AcceptsReturn = false };
            (inputTextBox as FrameworkElement).VerticalAlignment = VerticalAlignment.Bottom;
            var dialog = new ContentDialog
            {
                Content = inputTextBox,
                Title = title,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel"
            };
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return inputTextBox.Text;
            else
                return "";
        }
        #endregion


        public void InitializeScenes()
        {
            Collection<ISource> sources = new ObservableCollection<ISource>();
            sources.Add(new OnlineStreamSource("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)"));
            Scene scene1 = new Scene(sources);
            scene1.Name = "Online Stream Sceneiuhsefuhseifhiuesfiuesfiuhseifhesifhiehfiuesifhisefiuesfiuesifhseifhsiefhisefiuesf";
            this.Scenes.Add(scene1);

            /*
            Scene scene2 = new Scene();
            var availableFrameSourceGroups = await CameraHelper.GetFrameSourceGroupsAsync();
            var inputFrameSourceGroup = availableFrameSourceGroups.FirstOrDefault();
            scene2.AddSource(new LocalCameraSource(inputFrameSourceGroup));
            this.Scenes.Add(scene2);

            Scene scene3 = new Scene();
            inputFrameSourceGroup = availableFrameSourceGroups.ToArray()[1];
            scene3.AddSource(new LocalCameraSource(inputFrameSourceGroup));
            this.Scenes.Add(scene3);
            */
        }
    }
}
