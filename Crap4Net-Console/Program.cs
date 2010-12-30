using System;
using System.Configuration;
using Crap4Net;
using Crap4Net.CoverageParsers;

namespace Crap4Net_Console
{
    class Program
    {

        static void Main(string[] args)
        {

            try
            {
                var CoverageTool = ConfigurationManager.AppSettings["CoverageTool"];
                var CCTool = ConfigurationManager.AppSettings["CCTool"];
                var coverageFile = ConfigurationManager.AppSettings["CoverageReport"];
                var ccFile = ConfigurationManager.AppSettings["CCReport"];
                var outputFile = ConfigurationManager.AppSettings["OutputFile"];
                var formatterName = ConfigurationManager.AppSettings["FormatterType"];

                var CCParser = CreateCCParser(CCTool);
                var coverageParser = CreateCoverageParser(CoverageTool);

                CrapAnalyzer.CCParser = CCParser;
                CrapAnalyzer.CoverageParser = coverageParser;


                //create parsers
                var Results = CrapAnalyzer.CreateCrapReport(coverageFile, ccFile);
                IFormatter formatter = FormatterFactory.GetFormatter(formatterName);
                formatter.FormatAndSaveToFile(Results, outputFile);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("An exception occured whilst creating the CRAP report.  Details:\n{0}", exception));
            }
            

        }
        private static ICCParser CreateCCParser(string ccTool)
        {
            switch (ccTool)
            {
                case "Reflector":
                    return new ReflectorParser();
                default:
                    throw new ArgumentException(String.Format("unsupported CC tool: {0}", ccTool));
            }

        }
        private static ICoverageParser CreateCoverageParser(string coverageTool)
        {
            switch (coverageTool)
            {
                case "MSTest":
                    return new VSCoverageParser();
                case "PartCover":
                    return new PartCoverageParser();
                default:
                    throw new ArgumentException(String.Format("unsupported Coverage tool: {0}", coverageTool));
            }
        }
    }
}
