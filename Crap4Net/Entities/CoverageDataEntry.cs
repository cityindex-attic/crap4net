using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crap4Net
{
    public class CoverageDataEntry :IEquatable<CoverageDataEntry>
    {
        public MethodSignature Method { get; private set; }

        public int CoverageData { get; private set; }

        public CoverageDataEntry(string typeName, string methodName, int value)
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("value should be in percenatges.", "value");
            
            CoverageData = value;
            Method = new MethodSignature(typeName, methodName);
            
        }

        #region IEquatable<CoverageDataEntry> Members

        public bool Equals(CoverageDataEntry other)
        {
            if (null==other)
            {
                return false;
            }
            return ( other.Method.Equals(Method) && CoverageData.Equals(other.CoverageData) );
        }

        #endregion
    }
}
