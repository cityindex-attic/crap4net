using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using System;
using TypeMock.ArrangeActAssert;

namespace Crap4NetTests
{

    [TestClass()]
    [Isolated]
    public class CrapAnalyzerTest
    {
        [TestInitialize]
        public void Setup()
        {
            CrapAnalyzer.CCParser = null;
            CrapAnalyzer.CoverageParser = null;
        }

        [TestMethod]
        public void Join_GivenFullData_CreateProperCombination()
        {
            var fakeCCParser = Isolate.Fake.Instance<ICCParser>();
            IList<CCDataEntry> fakeCCData = new List<CCDataEntry>()
                    {   new CCDataEntry("MyClass","Method1",1),
                        new CCDataEntry("MyClass","Method2",1),
                        new CCDataEntry("MyClass","Method3",1)
                    };
            Isolate.WhenCalled(() => fakeCCParser.Data).WillReturn(fakeCCData);

            var fakeCoverageParser = Isolate.Fake.Instance<ICoverageParser>();
            IList<CoverageDataEntry> fakeCovData = new List<CoverageDataEntry>() 
                    {   new CoverageDataEntry("MyClass","Method1",30),
                        new CoverageDataEntry("MyClass","Method2",40),
                        new CoverageDataEntry("MyClass","Method3",50)
                    };
            Isolate.WhenCalled(() => fakeCoverageParser.Data).WillReturn(fakeCovData);
            
            CrapAnalyzer.CCParser = fakeCCParser;
            CrapAnalyzer.CoverageParser = fakeCoverageParser;
            
            var actual = CrapAnalyzer.CreateCrapReport("", "");

            Assert.AreEqual(3, actual.Count);
            Assert.IsTrue(actual.Contains(new CrapDataEntry("MyClass", "Method1", 1.34)));
            Assert.IsTrue(actual.Contains(new CrapDataEntry("MyClass", "Method2", 1.22)));
            Assert.IsTrue(actual.Contains(new CrapDataEntry("MyClass", "Method3", 1.12)));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CreateReport_CCParserNotGiven_ThrowsException()
        {
            var fake = Isolate.Fake.Instance<ICoverageParser>();
            CrapAnalyzer.CoverageParser = fake;
            CrapAnalyzer.CreateCrapReport("", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CreateReport_CoverageParserNotGiven_ThrowsException()
        {
            var fake = Isolate.Fake.Instance<ICCParser>();
            CrapAnalyzer.CCParser = fake;
            CrapAnalyzer.CreateCrapReport("", "");
        }

        [TestMethod]
        public void CreateReport_CovEntryNotInCC_IgnoreThatCovEntry()
        {
            var fakeCCParser = Isolate.Fake.Instance<ICCParser>();
            IList<CCDataEntry> fakeCCData = new List<CCDataEntry>()
                    {   new CCDataEntry("MyClass","Method1",1),
                        new CCDataEntry("MyClass","Method2",1),
                        new CCDataEntry("MyClass","Method3",1)
                    };
            Isolate.WhenCalled(() => fakeCCParser.Data).WillReturn(fakeCCData);

            var fakeCoverageParser = Isolate.Fake.Instance<ICoverageParser>();
            IList<CoverageDataEntry> fakeCovData = new List<CoverageDataEntry>() 
                    {   new CoverageDataEntry("NoSuchClass","Method1",30),
                    };
            Isolate.WhenCalled(() => fakeCoverageParser.Data).WillReturn(fakeCovData);

            CrapAnalyzer.CCParser = fakeCCParser;
            CrapAnalyzer.CoverageParser = fakeCoverageParser;

            var actual = CrapAnalyzer.CreateCrapReport("", "");
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void CreateReport_CCEntryNotInCov_IgnoreThatCovEntry()
        {
            var fakeCCParser = Isolate.Fake.Instance<ICCParser>();
            IList<CCDataEntry> fakeCCData = new List<CCDataEntry>()
                    {   new CCDataEntry("MyClass","Method1",1),
                        new CCDataEntry("MyClass","Method2",1),
                        new CCDataEntry("MyClass","Method3",1)
                    };
            Isolate.WhenCalled(() => fakeCCParser.Data).WillReturn(fakeCCData);

            var fakeCoverageParser = Isolate.Fake.Instance<ICoverageParser>();
            IList<CoverageDataEntry> fakeCovData = new List<CoverageDataEntry>() 
                    {   new CoverageDataEntry("NoSuchClass","Method1",30),
                    };
            Isolate.WhenCalled(() => fakeCoverageParser.Data).WillReturn(fakeCovData);

            CrapAnalyzer.CCParser = fakeCCParser;
            CrapAnalyzer.CoverageParser = fakeCoverageParser;

            var actual = CrapAnalyzer.CreateCrapReport("", "");
            Assert.AreEqual(0, actual.Count);

        }

        [TestMethod]
        public void CreateReport_CCEntryInCov_CalculateCrapForEntry()
        {

        }
        //need to test join for all kind of scenarios
        // 1) covergae include entry that doesnt exists in cc
        // 2) covergae doesnt include entry that exists in cc
        // 3) for entry that exists in both create the right combination

        
    }
}
