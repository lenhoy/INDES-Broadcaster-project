using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSUWP.Inferfaces
{
    public interface ISource
    {
        string GetOutput();
        string Output { get; }
    }
}
