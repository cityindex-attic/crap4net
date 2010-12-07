using System.Collections.Generic;
using System.Xml;
using System;
using Crap4Net.Formatters;

namespace Crap4Net
{
    public interface IFormatter
    {
        XmlDocument FormatReport(IList<CrapDataEntry> data);
        void FormatAndSaveToFile(IList<CrapDataEntry> data, string fileName);
    }

    public class FormatterFactory
    {
        public static IFormatter GetFormatter(string formatterName)
        {
            switch (formatterName)
            {
                case "Crap4J":
                    return new Crap4JXmlFormatter();
                    break;
                default:
                    return new XmlFormatter();
            }
        }
    }

}
