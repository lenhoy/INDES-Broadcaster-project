using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.Helpers;
using OBSUWP.Inferfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture.Frames;

namespace OBSUWP.DataClasses
{
    internal class LocalCameraSource : ObservableObject, ISource
    {

        public string Type => this.GetType().Name;

        private readonly MediaFrameSourceGroup frameSourceGroup;
        /// <summary>
        /// The MediaFrameGroupSource used by the CameraHelperService to retrieve/create the 
        /// CameraHelper
        /// <returns><see cref="MediaFrameSourceGroup"/></returns>
        /// </summary>
        public object Output => frameSourceGroup;

        public LocalCameraSource(MediaFrameSourceGroup inputFrameSourceGroup)
        {
            frameSourceGroup = inputFrameSourceGroup;
        }


        public string GetOutput()
        {
            throw new NotImplementedException();
        }
    }
}
