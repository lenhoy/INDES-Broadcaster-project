using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using System;
using System.Runtime.CompilerServices;
using Windows.Media.Playback;

namespace OBSUWP.DataClasses
{
    internal partial class LocalVideoSource : ObservableObject, ISource
    {
#nullable enable
        [ObservableProperty]
        private MediaPlayer? sourceMediaPlayer = null;

        public object? Output => this.SourceMediaPlayer;
#nullable disable

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
