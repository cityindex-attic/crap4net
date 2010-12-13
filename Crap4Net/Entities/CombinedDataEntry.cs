using System;

namespace Crap4Net
{
    public class CombinedDataEntry : IEquatable<CombinedDataEntry>
    {
        public MethodSignature Method 
        {
            get;
            private set;
        }
        public int CyclomaticComplexity
        {
            get;
            private set;
        }
        public int CoverageData
        {
            get;
            private set;
        }

        public CombinedDataEntry(MethodSignature method, int coverageData, int cyclomaticComplexity)
        {
            if (cyclomaticComplexity < 0)
                throw new ArgumentException("Cyclomatic Complexity value should be a positive number");
            if ((coverageData < 0) || (coverageData>100))
                throw new ArgumentException("Cyclomatic Complexity value should be a positive number");

            Method = method;
            CyclomaticComplexity = cyclomaticComplexity;
            CoverageData = coverageData;
        }

        #region IEquatable<CombinedDataEntry> Members

        public bool Equals(CombinedDataEntry other)
        {
            if (null==other)
            {
                return false;
            }
            return ( Method.Equals(other.Method) && 
                     CoverageData.Equals(other.CoverageData)  &&
                     CyclomaticComplexity.Equals(other.CyclomaticComplexity) );
        }

        #endregion
    }

}
