using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;

namespace Crap4Net.CoverageParsers
{
    public class PartCoverageParser : BaseCoverageParser 
    {
        protected override void ParseDataFromXml(string reportFileName)
        {
            IEnumerable<XElement> results = LoadTypesDataFromFile(reportFileName);
            foreach (var type in results)
            {
                string typeName = GetTypeNameWithoutNameSpace(type);
                foreach (var method in type.Elements("Method"))
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
            var results = loaded.Descendants("Type");
            return results;
        }

        private static string GetMethodName(XElement method)
        {
            if (IsGenericMethod(method))
            {
                return GetGenericMethodName(method);
            }
            else
            {
                return GetRegularMethodName(method);
            }
        }

        private static bool IsGenericMethod(XElement method)
        {
            var signature = (string)method.Attribute("sig");
            return signature.Contains('<');
        }

        private static string GetGenericMethodName(XElement method)
        {
            var methodName = (string)method.Attribute("name");
            var signature = (string)method.Attribute("sig");
            signature = signature.Trim(')');
            signature = signature.Trim('(');
            signature = signature.Trim();
            return methodName + signature;
        }

        private static string GetRegularMethodName(XElement method)
        {
            var methodName = (string)method.Attribute("name");
            return methodName;
        }

        private string GetTypeNameWithoutNameSpace(XElement type)
        {
            var typeName = (string)type.Attribute("name");
            typeName = RemoveNameSpace(typeName);
            return typeName;
        }

        private int GetCoverageValue(XElement method)
        {
            var actualVisits = ActualVisits(method);
            var total = TotalVisits(method);

            if (total != 0)
                return actualVisits * 100 / total;
            else
                return 0;
        }

        private string RemoveNameSpace(string name)
        {
            int index = name.IndexOf('.');
            return name.Remove(0, index + 1);
        }

        private int TotalVisits(XElement method)
        {
            return method.Elements("pt").Count();
        }

        private int ActualVisits(XElement method)
        {
            var result = (from visit in method.Elements("pt")
                          where (int)visit.Attribute("visit") > 0
                          select visit).Count();
            return result;
        }
    }

}
