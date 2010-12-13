using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crap4Net.CoverageParsers;

namespace Crap4Net.AcceptanceTests
{
    [TestClass]
    public class TestingToolOnItself
    {
        [TestMethod]
        public void CreatinCrapReportforCrap4Netcode()
        {
            ReflectorParser ccParser = new ReflectorParser();
            //ccParser.LoadData(@"Crap4Net\Crap4NetReflectorCCReport.xml");
            //foreach (var item in ccParser.Data)
            //{
            //    Console.WriteLine("{0}.{1}:{2}",
            //        item.Method.TypeName,
            //        item.Method.MethodName,
            //        item.CyclomaticComplexity);
            //}

            VSCoverageParser covParser = new VSCoverageParser();
            //covParser.LoadData(@"Crap4Net\data.coverage");
            //foreach (var item in covParser.Data)
            //{
            //    Console.WriteLine("{0}.{1}:{2}",
            //        item.Method.TypeName,
            //        item.Method.MethodName,
            //        item.CoverageData);
            //}
            CrapAnalyzer.CCParser = ccParser;
            CrapAnalyzer.CoverageParser = covParser;
            var result = CrapAnalyzer.CreateCrapReport(
                @"AcceptnaceTestData\Crap4Net\data.coverage",
                @"AcceptnaceTestData\Crap4Net\Crap4NetReflectorCCReport.xml");
            foreach (var item in result)
            {
                Console.WriteLine(item.Method+":"+item.Crap);

            }
        }
    }
}
