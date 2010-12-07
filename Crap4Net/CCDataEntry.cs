using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crap4Net
{
    public class CCDataEntry : IEquatable<CCDataEntry>
    {
        public CCDataEntry(string typeName, string methodName, int value)
        {
            if (value < 0)
                throw new ArgumentException("value should be a positive number", "value");

            Method = new MethodSignature(typeName, methodName);
            CyclomaticComplexity = value;
        }

        public MethodSignature Method { get; private set; }
        public int CyclomaticComplexity { get; set; }

        #region IEquatable<CCDataEntry> Members

        public bool Equals(CCDataEntry other)
        {
            if (null==other)
            {
                return false; ;
            }
            return ( Method.Equals(other.Method) && CyclomaticComplexity.Equals(other.CyclomaticComplexity) );
        }

        #endregion
    }
}
