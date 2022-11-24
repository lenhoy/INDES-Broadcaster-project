using OBS_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBS_UWP.Classes
{
    internal class LocalVideoSource: ISource
    {
        internal string sourcePath;

        public LocalVideoSource(string path)
        {
            this.sourcePath = path;
        }

        public string getOutput()
        {
            return sourcePath;
        }
    }
}
