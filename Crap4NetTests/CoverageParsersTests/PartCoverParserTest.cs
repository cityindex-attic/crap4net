using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.IO;
using System.Collections;
using Crap4Net.CoverageParsers;

namespace Crap4NetTests
{


    [TestClass()]
    public class PartCoverParserTests
    {
        private const string PARTCOVER_FILEPATH = @"UnitTestData\PartCoverFiles\";

        [TestMethod()]
        [ExpectedException(typeof(ApplicationException))]
        public void Data_FileNotLoaded_ThrowsException()
        {
            var target = new PartCoverageParser();
            var dummy = target.Data;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadData_InvalidFileName_throwsException()
        {
            var target = new PartCoverageParser();
            target.LoadData("NoSuchFile.xml");
        }

        [TestMethod]
        public void LoadData_EmptyClass_ParseOnlyCtor()
        {
            var target = new PartCoverageParser();
            target.LoadData(PARTCOVER_FILEPATH+"EmptyClass.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("class2", ".ctor", 0)));
        }

        [TestMethod]
        public void LoadData_CtorMethod_ParseCtor()
        {
            var target = new PartCoverageParser();
            target.LoadData(PARTCOVER_FILEPATH+"ctor.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".ctor", 100)));
        }

        [TestMethod]
        public void LoadData_GenericMethod_ParsedCorrectly()
        {
            var target = new PartCoverageParser();
            target.LoadData(PARTCOVER_FILEPATH + "GenericMethod.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "GenericMethod<1>", 100)));
        }

        [TestMethod]
        public void LoadData_FullCoverage_CoverageIs100()
        {
            var target = new PartCoverageParser();
            target.LoadData(PARTCOVER_FILEPATH + "MethodWithFullCoverage.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".ctor", 100)));
        }

        [TestMethod]
        public void LoadData_MethodWithMoreThenOneVisit_calculateCoverageProperly()
        {
            var target = new PartCoverageParser();
            target.LoadData(PARTCOVER_FILEPATH + "MethodWithMoreThenOneVisit.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "method1", 90)));
        }
        
        [TestMethod]
        public void LoadData_NoCoverage_ReturnsZeroCoverage()
        {
            var target = new PartCoverageParser();
            target.LoadData(PARTCOVER_FILEPATH + "MethodWithNoCoverage.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("class2", ".ctor", 0)));
        }

        [TestMethod]
        public void LoadData_PartialCoverage_CalculateCorrectCoverage()
        {
            var target = new PartCoverageParser();
            target.LoadData(@PARTCOVER_FILEPATH + "MethodWithPartialCoverage.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "method1", 63)));
        }

        [TestMethod]
        public void LoadData_Property_returnResultforGetAndSet()
        {
            var target = new PartCoverageParser();
            target.LoadData(PARTCOVER_FILEPATH + "Property.xml");
            var actual = target.Data;

            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "get_SomeProperty", 0)));
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "set_SomeProperty", 0)));
        }

        [TestMethod]
        public void LoadData_StaticCtor_ParsedCorrectly()
        {
            var target = new PartCoverageParser();
            target.LoadData(PARTCOVER_FILEPATH + "StaticCtor.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".cctor", 100)));
        }

        [TestMethod]
        [Ignore]
        public void LoadData_OverloadedMethod_ParseAllInstances()
        {
            Assert.Inconclusive("not Supported yet - need to add method true signature");
            var target = new PartCoverageParser();
            target.LoadData("ctor.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".ctor", 100)));
        }

        [TestMethod]
        public void Data_ValidFile_RetrievesCorrectData()
        {
            var target = new PartCoverageParser();
            target.LoadData(PARTCOVER_FILEPATH + "PartCoverResult.xml");
            var actual = target.Data;

            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "method1", 63)));
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "method2", 0)));
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".ctor", 100)));
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1Test", "method1Test", 100)));
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1Test", ".ctor", 100)));
            Assert.AreEqual(5, actual.Count);
        }
    }
}
