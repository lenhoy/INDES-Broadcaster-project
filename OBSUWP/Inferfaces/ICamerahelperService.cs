using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture.Frames;

namespace OBSUWP.Inferfaces
{
    internal interface ICamerahelperService
    {
        /// <summary>
        /// The single CameraHelper instance provides framearrived events the CameraPreview 
        /// Control can subscribe to.
        /// </summary>
        /// <returns>CameraHelper</returns>
        CameraHelper GetCameraHelper(MediaFrameSourceGroup frameSourceGroup);

    }
}
