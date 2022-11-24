using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Streaming.Adaptive;

namespace OBS_UWP.Interfaces
{
    /// <summary>
    /// Interface for all Sources that define methods and fields needed for implementation
    /// in the scenes. 
    /// </summary>
    public interface ISource
    {
        string getOutput();
    }
}
