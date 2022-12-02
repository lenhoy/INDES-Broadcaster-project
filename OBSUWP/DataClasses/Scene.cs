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
    public class Scene : ObservableObject
    {
        public Scene() 
        {
            _sources = new ObservableCollection<ISource>();
        }
        public Scene(Collection<ISource> sources)
        {
            Sources = (ObservableCollection<ISource>) sources;
        }

        public Scene Self { get => this; }

        #region Sources
        // get the sources / set all soures at once
        private ObservableCollection<ISource> _sources;
        public ObservableCollection<ISource> Sources
        {
            get => _sources;
            set => SetProperty<ObservableCollection<ISource>>(ref _sources, value);
        }

        public ISource GetSource(int index)
        {
            return _sources[index];
        }

        public void AddSource(ISource source)
        {
            _sources.Add(source);
        }

        public void ChangeSource(int index, ISource newSource)
        {
            _sources[index] = newSource;
        }

        // TODO: is this needed?
        public ref ObservableCollection<ISource> GetSourcesByRef(){
            return ref _sources;
        }
        #endregion Sources
    }
}
