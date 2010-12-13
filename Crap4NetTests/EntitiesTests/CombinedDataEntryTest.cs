using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Crap4NetTests
{
    
    
    [TestClass()]
    public class CombinedDataEntryTest
    {
        const string DONT_CARE = "Dont Care";
        const string VALID_TYPE_NAME = "SomeClass";
        const string VALID_METHOD_NAME = "SomeMethod";

        private MethodSignature _Method = new MethodSignature(VALID_TYPE_NAME, VALID_METHOD_NAME);

        [TestMethod()]
        public void Equals_IdenticalValues_ReturnTrue()
        {
            var target1 = new CombinedDataEntry(_Method, 1, 5);
            var target2 = new CombinedDataEntry(_Method, 1, 5);
            Assert.IsTrue(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_DifferentMethod_ReturnFalse()
        {
            var otherMethod = new MethodSignature(VALID_TYPE_NAME, VALID_METHOD_NAME+"a");
            var target1 = new CombinedDataEntry(_Method, 1, 5);
            var target2 = new CombinedDataEntry(otherMethod, 1, 5);
            Assert.IsFalse(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_DifferentCCValue_ReturnFalse()
        {
            CombinedDataEntry target1 = new CombinedDataEntry(_Method, 5, 1);
            CombinedDataEntry target2 = new CombinedDataEntry(_Method, 5, 2);
            Assert.IsFalse(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_DifferentCoverageValue_ReturnFalse()
        {
            CombinedDataEntry target1 = new CombinedDataEntry(_Method, 1, 0);
            CombinedDataEntry target2 = new CombinedDataEntry(_Method, 2, 0);
            Assert.IsFalse(target1.Equals(target2));
        }

        [TestMethod()]
        public void Equals_NullValue_ReturnFalse()
        {
            CombinedDataEntry target = new CombinedDataEntry(_Method, 1,5);
            bool actual = target.Equals(null);
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void ctor_LegalValue_InitializeFields()
        {
            CombinedDataEntry target = new CombinedDataEntry(_Method, 1, 2);
            Assert.AreEqual(1, target.CoverageData);
            Assert.AreEqual(2, target.CyclomaticComplexity);
            Assert.AreEqual(_Method, target.Method);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_NegativeCCValue_ThrowsException()
        {
            CombinedDataEntry target = new CombinedDataEntry(_Method, 5, -1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_NegativeCoverageValue_ThrowsException()
        {
            CombinedDataEntry target = new CombinedDataEntry(_Method, -1, 5);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_TooBigCoverageValue_ThrowsException()
        {
            CombinedDataEntry target = new CombinedDataEntry(_Method, -1, 200);
        }
    }
}
