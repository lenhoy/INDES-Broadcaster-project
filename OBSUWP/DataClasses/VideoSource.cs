using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace OBSUWP.DataClasses
{
    internal partial class VideoSource : ObservableValidator, ISource
    {
        [ObservableProperty]
        [Required]
        [Url]
        private string path;

        public VideoSource(string path)
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
