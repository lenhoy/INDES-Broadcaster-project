using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.DataClasses;
using OBSUWP.Inferfaces;

namespace OBSUWP
{
    internal class MainPageViewModel: ObservableObject
    {
        public MainPageViewModel()
        {
            //initialize scenes
            InitializeScenes();
        }
        // observable list of scenes
        public ObservableCollection<Scene> Scenes { get; set; } = new ObservableCollection<Scene>();

        public void InitializeScenes()
        {
            Collection<ISource> sources = new ObservableCollection<ISource>();
            sources.Add(new VideoSource("http://amssamples.streaming.mediaservices.windows.net/49b57c87-f5f3-48b3-ba22-c55cfdffa9cb/Sintel.ism/manifest(format=m3u8-aapl)"));
            Scene temp = new Scene(sources);
            this.Scenes.Add(temp);
            Scene temp2 = new Scene();
            temp2.AddSource(new VideoSource("C:\\Users\\lenna\\Downloads\\R22_Revolve_TO_EXPO.mp4"));
            this.Scenes.Add(temp2);
        }
    }
}
