using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Crap4NetTests
{
    [TestClass()]
    public class CrapCalculatorTest
    {

        #region TestContext
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void CalculateCrap_LegalValues_ReturnCorrectCrap()
        {
            //arrange
            var CCValue = 15;
            var CoverageValue = 65; //%

            //act
            var actual = CrapCalculator.CalculateCrap(CCValue, CoverageValue);

            //assert
            Assert.AreEqual(24.65d, actual);
        }

        [TestMethod]
        public void CalculateCrap_LegalValues_ReturnCorrectCrap2()
        {
            var CCValue = 10;
            var CoverageValue = 30; 

            var actual = CrapCalculator.CalculateCrap(CCValue, CoverageValue);

            Assert.AreEqual(44.3d, actual);
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateCrap_NegativeCoverageData_ThrowsException()
        {
            var ccValue = 5;
            var covarageValue = -1;

            CrapCalculator.CalculateCrap(ccValue, covarageValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateCrap_CoverageBiggerThen100_ThrowsExceptionSpecificSyntax()
        {
            var ccValue = 5;
            var covarageValue = 150;

            try
            {
                CrapCalculator.CalculateCrap(ccValue, covarageValue);
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, covarageValue.ToString());
                StringAssert.Contains(ex.Message, "Illegal Coverage Value");
                throw;

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateCrap_NegativeCCValue_ThrowsException()
        {
            var ccValue = -5;
            var covarageValue = 20;

            try
            {
                CrapCalculator.CalculateCrap(ccValue, covarageValue);
            }
            catch (ArgumentException ex)
            {
                var message = "CCValue cant be negative";
                Assert.AreEqual(message, ex.Message);
                throw;
            }
        }
    }
}
