using Crap4Net;
using System;
using NUnit.Framework;

namespace Crap4Net.Entitiestests
{
    [TestFixture]
    public class CCDataEntryTest
    {
        const string VALID_TYPE_NAME = "SomeClass";
        const string VALID_METHOD_NAME = "SomeMethod";

        [Test()]
        public void Equals_IdenticalValues_ReturnTrue()
        {
            CCDataEntry target1 = new CCDataEntry("SomeClass", "SomeMethod", 1);
            CCDataEntry target2 = new CCDataEntry("SomeClass", "SomeMethod", 1);
            Assert.IsTrue(target1.Equals(target2));
        }

        [Test()]
        public void Equals_DifferentTypeName_ReturnFalse()
        {
            CCDataEntry target1 = new CCDataEntry("SomeClass", VALID_METHOD_NAME, 1);
            CCDataEntry target2 = new CCDataEntry("SomeClass1", VALID_METHOD_NAME, 1);
            Assert.IsFalse(target1.Equals(target2));
        }

        [Test()]
        public void Equals_DifferentMethod_ReturnFalse()
        {
            CCDataEntry target1 = new CCDataEntry(VALID_TYPE_NAME, "SomeMethod", 1);
            CCDataEntry target2 = new CCDataEntry(VALID_TYPE_NAME, "SomeMethod1", 1);
            Assert.IsFalse(target1.Equals(target2));
        }

        [Test()]
        public void Equals_DifferentCCValue_ReturnFalse()
        {
            CCDataEntry target1 = new CCDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 1);
            CCDataEntry target2 = new CCDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 2);
            Assert.IsFalse(target1.Equals(target2));
        }

        [Test()]
        public void Equals_NullValue_ReturnFalse()
        {
            CCDataEntry target = new CCDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 1);
            bool actual = target.Equals(null);
            Assert.IsFalse(actual);
        }

        [Test()]
        public void ctor_LegalValue_InitializeFields()
        {
            CCDataEntry target = new CCDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 5);
            Assert.AreEqual(5, target.CyclomaticComplexity);
            Assert.AreEqual(VALID_TYPE_NAME, target.Method.TypeName);
            Assert.AreEqual(VALID_METHOD_NAME, target.Method.MethodName);

        }

        [Test()]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_NegativeCCValue_ThrowsException()
        {
            CCDataEntry target = new CCDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, -1);
        }
    }
}
