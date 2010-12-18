using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Crap4Net.Formatters;
using NUnit.Framework;

namespace Crap4Net.UnitTests
{
    [TestFixture]
    public class Crap4JXmlFormatterTests
    {
        IList<CrapDataEntry> _crapMethods = new List<CrapDataEntry>
                                 {
                                     new CrapDataEntry("type1", "method1", 20.11, 27, 14),
                                     new CrapDataEntry("type1", "method2", 20.1, 27, 14),
                                     new CrapDataEntry("type1", "method3", 2, 27, 14)
                                 };
        [Test]
        public void FormatsStatsCorrectly()
        {
            string expectedStatsXML =
@"<name>name</name>
<totalCrap>42.2</totalCrap>
<crap>0.0</crap>
<median>0.0</median>
<average>0.0</average>
<stdDev>0.0</stdDev>
<methodCount>3</methodCount>
<crapMethodCount>2</crapMethodCount>
<crapMethodPercent>66.7</crapMethodPercent>
<crapLoad>29</crapLoad>
<crapThreshold>15</crapThreshold>
<globalAverage>-1.0</globalAverage>
<globalCraploadAverage>-1.0</globalCraploadAverage>
<globalCrapMethodAverage>-1.0</globalCrapMethodAverage>
<globalTotalMethodAverage>-1.0</globalTotalMethodAverage>
<globalAverageDiff>0.0</globalAverageDiff>
<globalCraploadAverageDiff>0.0</globalCraploadAverageDiff>
<globalCrapMethodAverageDiff>0.0</globalCrapMethodAverageDiff>
<globalTotalMethodAverageDiff>0.0</globalTotalMethodAverageDiff>
<shareStatsUrl>http://www.crap4j.org/benchmark/stats/new?stat[project_hash]=1290006074016&amp;amp;stat[project_url]=test&amp;amp;stat[crap]=50.00&amp;amp;stat[crap_load]=5&amp;amp;stat[crap_methods]=1&amp;amp;stat[total_methods]=2&amp;amp;stat[ones]=0&amp;amp;stat[twos]=1&amp;amp;stat[fours]=0&amp;amp;stat[eights]=0&amp;amp;stat[sixteens]=1&amp;amp;stat[thirtytwos]=0&amp;amp;stat[sixtyfours]=0&amp;amp;stat[one28s]=0&amp;amp;stat[two56s]=0</shareStatsUrl>
<histogram><hist><place>one</place><value>0</value><height>0.00px</height></hist></histogram>
";

            var formatter = new Crap4JXmlFormatter();
            var xml = formatter.FormatReport(_crapMethods);

            AssertIsEqualWhiteSpaceInsensitive(expectedStatsXML, xml.SelectSingleNode("//stats").InnerXml);
        }

        [Test]
        public void FormatsMethodDataCorrectly()
        {
            string expectedTest1XML =
@"<package>type1</package>
<className>type1</className>
<methodName>method1</methodName>
<methodSignature>()</methodSignature>
<fullMethod>type1.method1</fullMethod>
<crap>20.11</crap>
<complexity>14</complexity>
<coverage>27</coverage>
<crapLoad>14</crapLoad>
";

            var formatter = new Crap4JXmlFormatter();
            var xml = formatter.FormatReport(_crapMethods);

            AssertIsEqualWhiteSpaceInsensitive(expectedTest1XML, xml.SelectSingleNode("//methods/method").InnerXml);
        }

        private static void AssertIsEqualWhiteSpaceInsensitive(string expected, string actual)
        {
            expected = Regex.Replace(expected, @"\s", "");
            actual = Regex.Replace(actual, @"\s", "");
            if (expected == actual) return;

            Console.WriteLine(expected);
            Console.WriteLine("===== DOES NOT EQUAL =====");
            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
