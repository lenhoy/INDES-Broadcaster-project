using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSUWP.DataClasses
{
    internal class LocalVideoSource : ObservableObject, ISource
    {
        public object Output => throw new NotImplementedException();

        public string Type => throw new NotImplementedException();

        public string GetOutput()
        {
            throw new NotImplementedException();
        }
    }
}
