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
    internal class VideoSource : ObservableObject, ISource
    {
        private string _path;
        [Required]
        [Url]
        public string Path
        {
            get { return _path; }
            set { SetProperty<string>(ref _path, value); }
        }

        public VideoSource(string path)
        {
            this._path = path;
        }

        public string Output {get => _path;} 

        public string GetOutput()
        {
            return _path;
        }
    }
}
