using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Crap4Net.Entities;

namespace Crap4Net.Formatters
{
    class Crap4JXmlFormatter : IFormatter
    {
        #region IFormatter Members

        public XmlDocument FormatReport(IList<CrapDataEntry> data)
        {
            XmlDocument xmlDoc = new XmlDocument();
            AppendDecleration(xmlDoc);
            XmlElement root = CreateRootElement(xmlDoc);

            AppendHeadersInfoElement(xmlDoc,root);

            var stats = CreateStats(xmlDoc, new CrapStatsEntry(data, Constants.CrapThreshold));
            root.AppendChild(stats);
            var methods = CreateMethods(xmlDoc,data);
            root.AppendChild(methods);
            xmlDoc.AppendChild(root);
            return xmlDoc;
        }

        public void FormatAndSaveToFile(IList<CrapDataEntry> data, string fileName)
        {
            var xmlDoc = FormatReport(data);
            XmlTextWriter writer =
                new XmlTextWriter(fileName, new UTF8Encoding(false));
            xmlDoc.Save(writer);
            writer.Close();
        }

        #endregion

        private static XmlDeclaration AppendDecleration(XmlDocument xmlDoc)
        {
            // Write down the XML declaration
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
            return xmlDeclaration;
        }

        private XmlElement CreateRootElement(XmlDocument xmlDoc)
        {
            var root = xmlDoc.CreateElement("crap_result");
            return root;
        }

        private void AppendHeadersInfoElement(XmlDocument xmlDoc, XmlElement root)
        {
            XmlElement projectNode = xmlDoc.CreateElement("project");
            var value = xmlDoc.CreateTextNode(@"C:\");
            projectNode.AppendChild(value);
            root.AppendChild(projectNode);

            XmlElement projectID = xmlDoc.CreateElement("project_id");
            value = xmlDoc.CreateTextNode("1290006074016");
            projectID.AppendChild(value);
            root.AppendChild(projectID);

            XmlElement timeStamp = xmlDoc.CreateElement("timestamp");
            value = xmlDoc.CreateTextNode("1/1/00 00:01 AM");
            timeStamp.AppendChild(value);
            root.AppendChild(timeStamp);

            XmlElement classDirectories = xmlDoc.CreateElement("classDirectories");
            value = xmlDoc.CreateTextNode(@"C:\");
            classDirectories.AppendChild(value);
            root.AppendChild(classDirectories);

            XmlElement testClassDirectories = xmlDoc.CreateElement("testClassDirectories");
            value = xmlDoc.CreateTextNode(@"C:\");
            testClassDirectories.AppendChild(value);
            root.AppendChild(testClassDirectories);

            XmlElement sourceDirectories = xmlDoc.CreateElement("sourceDirectories");
            value = xmlDoc.CreateTextNode(@"C:\");
            sourceDirectories.AppendChild(value);
            root.AppendChild(sourceDirectories);

            XmlElement libClassPaths = xmlDoc.CreateElement("libClasspaths");
            value = xmlDoc.CreateTextNode(@"C:\");
            libClassPaths.AppendChild(value);
            root.AppendChild(libClassPaths);
        }

        private XmlElement CreateStats(XmlDocument xmlDoc, CrapStatsEntry crapStatsEntry)
        {
            XmlElement stats = xmlDoc.CreateElement("stats");

            XmlElement elem = xmlDoc.CreateElement("name");
            var value = xmlDoc.CreateTextNode("name");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("totalCrap");
            value = xmlDoc.CreateTextNode(crapStatsEntry.CalculateTotalCrap().ToString("0.0"));
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("crap");
            value = xmlDoc.CreateTextNode("0.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("median");
            value = xmlDoc.CreateTextNode("0.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("average");
            value = xmlDoc.CreateTextNode("0.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("stdDev");
            value = xmlDoc.CreateTextNode("0.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("methodCount");
            value = xmlDoc.CreateTextNode(crapStatsEntry.CalculateTotalMethods().ToString());
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("crapMethodCount");
            value = xmlDoc.CreateTextNode(crapStatsEntry.CalculateTotalCrapMethods().ToString());
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("crapMethodPercent");
            value = xmlDoc.CreateTextNode(crapStatsEntry.CalculateCrapMethodPercentage().ToString("0.0"));
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("crapLoad");
            value = xmlDoc.CreateTextNode(crapStatsEntry.CalculateTotalCrapLoad().ToString("0"));
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("crapThreshold");
            value = xmlDoc.CreateTextNode(crapStatsEntry.Threshold.ToString());
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("globalAverage");
            value = xmlDoc.CreateTextNode("-1.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("globalCraploadAverage");
            value = xmlDoc.CreateTextNode("-1.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("globalCrapMethodAverage");
            value = xmlDoc.CreateTextNode("-1.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("globalTotalMethodAverage");
            value = xmlDoc.CreateTextNode("-1.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("globalAverageDiff");
            value = xmlDoc.CreateTextNode("0.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("globalCraploadAverageDiff");
            value = xmlDoc.CreateTextNode("0.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("globalCrapMethodAverageDiff");
            value = xmlDoc.CreateTextNode("0.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("globalTotalMethodAverageDiff");
            value = xmlDoc.CreateTextNode("0.0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = xmlDoc.CreateElement("shareStatsUrl");
            value = xmlDoc.CreateTextNode("http://www.crap4j.org/benchmark/stats/new?stat[project_hash]=1290006074016&amp;stat[project_url]=test&amp;stat[crap]=50.00&amp;stat[crap_load]=5&amp;stat[crap_methods]=1&amp;stat[total_methods]=2&amp;stat[ones]=0&amp;stat[twos]=1&amp;stat[fours]=0&amp;stat[eights]=0&amp;stat[sixteens]=1&amp;stat[thirtytwos]=0&amp;stat[sixtyfours]=0&amp;stat[one28s]=0&amp;stat[two56s]=0");
            elem.AppendChild(value);
            stats.AppendChild(elem);

            elem = CreateHistogramElemnt(xmlDoc);
            stats.AppendChild(elem);

            return stats;
        }

        private static XmlElement CreateHistogramElemnt(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement("histogram");
            {
                var hist = xmlDoc.CreateElement("hist");
                {
                    var place = xmlDoc.CreateElement("place");
                    {
                        var text = xmlDoc.CreateTextNode("one");
                        place.AppendChild(text);
                    }

                    var value = xmlDoc.CreateElement("value");
                    {
                        var text = xmlDoc.CreateTextNode("0");
                        value.AppendChild(text);
                    }

                    var height = xmlDoc.CreateElement("height");
                    {
                        var text = xmlDoc.CreateTextNode("0.00px");
                        height.AppendChild(text);
                    }
                    hist.AppendChild(place);
                    hist.AppendChild(value);
                    hist.AppendChild(height);
                }
                elem.AppendChild(hist);
            }
            return elem;
        }

        private XmlElement CreateMethods(XmlDocument xmlDoc, IList<CrapDataEntry> data)
        {
            var methods = xmlDoc.CreateElement("methods");
            foreach (var method in data)
            {
                var methodElement = CreateMethodElement(xmlDoc,method);
                methods.AppendChild(methodElement);
            }
            return methods;
            
        }

        private XmlElement CreateMethodElement(XmlDocument xmlDoc,CrapDataEntry method)
        {
            var methodElement = xmlDoc.CreateElement("method");

            var package = xmlDoc.CreateElement("package");
            var packValue = xmlDoc.CreateTextNode(method.Method.TypeName);
            package.AppendChild(packValue);
            methodElement.AppendChild(package);

            var className = xmlDoc.CreateElement("className");
            var classNameValue = xmlDoc.CreateTextNode(method.Method.TypeName);
            className.AppendChild(classNameValue);
            methodElement.AppendChild(className);

            var methodName = xmlDoc.CreateElement("methodName");
            var methodNameValue = xmlDoc.CreateTextNode(method.Method.MethodName);
            methodName.AppendChild(methodNameValue);
            methodElement.AppendChild(methodName);

            var methodSignature = xmlDoc.CreateElement("methodSignature");
            var methodSignatureValue = xmlDoc.CreateTextNode("()");
            methodSignature.AppendChild(methodSignatureValue);
            methodElement.AppendChild(methodSignature);

            var fullMethod = xmlDoc.CreateElement("fullMethod");
            var fullMethodValue = xmlDoc.CreateTextNode(string.Format("{0}.{1}",method.Method.TypeName,method.Method.MethodName));
            fullMethod.AppendChild(fullMethodValue);
            methodElement.AppendChild(fullMethod);

            var crap = xmlDoc.CreateElement("crap");
            var crapValue = xmlDoc.CreateTextNode(method.Crap.ToString());
            crap.AppendChild(crapValue);
            methodElement.AppendChild(crap);
            
            var complexity= xmlDoc.CreateElement("complexity");
            var complexityValue = xmlDoc.CreateTextNode(method.CC.ToString());
            complexity.AppendChild(complexityValue);
            methodElement.AppendChild(complexity);

            var coverage = xmlDoc.CreateElement("coverage");
            var coverageValue = xmlDoc.CreateTextNode(method.Coverage.ToString());
            coverage.AppendChild(coverageValue);
            methodElement.AppendChild(coverage);

            var crapLoad = xmlDoc.CreateElement("crapLoad");
            var crapLoadValue = xmlDoc.CreateTextNode(method.CalculateCrapLoad().ToString("0"));
            crapLoad.AppendChild(crapLoadValue);
            methodElement.AppendChild(crapLoad);

            return methodElement;
        }
        
    }
}
