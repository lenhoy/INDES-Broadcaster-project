using Microsoft.Toolkit.Uwp.Helpers;
using OBSUWP.Inferfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture.Frames;

namespace OBSUWP.Services
{
    public sealed class CameraHelperService : ICamerahelperService
    {

        private List<CameraHelper> cameraHelpers = new List<CameraHelper>();


        /// <summary>
        /// Gets instance of CameraHelper for the given MediaFrameSourceGroup
        /// Creates new one if none are found
        /// </summary>
        /// <param name="frameSourceGroup"></param>
        /// <returns></returns>
        public CameraHelper GetCameraHelper(MediaFrameSourceGroup frameSourceGroup)
        {
            if (cameraHelpers != null)
            {
                // Look for existing CameraHelper given the frameSourceGroup
                foreach (CameraHelper cameraHelper in cameraHelpers)
                {
                    if (cameraHelper.FrameSourceGroup == frameSourceGroup)
                    {
                        return cameraHelper;
                    }
                }
            }

            // Create new cameraHelper if none are set or could find one for the given framSourceGroup 
            cameraHelpers.Add(new CameraHelper() { FrameSourceGroup = frameSourceGroup });
            return cameraHelpers[cameraHelpers.Count - 1]; // return the just added CameraHelper

        }
    }
}
