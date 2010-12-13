using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crap4Net.CoverageParsers;

namespace Crap4NetTests
{


    [TestClass()]
    public class CRAPAcceptanceTests
    {

        #region TestContext

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
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

        #region First Acceptance Tests
        [TestMethod]
        public void CreateCrapReport_UsingCCMAndPartcover_CreatesCorrectReport()
        {
            CrapAnalyzer.CCParser = new CCMParser();
            CrapAnalyzer.CoverageParser = new PartCoverageParser();

            var actual = CrapAnalyzer.CreateCrapReport(@"AcceptnaceTestData\PartCoverResult.xml",
                @"AcceptnaceTestData\ccmReport.xml");

            Assert.AreEqual(3, actual.Count);
            Assert.IsTrue(actual.Contains(new CrapDataEntry("Class1", ".ctor", 1, 0, 0)));
            Assert.IsTrue(actual.Contains(new CrapDataEntry("Class1", "method1", 2.20, 0, 0)));
            Assert.IsTrue(actual.Contains(new CrapDataEntry("Class1", "method2", 2, 0, 0)));
        }

        [TestMethod]
        public void CreateCrapReport_UsingReflectorAndPartcover_CreatesCorrectReport()
        {
            CrapAnalyzer.CCParser = new ReflectorParser();
            CrapAnalyzer.CoverageParser = new PartCoverageParser();


            var actual = CrapAnalyzer.CreateCrapReport(@"AcceptnaceTestData\PartCoverResult.xml",
                @"AcceptnaceTestData\ReflectorCCReport.xml");

            Assert.AreEqual(3, actual.Count);
            Assert.IsTrue(actual.Contains(new CrapDataEntry("Class1", ".ctor", 1, 0, 0)));
            Assert.IsTrue(actual.Contains(new CrapDataEntry("Class1", "method1", 2.20, 0, 0)));
            Assert.IsTrue(actual.Contains(new CrapDataEntry("Class1", "method2", 2, 0, 0)));
        }
        #endregion

        #region DataDrivenTests
        [Ignore]
        public void CreateCrapReport_AcceptanceTests()
        {
            var ccmReport = (string)TestContext.DataRow["CCMReport"];
            var coverageReport = (string)TestContext.DataRow["coverageReport"];
            var crapReport = (string)TestContext.DataRow["ExpectedCrapReport"];

            var Expected = LoadResults(crapReport);


            var actual = CrapAnalyzer.CreateCrapReport(ccmReport, coverageReport);


            CollectionAssert.AreEquivalent(Expected as ICollection, actual as ICollection);
        }


        private static IDictionary<string, double> LoadResults(string crapReport)
        {
            XDocument data = XDocument.Load(crapReport);

            var results = new Dictionary<string, double>();
            var crapentries = from entry in data.Descendants("Result")
                              select new { Method = (string)entry.Element("Method"), CRAP = (float)entry.Element("CRAP") };

            foreach (var item in crapentries)
            {
                results[item.Method] = Math.Round(item.CRAP, 2);
            }
            return results;
        }
        #endregion


    }
}
