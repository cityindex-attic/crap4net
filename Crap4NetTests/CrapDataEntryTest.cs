using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Crap4NetTests
{

    [TestClass()]
    public class CrapDataEntryTest
    {
        const string DONT_CARE = "Dont Care";
        const string VALID_TYPE_NAME = "SomeClass";
        const string VALID_METHOD_NAME = "SomeMethod";

        [TestMethod()]
        public void Equals_IdenticalValues_ReturnTrue()
        {
            CrapDataEntry target1 = new CrapDataEntry("SomeClass", "SomeMethod", 1.54);
            CrapDataEntry target2 = new CrapDataEntry("SomeClass", "SomeMethod", 1.54);
            Assert.IsTrue(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_DifferentTypeName_ReturnFalse()
        {
            CrapDataEntry target1 = new CrapDataEntry("SomeClass", VALID_METHOD_NAME, 1);
            CrapDataEntry target2 = new CrapDataEntry("SomeClass1", VALID_METHOD_NAME, 1);
            Assert.IsFalse(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_DifferentMethod_ReturnFalse()
        {
            CrapDataEntry target1 = new CrapDataEntry(VALID_TYPE_NAME, "SomeMethod", 1);
            CrapDataEntry target2 = new CrapDataEntry(VALID_TYPE_NAME, "SomeMethod1", 1);
            Assert.IsFalse(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_DifferentCrapValue_ReturnFalse()
        {
            CrapDataEntry target1 = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 1);
            CrapDataEntry target2 = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 2);
            Assert.IsFalse(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_NullValue_ReturnFalse()
        {
            CrapDataEntry target = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 1);
            bool actual = target.Equals(null);
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void ctor_LegalValue_InitializeFields()
        {
            CrapDataEntry target = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 5.4);
            Assert.AreEqual(5.4, target.Crap);
            Assert.AreEqual(VALID_TYPE_NAME, target.Method.TypeName);
            Assert.AreEqual(VALID_METHOD_NAME, target.Method.MethodName);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_NegativeCCValue_ThrowsException()
        {
            CrapDataEntry target = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, -1);
        }
    }
}
