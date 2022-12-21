using Microsoft.Toolkit.Uwp.Helpers;
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
