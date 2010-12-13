using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crap4Net
{

    public interface ICCParser
    {
        void LoadData(string reportFilename);
        IList<CCDataEntry> Data { get; }
    }
}
