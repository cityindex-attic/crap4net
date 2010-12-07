using System;
using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crap4NetTests
{

    /// <summary>
    /// Some Limitations of the VSTS Code Coverage:
    /// 1. It doesnt support Generic Methods - at least it doesnt give a proper signature data
    /// 2. Empty classes are just not ther - which baiscally is ok.
    /// </summary>


    [TestClass()]
    public class VSCoverageParserTests
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
        private VSCoverageParser _target;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _target = new VSCoverageParser();
            _target.LoadData(@"unitTestdata\VSCoverageFiles\DummyProject.coverage");
        }

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
            var target = new VSCoverageParser();
            var dummy = target.Data;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadData_InvalidFileName_throwsException()
        {
            var target = new VSCoverageParser();
            target.LoadData("NoSuchFile.xml");
        }

        [TestMethod]
        [Ignore]
        public void LoadData_EmptyClass_ParseOnlyCtor()
        {
            //Empty Classes are just not there
            //nothing to test...
        }

        [TestMethod]
        public void LoadData_CtorMethod_ParseCtor()
        {
            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".ctor", 100)));
        }

        [TestMethod]
        public void LoadData_GenericMethod_ParsedCorrectly()
        {

            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "GenericMethod", 0)));
        }

        [TestMethod]
        public void LoadData_FullCoverage_CoverageIs100()
        {
            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".ctor", 100)));
        }

        [TestMethod]
        public void LoadData_NoCoverage_ReturnsZeroCoverage()
        {
            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "staticMethod", 0)));
        }

        [TestMethod]
        public void LoadData_PartialCoverage_CalculateCorrectCoverage()
        {
            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "method1", 66)));
        }

        [TestMethod]
        public void LoadData_Property_returnResultforGetAndSet()
        {
            var actual = _target.Data;

            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "get_SomeProperty", 0)));
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "set_SomeProperty", 0)));
        }

        [TestMethod]
        public void LoadData_StaticCtor_ParsedCorrectly()
        {
            var actual = _target.Data;

            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".cctor", 100)));
        }

        [TestMethod]
        [Ignore()] //not yet anyway
        public void LoadData_OverloadedMethod_ParseAllInstances()
        {
            Assert.Inconclusive("not Supported yet - need to add method true signature");
            var target = new VSCoverageParser();
            target.LoadData("ctor.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".ctor", 100)));
        }

    }
}
