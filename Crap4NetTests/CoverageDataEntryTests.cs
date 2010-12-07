
using System;
using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crap4NetTests
{
    [TestClass()]
    public class CoverageDataEntryTest
    {
        const string DONT_CARE = "Dont Care";
        const string VALID_TYPE_NAME = "SomeClass";
        const string VALID_METHOD_NAME = "SomeMethod";

        [TestMethod()]
        public void Equals_IdenticalValues_ReturnTrue()
        {
            var target1 = new CoverageDataEntry("SomeClass", "SomeMethod", 1);
            var target2 = new CoverageDataEntry("SomeClass", "SomeMethod", 1);
            Assert.IsTrue(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_DifferentTypeName_ReturnFalse()
        {
            CoverageDataEntry target1 = new CoverageDataEntry("SomeClass", VALID_METHOD_NAME, 1);
            CoverageDataEntry target2 = new CoverageDataEntry("SomeClass1", VALID_METHOD_NAME, 1);
            Assert.IsFalse(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_DifferentMethod_ReturnFalse()
        {
            CoverageDataEntry target1 = new CoverageDataEntry(VALID_TYPE_NAME, "SomeMethod", 1);
            CoverageDataEntry target2 = new CoverageDataEntry(VALID_TYPE_NAME, "SomeMethod1", 1);
            Assert.IsFalse(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_DifferentCCValue_ReturnFalse()
        {
            CoverageDataEntry target1 = new CoverageDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 1);
            CoverageDataEntry target2 = new CoverageDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 2);
            Assert.IsFalse(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_NullValue_ReturnFalse()
        {
            CoverageDataEntry target = new CoverageDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 1);
            bool actual = target.Equals(null);
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void ctor_LegalValue_InitializeFields()
        {
            CoverageDataEntry target = new CoverageDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 5);
            Assert.AreEqual(5, target.CoverageData);
            Assert.AreEqual(VALID_TYPE_NAME, target.Method.TypeName);
            Assert.AreEqual(VALID_METHOD_NAME, target.Method.MethodName);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_NegativeCCValue_ThrowsException()
        {
            CoverageDataEntry target = new CoverageDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, -1);
        }
    }
}
