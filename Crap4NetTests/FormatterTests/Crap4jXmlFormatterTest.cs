//using System.Collections.Generic;
//using System.IO;
//using System.Xml;
//using Crap4Net;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Crap4Net.Formatters;

//namespace Crap4NetTests
//{
//    [TestClass()]
//    public class Crap4JXmlFormatterTest
//    {

//        #region Additional test attributes
//        // 
//        //You can use the following additional attributes as you write your tests:
//        //
//        //Use ClassInitialize to run code before running the first test in the class
//        //[ClassInitialize()]
//        //public static void MyClassInitialize(TestContext testContext)
//        //{
//        //}
//        //
//        //Use ClassCleanup to run code after all tests in a class have run
//        //[ClassCleanup()]
//        //public static void MyClassCleanup()
//        //{
//        //}
//        //
//        //Use TestInitialize to run code before running each test
//        //[TestInitialize()]
//        //public void MyTestInitialize()
//        //{
//        //}
//        //
//        //Use TestCleanup to run code after each test has run
//        //[TestCleanup()]
//        //public void MyTestCleanup()
//        //{
//        //}
//        //
//        #endregion

//        private IList<CrapDataEntry> _Data = new List<CrapDataEntry>(){
//            new CrapDataEntry("Class1", ".ctor", 17),
//            new CrapDataEntry("Class1", "method1", 13.2),
//            new CrapDataEntry("Class2", "method2", 1)};

//        [TestMethod()]
//        public void FormatReport_GivenValidData_BuildProperXml()
//        {

//            var target = new Crap4JXmlFormatter();
//            target.FormatAndSaveToFile(_Data,@"E:\Users\lior\Documents\Visual Studio 2008\Projects\Crap4Net\xslt\Crap4j\MyReport.xml");

//            //string actual = GetStringfromXml(xmlDoc);

//            //string expected = LoadXml(@"UnitTestData\CrapReport.xml");
//            //Assert.AreEqual(expected, actual);
//        }

//        [TestMethod]
//        public void FormatAndSaveToFile_ValidData_SavesAsXMlToGivenFileName()
//        {
//            try
//            {
//                new XmlFormatter().FormatAndSaveToFile(_Data, "output.xml");

//                string actual = LoadXml("output.xml");
//                string expected = LoadXml(@"UnitTestData\CrapReport.xml");
//                Assert.AreEqual(expected, actual);
//            }
//            finally
//            {
//                File.Delete("output.xml");
//            }


//        }

//        private static string GetStringfromXml(XmlDocument xmlDoc)
//        {
//            using (StringWriter stringWriter = new StringWriter())
//            {
//                using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
//                {
//                    xmlDoc.WriteTo(xmlWriter);
//                }
//                string actual = stringWriter.ToString();
//                return actual;
//            }
//        }
//        private static string LoadXml(string fileName)
//        {
//            XmlDocument loaded = new XmlDocument();
//            loaded.Load(fileName);
//            using (StringWriter stringWriter = new StringWriter())
//            {
//                using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
//                {
//                    loaded.WriteTo(xmlWriter);
//                }
//                return stringWriter.ToString();
//            }
//        }
//    }
//}
