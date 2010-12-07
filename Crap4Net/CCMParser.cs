using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace Crap4Net
{
    public class CCMParser : ICCParser
    {

        public CCMParser()
        {
            _data = null;
        }
        #region ICCParser Members

        IList<CCDataEntry> _data;
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

        public void LoadData(string reportFilename)
        {
            if (!File.Exists(reportFilename))
            {
                throw new ArgumentException("CMM result file was not found");
            }
            var loaded = XDocument.Load(reportFilename);

            var results = loaded.Descendants("metric");

            ParseData(results);
        }

        private static void ParseMethodAndTypeName(string method, out string typename, out string methodName)
        {
            string[] parts = method.Split(new string[]{"::"},StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length!=2)
                throw new ApplicationException("CCM bad format, couldnt parse method");
            typename = parts[0];
            if (parts[0] == parts[1])
            {
                methodName = ".ctor";
            }
            else
            {
                methodName = parts[1];
            }
        }
        private void ParseData(IEnumerable<XElement> results)
        {
            var data = from result in results
                       select new { Method = (string)result.Element("unit"), Complexity = (int)result.Element("complexity") };

            Data = new List<CCDataEntry>();
            foreach (var metric in data)
            {
                string typeName,methodName;
                ParseMethodAndTypeName(metric.Method,out typeName,out methodName);
                CCDataEntry newCCDataEntry = new CCDataEntry(typeName, methodName, metric.Complexity);
                if (!Data.Contains(newCCDataEntry))
                {
                    Data.Add(newCCDataEntry);
                }
            }
        }


        #endregion
    }
}
