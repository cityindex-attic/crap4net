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

        /// <summary>
        /// Q: What is the CRAPload number?
        /// A: Crap load represents the "minimum" amount of work that could be done 
        /// to get below the crap threshold of 30 for a method. The summary Crapload 
        /// number is for the whole project and is just the sum of crap load for all methods. 
        /// The idea is that this gives you a floor value for the amount of work to get rid of crap. 
        /// Usually, the amount of work will be greater.
        ///
        /// Examining this in more detail, to lower the Crap score, there are two options: r
        /// educe cyclomatic complexity in a method, or write tests that cover 
        /// more of the paths in the method. In the case of the Crapload number, 
        /// we take the simplest possible restructuring of code, which is to 
        /// refactor it into methods that are half the size of the previous method, 
        /// say using the Extract Method refactoring. For example, I have a method 
        /// of complexity 60. if I refactor by extracting half the method into a new method, 
        /// then I would have two methods of complexity 30.
        ///
        /// Obviously, this blind refactoring is a pretty poor way to "fix" a system, and in reality, 
        /// some other, more appropriate, restructuring of the system would be the action to take. 
        /// In lieu of being able to determine that programmatically, we can at least compute t
        /// he minimum amount of work that could possibly be done to remove Crap from the system.
        /// 
        /// Crap load for a method is calculated as follows:
        ///
        /// public int getCrapLoad(float crapThreshold) {
        ///   int crapLoad = 0;
        ///   if (getCrap() >= crapThreshold) {
        ///     int complexity = getComplexity();
        ///     float coverage = getCoverage();
        ///     crapLoad += complexity * (1.0 - coverage);
        ///     crapLoad += complexity / crapThreshold;
        ///   }
        /// return crapLoad;
        /// }
        /// 
        /// So, interpreting that, if the CRAP score for a method is above the threshold, 15, 
        /// then for every point of uncovered complexity, add 1 for a test to cover that path. 
        /// Then for every bit of complexity over the threshold, figure out the number of 
        /// extract methods dividing in half that need to be done to get below the threshold.
        /// </summary>
        [Test]
        public void CrapLoadCalculatedCorrectly()
        {
            CrapDataEntry target1 = new CrapDataEntry("SomeClass", "SomeMethod", 40.51, 27, 9);
            Assert.AreEqual(9.2666666666666675d, target1.CalculateCrapLoad());
        }

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
