using System;
using System.Collections.Generic;
using System.Linq;

namespace Crap4Net.Entities
{
    public class CrapStatsEntry
    {
        private readonly IList<CrapDataEntry> _crapMethodData;
        private readonly double _threshold;
        public double Threshold { get { return _threshold; } }

        public CrapStatsEntry(IList<CrapDataEntry> crapMethodData, double threshold)
        {
            _crapMethodData = crapMethodData;
            _threshold = threshold;
        }

        public double CalculateTotalCrap()
        {
            return _crapMethodData.Sum(x => x.Crap);
        }

        public int CalculateTotalMethods()
        {
            return _crapMethodData.Count;
        }

        public int CalculateTotalCrapMethods()
        {
            return _crapMethodData.Sum(x => x.Crap > _threshold ? 1 : 0);
        }

        public double CalculateCrapMethodPercentage()
        {
            return Convert.ToDouble(CalculateTotalCrapMethods()) / Convert.ToDouble(CalculateTotalMethods())*100;
        }

        public double CalculateTotalCrapLoad()
        {
            return _crapMethodData.Sum(x => 
                x.CalculateCrapLoad()); 
        }
    }
}