using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace OBSUWP.DataClasses
{
    internal partial class OnlineStreamSource : ObservableValidator, ISource
    {
        [ObservableProperty]
        [Required]
        [Url]
        private string path;

        public OnlineStreamSource(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// <returns>Cast to <see cref="String"/></returns>
        /// </summary>
        public object Output => path;

        public string Type => this.GetType().Name;

        public string GetOutput()
        {
            return path;
        }

    }
}
