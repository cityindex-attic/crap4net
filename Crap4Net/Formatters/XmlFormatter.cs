using System.Collections.Generic;
using System.Xml;
using System;

namespace Crap4Net
{
    public class XmlFormatter : IFormatter
    {
        public XmlDocument FormatReport(IList<CrapDataEntry> data)
        {
            XmlDocument xmlDoc = new XmlDocument();

            AppendDecleration(xmlDoc);

            AppendRootElement(xmlDoc);

            XmlElement sectionNode = AppendSectionElement(xmlDoc);

            AppendCrapEntries(data, xmlDoc, sectionNode);

            return xmlDoc;
        }

        public void FormatAndSaveToFile(IList<CrapDataEntry> data, string fileName)
        {
            var xmlDoc = new XmlFormatter().FormatReport(data);
            xmlDoc.Save(fileName);
        }

        private static XmlDeclaration AppendDecleration(XmlDocument xmlDoc)
        {
            // Write down the XML declaration
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
            return xmlDeclaration;
        }

        private static void AppendRootElement(XmlDocument xmlDoc)
        {
            XmlElement rootNode = xmlDoc.CreateElement("CrapReport");
            xmlDoc.AppendChild(rootNode);
        }

        private static XmlElement AppendSectionElement(XmlDocument xmlDoc)
        {
            // Create a new <Category> element and add it to the root node
            XmlElement sectionNode = xmlDoc.CreateElement("Section");
            sectionNode.SetAttribute("Name", "Methods");
            xmlDoc.DocumentElement.PrependChild(sectionNode);
            return sectionNode;
        }

        private static void AppendCrapEntries(IList<CrapDataEntry> data, XmlDocument xmlDoc, XmlElement sectionNode)
        {
            // Create the required nodes
            foreach (var item in data)
            {
                XmlElement methodNode = xmlDoc.CreateElement("Method");
                methodNode.SetAttribute("Type", item.Method.TypeName);
                methodNode.SetAttribute("Name", item.Method.MethodName);
                methodNode.SetAttribute("Crap", item.Crap.ToString());
                sectionNode.AppendChild(methodNode);
            }
        }
    }
}
