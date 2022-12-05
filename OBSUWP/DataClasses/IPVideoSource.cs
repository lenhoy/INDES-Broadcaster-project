using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OBSUWP.DataClasses
{
    internal class IPVideoSource : ObservableObject, ISource
    {

        public string Type => this.GetType().Name;

        public object Output => throw new NotImplementedException();

        public string GetOutput()
        {
            throw new NotImplementedException();
        }
    }
}
