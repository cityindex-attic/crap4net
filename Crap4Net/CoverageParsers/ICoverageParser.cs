using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crap4Net.CoverageParsers
{

    public interface ICoverageParser
    {
        void LoadData(string reportFileName);
        IList<CoverageDataEntry> Data { get; }
    }
}
