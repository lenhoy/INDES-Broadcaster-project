using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Windows.Storage.Pickers.Provider;

namespace OBSUWP.DataClasses
{
    public partial class Scene : ObservableObject
    {
        public Scene Self { get => this; }
        
        [NotifyPropertyChangedFor(nameof(Self))] // TODO find out how to make sure scene if deleted
        [ObservableProperty]
        private ObservableCollection<ISource> sources;

        public string Name { get; set; }


        #region contructors

        public Scene()
        {
            sources = new ObservableCollection<ISource>();
            SetName();
        }
        
        public Scene(Collection<ISource> sources)
        {
            Sources = (ObservableCollection<ISource>)sources;
            SetName();
        }

        #endregion


        #region Sources
        public void AddSource(ISource source)
        {
            sources.Add(source);
        }

        public ISource GetSource(int index)
        {
            return sources[index];
        }

        // TODO: is this needed?
        public ref ObservableCollection<ISource> GetSourcesByRef()
        {
            return ref sources;
        }
        #endregion Sources

        private void SetName()
        {
            this.Name = "New Scene";
        }
    }
}
