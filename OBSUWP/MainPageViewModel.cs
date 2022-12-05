using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.Helpers;
using OBSUWP.DataClasses;
using OBSUWP.Inferfaces;
using Windows.UI.Xaml.Controls;

namespace OBSUWP
{
    internal partial class MainPageViewModel: ObservableObject
    {
        // observable list of scenes
        public ObservableCollection<Scene> Scenes { get; set; } = new ObservableCollection<Scene>();

        // The selected Live Scene
        [ObservableProperty]
        private Scene liveScene;

        // The selected preview scene
        [ObservableProperty]
        private Scene previewScene;


        /// <summary>
        /// Construct the viewmodel
        /// </summary>
        public MainPageViewModel()
        {
            //initialize scenes
            InitializeScenes();

        }
        #region Commands

        // Create Scene

        // Delete Scene

        // Add Source
        [RelayCommand]
        private void AddSourceToPreviewScene(ISource source)
        {
            
        }

        // Remove Source
        [RelayCommand]
        private void RemoveSourceFromPreviewScene(ISource source)
        {
            previewScene.Sources.Remove(source);
        }

        #endregion

        public async void InitializeScenes()
        {
            Collection<ISource> sources = new ObservableCollection<ISource>();
            sources.Add(new VideoSource("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)"));
            Scene scene1 = new Scene(sources);
            this.Scenes.Add(scene1);

            Scene scene2 = new Scene();
            var availableFramSourceGroups = await CameraHelper.GetFrameSourceGroupsAsync();
            var inputFrameSourceGroup = availableFramSourceGroups.FirstOrDefault();
            scene2.AddSource(new LocalCameraSource(inputFrameSourceGroup));
            this.Scenes.Add(scene2);

            Scene scene3 = new Scene();
            inputFrameSourceGroup = availableFramSourceGroups.ToArray()[1];
            scene3.AddSource(new LocalCameraSource(inputFrameSourceGroup));
            this.Scenes.Add(scene3);
        }
    }
}
