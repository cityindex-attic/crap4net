using System;
using Crap4Net.CoverageParsers;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Crap4Net.CoverageParsers
{
    public class CoberturaParser : BaseCoverageParser
    {

        protected override void ParseDataFromXml(string reportFileName)
        {
            IEnumerable<XElement> results = LoadTypesDataFromFile(reportFileName);
            foreach (var type in results)
            {
                string typeName = GetTypeNameWithoutNameSpace(type);
                foreach (var method in type.Descendants("method"))
                {
                    string methodName = GetMethodName(method);
                    int coverage = GetCoverageValue(method);
                    Data.Add(new CoverageDataEntry(typeName, methodName, coverage));
                }
            }
        }

        private static IEnumerable<XElement> LoadTypesDataFromFile(string reportFileName)
        {
            XDocument loaded = XDocument.Load(reportFileName);
            var results = loaded.Descendants("class");
            return results;
        }

        private string GetTypeNameWithoutNameSpace(XElement type)
        {
            var typeName = (string)type.Attribute("name");
            typeName = RemoveNameSpace(typeName);
            return typeName;
        }

        private string RemoveNameSpace(string name)
        {
            int index = name.LastIndexOf('.');
            return name.Remove(0, index + 1);
        }

        private string GetMethodName(XElement method)
        {
            return method.Attribute("name").Value;
        }

        private int GetCoverageValue(XElement method)
        {
            var coverageString = method.Attribute("line-rate").Value;
            return (int)(Double.Parse(coverageString) * 100);
            
        }

    }
}
