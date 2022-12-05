using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using Windows.Graphics.Imaging;

namespace OBSUWP.DataClasses
{
    public partial class Scene : ObservableObject
    {
        public Scene() 
        {
            sources = new ObservableCollection<ISource>();
        }
        public Scene(Collection<ISource> sources)
        {
            Sources = (ObservableCollection<ISource>) sources;
        }

        public Scene Self { get => this; }

        #region Sources
        // get the sources / set all soures at once
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Self))] // TODO find out how to make sure scene if deleted
        private ObservableCollection<ISource> sources;

        public ISource GetSource(int index)
        {
            return sources[index];
        }

        public void AddSource(ISource source)
        {
            sources.Add(source);
        }

        public void ChangeSource(int index, ISource newSource)
        {
            sources[index] = newSource;
        }

        // TODO: is this needed?
        public ref ObservableCollection<ISource> GetSourcesByRef(){
            return ref sources;
        }
        #endregion Sources
    }
}
