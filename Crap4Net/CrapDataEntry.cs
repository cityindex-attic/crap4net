using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crap4Net
{
    public class CrapDataEntry : IEquatable<CrapDataEntry>
    {
        public CrapDataEntry(string typeName, string methodName, double value)
        {
            if (value < 0)
                throw new ArgumentException("value should be a positive number", "value");

            Method = new MethodSignature(typeName, methodName);
            Crap = value;
        }

        public MethodSignature Method { get; private set; }
        public double Crap { get; set; }

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
    }
}
