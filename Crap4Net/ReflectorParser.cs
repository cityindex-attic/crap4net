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
            if (fullMethodName.StartsWith("<>"))
            {
                return GetAnontmousTypeName(fullMethodName);
            }
            string[] parts = fullMethodName.Split(new char[] { '.' });
            if ((parts.Length==3) && (parts[1]!=String.Empty))
                return parts[1];
            if (parts.Length == 4)
                return parts[1];
            return parts[0];
        }

        private string GetAnontmousTypeName(string fullMethodName)
        {
            string[] parts = fullMethodName.Split(new char[] { '.' });
            return parts[0];
        }

        private string GetMethodName(XElement method)
        {
            string methodName = null;
            var fullMethodName = (string)method.Attribute("Name");
            if (fullMethodName.StartsWith("<>"))
            {
                fullMethodName = "Anonymous." + fullMethodName;
            }
            string[] parts = fullMethodName.Split(new char[] { '.' });
            if (IsCtor(fullMethodName))
                return ".ctor";
            if (IsCctor(fullMethodName))
                return ".cctor";
            methodName = GetMethodPart(parts);
            return CleanArguments(methodName);
        }

        private bool IsCtor(string fullName)
        {
            if (fullName.Contains(".ctor"))
            {
            	return true;
            }
            return false;
        }

        private bool IsCctor(string fullName)
        {
            if (fullName.Contains(".cctor"))
            {
                return true;
            }
            //else if (parts.Length == 3 && parts[2].StartsWith("cctor"))
            //{
            //    return true;
            //}
            return false;
        }

        private string CleanArguments(string methodName)
        {
            return methodName.Split(new char[] { '(' })[0];
        }

        private static string GetMethodPart(string[] parts)
        {
            if (parts.Length < 3)
                return parts[1];
            return parts[2];
        }
        private int GetCCValue(XElement method)
        {
            return (int)method.Attribute("CyclomaticComplexity");
        }
        #endregion
    }
}
