using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crap4Net.CoverageParsers;

namespace Crap4Net
{


    public class CrapAnalyzer
    {

        public static ICCParser CCParser { get; set; }

        public static ICoverageParser CoverageParser { get; set; }

        public static IList<CrapDataEntry> CreateCrapReport(string coverageReport, string ccmReport)
        {
            LoadData(coverageReport, ccmReport);

            var results = new List<CrapDataEntry>();
            var combinedData = Join(CoverageParser.Data, CCParser.Data);

            foreach (var item in combinedData)
            {
                var crapValue = CrapCalculator.CalculateCrap(item.CyclomaticComplexity, item.CoverageData);
                var entry = new CrapDataEntry(item.Method.TypeName, 
                                item.Method.MethodName, 
                                crapValue,
                                item.CoverageData,
                                item.CyclomaticComplexity);
                results.Add(entry);
            }
            return results;
        }

        private static void LoadData(string coverageReport, string ccmReport)
        {
            if (null == CCParser)
            {
                throw new ApplicationException("CC Parser object not specified. Cant parse CC data without a parser.");
            }
            if (null == CoverageParser)
            {
                throw new ApplicationException("Coverage Parser object not specified. Cant parse Coverage data without a parser.");

            }
            CCParser.LoadData(ccmReport);
            CoverageParser.LoadData(coverageReport);
        }

        private static CCDataEntry LocateCCData(MethodSignature method, IList<CCDataEntry> ccData)
        {
            var answer = (from data in ccData
                          where data.Method.Equals(method)
                          select data);
            return answer.SingleOrDefault();
        }

        private static IList<CombinedDataEntry> Join(IList<CoverageDataEntry> coverageData, IList<CCDataEntry> ccData)
        {
            var results = new List<CombinedDataEntry>();
            foreach (var item in coverageData)
            {
                var ccEntry = LocateCCData(item.Method, ccData);
                if (null != ccEntry)
                {
                    var CombinedData = new CombinedDataEntry(item.Method, item.CoverageData, ccEntry.CyclomaticComplexity);
                    results.Add(CombinedData);
                }
                //else
                //{
                //    Console.WriteLine("Didnt Find MEthod: {0}.{1}", item.Method.TypeName, item.Method.MethodName);
                //}
            }
            return results;
        }

        
    }
}
