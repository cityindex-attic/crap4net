﻿using System;
using Crap4Net;
using Crap4Net.CoverageParsers;
using NUnit.Framework;

namespace Crap4NetTests
{
    [TestFixture]
    public class CoberturaParserTest
    {
        private const string PARTCOVER_FILEPATH = @"UnitTestData\PartCoverFiles\";

        CoberturaParser _target;

        [SetUp]
        public void Setup()
        {
            _target = new CoberturaParser();
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void Data_FileNotLoaded_ThrowsException()
        {
            var target = new CoberturaParser();
            var dummy = target.Data;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadData_InvalidFileName_throwsException()
        {
            var target = new VSCoverageParser();
            target.LoadData("NoSuchFile.xml");
        }

        [Test]
        [Ignore]
        public void LoadData_EmptyClass_ParseOnlyCtor()
        {
            //Empty Classes are just not there
            //nothing to test...
        }

        [Test]
        [Ignore]
        public void LoadData_CtorMethod_ParseCtor()
        {
            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".ctor", 100)));
        }

        [Test]
        [Ignore]
        public void LoadData_GenericMethod_ParsedCorrectly()
        {

            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "GenericMethod", 0)));
        }

        [Test]
        [Ignore]
        public void LoadData_FullCoverage_CoverageIs100()
        {
            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".ctor", 100)));
        }

        [Test]
        [Ignore]
        public void LoadData_NoCoverage_ReturnsZeroCoverage()
        {
            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "staticMethod", 0)));
        }

        [Test]
        [Ignore]
        public void LoadData_PartialCoverage_CalculateCorrectCoverage()
        {
            var actual = _target.Data;
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "method1", 66)));
        }

        [Test]
        [Ignore]
        public void LoadData_Property_returnResultforGetAndSet()
        {
            var actual = _target.Data;

            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "get_SomeProperty", 0)));
            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", "set_SomeProperty", 0)));
        }

        [Test]
        [Ignore]
        public void LoadData_StaticCtor_ParsedCorrectly()
        {
            var actual = _target.Data;

            Assert.IsTrue(actual.Contains(new CoverageDataEntry("Class1", ".cctor", 100)));
        }

        [Test]
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
