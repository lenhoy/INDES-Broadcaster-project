using OBS_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBS_UWP.Classes
{
    public class Scene
    {
        public ObservableCollection<ISource> Sources { get; }

        public void AddSource(ISource source)
        {
            Sources.Add(source);
        }
    }
}
