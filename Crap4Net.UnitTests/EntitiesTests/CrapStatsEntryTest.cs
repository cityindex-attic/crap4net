using System;
using System.Collections.Generic;
using System.Text;
using Crap4Net.Entities;
using NUnit.Framework;

namespace Crap4Net.UnitTests.EntitiesTests
{
    [TestFixture]
    public class CrapStatsEntryTest
    {
        private List<CrapDataEntry> _crapMethodData = new List<CrapDataEntry>
                                           {
                                               new CrapDataEntry("type1", "method1", 20.11, 27, 14),
                                               new CrapDataEntry("type1", "method2", 20.1, 27, 14),
                                               new CrapDataEntry("type1", "method3", 2, 27, 14)
                                           };

        [Test]
        public void TotalCrapIsCalculatedAsTheSumOfCrapForEachMethod()
        {
            var crapSummary = new CrapStatsEntry(_crapMethodData, 1);
            Assert.AreEqual(42.21, crapSummary.CalculateTotalCrap());
        }

        [Test]
        public void MethodCountIsCalculatedAsTheSumOfMethods()
        {
            var crapSummary = new CrapStatsEntry(_crapMethodData, 2);
            Assert.AreEqual(3, crapSummary.CalculateTotalMethods());
        }

        [Test]
        public void CrapMethodCountIsCalculatedAsTheSumOfMethodsWithCrapScoreAboveThreshold()
        {
            var crapSummary = new CrapStatsEntry(_crapMethodData, 2);
            Assert.AreEqual(2, crapSummary.CalculateTotalCrapMethods());
        }
        [Test]
        public void CrapMethodPercentIsCalculatedAsTheNumCrapMethodsOverNumMethods()
        {
            var crapSummary = new CrapStatsEntry(_crapMethodData, 2);
            Assert.AreEqual(66.666666666666657d, crapSummary.CalculateCrapMethodPercentage());
        }

        [Test]
        public void TotalCrapLoadIsCalculatedAsTheSumOfCrapLoadForEachMethod()
        {
            var crapSummary = new CrapStatsEntry(_crapMethodData, 1);
            Assert.AreEqual(28.829629629629633d, crapSummary.CalculateTotalCrapLoad());
        }
    }
}