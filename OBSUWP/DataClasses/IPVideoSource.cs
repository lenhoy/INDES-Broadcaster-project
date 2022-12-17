using CommunityToolkit.Mvvm.ComponentModel;
using OBSUWP.Inferfaces;
using System;

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
