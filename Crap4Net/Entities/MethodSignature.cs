using System;

namespace Crap4Net
{
    public class MethodSignature : IEquatable<MethodSignature>
    {
        public MethodSignature(string typeName, string methodName)
        {
            if (String.IsNullOrEmpty(typeName))
                throw new ArgumentException("typeName is null or empty.", "typeName");
            if (String.IsNullOrEmpty(methodName))
                throw new ArgumentException("methodName is null or empty.", "methodName");
            TypeName = typeName;
            MethodName = methodName;
        }

        public string TypeName { get; private set; }
        public string MethodName { get; private set; }

        public override string ToString()
        {
            return String.Format("{0}.{1}", TypeName, MethodName);
        }
        
        #region IEquatable<MethodSignature> Members

        public bool Equals(MethodSignature other)
        {
            if (null == other)
            {
                return false;
            }
            return TypeName.Equals(other.TypeName) &&
                MethodName.Equals(other.MethodName);
        }
        #endregion
    }
}
