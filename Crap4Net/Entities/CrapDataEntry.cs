using System;

namespace Crap4Net
{
    public class CrapDataEntry : IEquatable<CrapDataEntry>
    {
        public CrapDataEntry(string typeName, string methodName, double crapValue, int coverageValue,int ccValue)
        {
            if (ccValue < 0)
                throw new ArgumentException("ccValue should be a positive number, and not: "+ ccValue);
            
            if ((coverageValue < 0) ||(coverageValue > 100))
                throw new ArgumentException("coverageValue is a percenatge between 0-100 and not: " + coverageValue);

            if (crapValue < 0)
                throw new ArgumentException("crapValue should be a positive number and not: " + crapValue);

            Method = new MethodSignature(typeName, methodName);
            Crap = crapValue;
            Coverage = coverageValue;
            CC = ccValue;
        }

        public MethodSignature Method { get; private set; }
        public double Crap { get; set; }

        public int Coverage { get; set; }
        public int CC { get; set; }
        #region IEquatable<CCDataEntry> Members

        public bool Equals(CrapDataEntry other)
        {
            if (null==other)
            {
                return false; ;
            }
            return ( Method.Equals(other.Method) && Crap.Equals(other.Crap) );
        }

        #endregion

        public double CalculateCrapLoad()
        {
            double crapLoad = 0;
            if (Crap >= Constants.CrapThreshold)
            {
                crapLoad += CC * (1.0 - CoverageAsPercentage());
                crapLoad += CC / Constants.CrapThreshold;
            }
            return crapLoad;
        }

        private double CoverageAsPercentage()
        {
            var coverageAsPercentage = 1/Convert.ToDouble(Coverage);
            if (Double.IsPositiveInfinity(coverageAsPercentage)) coverageAsPercentage = 0;
            return coverageAsPercentage;
        }
    }
}
