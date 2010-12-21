using System;
using NUnit.Framework;

namespace Crap4Net.Entitiestests
{

    [TestFixture]
    public class CrapDataEntryTest
    {
        const string DONT_CARE = "Dont Care";
        const string VALID_TYPE_NAME = "SomeClass";
        const string VALID_METHOD_NAME = "SomeMethod";

        [Test]
        public void Equals_IdenticalValues_ReturnTrue()
        {
            CrapDataEntry target1 = new CrapDataEntry("SomeClass", "SomeMethod", 1.54, 0, 0);
            CrapDataEntry target2 = new CrapDataEntry("SomeClass", "SomeMethod", 1.54, 0, 0);
            Assert.IsTrue(target1.Equals(target2));
        }

        [Test]
        public void Equals_DifferentTypeName_ReturnFalse()
        {
            CrapDataEntry target1 = new CrapDataEntry("SomeClass", VALID_METHOD_NAME, 1, 0, 0);
            CrapDataEntry target2 = new CrapDataEntry("SomeClass1", VALID_METHOD_NAME, 1, 0, 0);
            Assert.IsFalse(target1.Equals(target2));
        }

        [Test]
        public void Equals_DifferentMethod_ReturnFalse()
        {
            CrapDataEntry target1 = new CrapDataEntry(VALID_TYPE_NAME, "SomeMethod", 1, 0, 0);
            CrapDataEntry target2 = new CrapDataEntry(VALID_TYPE_NAME, "SomeMethod1", 1, 0, 0);
            Assert.IsFalse(target1.Equals(target2));
        }

        [Test]
        public void Equals_DifferentCrapValue_ReturnFalse()
        {
            CrapDataEntry target1 = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 1, 0, 0);
            CrapDataEntry target2 = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 2, 0, 0);
            Assert.IsFalse(target1.Equals(target2));
        }

        [Test]
        public void Equals_NullValue_ReturnFalse()
        {
            CrapDataEntry target = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 1, 0, 0);
            bool actual = target.Equals(null);
            Assert.IsFalse(actual);
        }

        [Test]
        public void ctor_LegalValue_InitializeFields()
        {
            CrapDataEntry target = new CrapDataEntry(VALID_TYPE_NAME, 
                                       VALID_METHOD_NAME, 
                                       5.4,12,1);
            Assert.AreEqual(5.4, target.Crap);
            Assert.AreEqual(VALID_TYPE_NAME, target.Method.TypeName);
            Assert.AreEqual(VALID_METHOD_NAME, target.Method.MethodName);

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_NegativeCrapValue_ThrowsException()
        {
            CrapDataEntry target = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, -1, 0, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_NegativeCoverageValue_ThrowsException()
        {
            CrapDataEntry target = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 5.4,-1,3);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_TooBigCoverageValue_ThrowsException()
        {
            CrapDataEntry target = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 5.4, 101, 3);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_NegativeCCValue_ThrowsException()
        {
            CrapDataEntry target = new CrapDataEntry(VALID_TYPE_NAME, VALID_METHOD_NAME, 5.4, 0, -1);
        }

    }
}
