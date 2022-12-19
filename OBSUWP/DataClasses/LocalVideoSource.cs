using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using System;
using System.Runtime.CompilerServices;
using Windows.Media.Playback;

namespace OBSUWP.DataClasses
{
    internal partial class LocalVideoSource : ObservableObject, ISource
    {

        [ObservableProperty]
        private MediaPlayer sourceMediaPlayer;

        public object Output => this.SourceMediaPlayer;

        public string Type => typeof(LocalVideoSource).ToString();

        public LocalVideoSource(MediaPlayer mp)
        {
            this.sourceMediaPlayer = mp;
        }

        /// <summary>
        /// Returns the path to the local video file
        /// </summary>
        /// <returns></returns>
        public string GetOutput()
        {
            return this.SourceMediaPlayer.Source.ToString();
        }
    }
}
