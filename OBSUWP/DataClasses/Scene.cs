﻿using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OBSUWP.DataClasses
{
    public partial class Scene : ObservableObject
    {
        #region Properties
        public Scene Self { get => this; }
        
        [NotifyPropertyChangedFor(nameof(Self))] // TODO find out how to make sure scene if deleted
        [ObservableProperty]
        private ObservableCollection<ISource> sources;

        public string Name { get; private set; }
        #endregion

        #region Constructors
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

        #region Helpers
        /// <summary>
        /// Sets the name simply by current number of scenes
        /// </summary>
        private void SetName()
        {
            this.Name = "Scene " + MainPageViewModel.Current.Scenes.Count.ToString();
        }
        #endregion
    }
}
