using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace Crap4Net
{
    public class ReflectorParser : ICCParser
    {
        private IList<CCDataEntry> _data;

        IDictionary<string, List<string>> _knownMethods = new Dictionary<string,List<String>>();

        #region ICCParser Members
        public IList<CCDataEntry> Data
        {
            get
            {
                if (_data == null)
                    throw new ApplicationException("Data was not loaded");
                return _data;
            }
            private set
            {
                _data = value;
            }
        }

        public void LoadData(string reportFileName)
        {
            if (!File.Exists(reportFileName))
            {
                throw new ArgumentException("Reflector report file was not found: " + reportFileName);
            }
            Data = new List<CCDataEntry>();
            ParseDataFromXml(reportFileName);
        }

        private bool MethodIsNew(string typeName, string methodName)
        {
            List<string> methods;
            var foundType = _knownMethods.TryGetValue(typeName, out methods);
            if (foundType == false)
            {
                methods = new List<String>(){methodName};
                _knownMethods[typeName] = methods;
                return true;
            }
            if (!methods.Contains(methodName))
            {
                methods.Add(methodName);
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ParseDataFromXml(string reportFileName)
        {
            IEnumerable<XElement> results = LoadMethodsDataFromFile(reportFileName);
            foreach (var method in results)
            {
                string typeName = GetTypeNameWithoutNameSpace(method);
                string methodName = GetMethodName(method);
                int coverage = GetCCValue(method);
                var newEntry = new CCDataEntry(typeName, methodName, coverage);
                if (MethodIsNew(typeName,methodName))
                //if (!Data.Contains(newEntry))
                    Data.Add(newEntry);
            }
        }

        private IEnumerable<XElement> LoadMethodsDataFromFile(string reportFileName)
        {
            XDocument loaded = XDocument.Load(reportFileName);
            var result = (from metric in loaded.Descendants()
                          where metric.Name.LocalName == "Method"
                          select metric);
            return result;
        }

        private string GetTypeNameWithoutNameSpace(XElement method)
        {
            var fullMethodName = (string)method.Attribute("Name");
            return SignatureParser.GetClassName(fullMethodName);
        }


        private string GetMethodName(XElement method)
        {
            var fullMethodName = (string)method.Attribute("Name");
            return SignatureParser.GetMethodName(fullMethodName);
        }

        private int GetCCValue(XElement method)
        {
            return (int)method.Attribute("CyclomaticComplexity");
        }
        #endregion
    }
}
