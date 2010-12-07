using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Crap4NetTests
{
    
    
    [TestClass()]
    public class CCMParserTests
    {
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
        [ExpectedException(typeof(ApplicationException))]
        public void Data_FileNotLoaded_ThrowsException()
        {
            var target = new CCMParser();
            var dummy = target.Data;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadData_InvalidFileName_throwsException()
        {
            var target = new CCMParser();
            target.LoadData("NoSuchFile.xml");
        }
        
        [TestMethod]
        public void Data_ValidFile_RetrievesCorrectData()
        {
            var target = new CCMParser();
            target.LoadData(@"UnitTestData\ccmReport.xml");
            var actual = target.Data;

            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1","method1",2)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1","method2",1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1",".ctor",1)));
            Assert.AreEqual(3, actual.Count);
        }
    }
}
