using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Crap4NetTests
{


    [TestClass()]
    public class ReflectorParserTests
    {
        private const string  REFLECTOR_FILE_PATH = @"UnitTestData\ReflectorFiles\";
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        
        [TestMethod()]
        [ExpectedException(typeof(ApplicationException))]
        public void Data_FileNotLoaded_ThrowsException()
        {
            var target = new ReflectorParser();
            var dummy = target.Data;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadData_InvalidFileName_throwsException()
        {
            var target = new ReflectorParser();
            target.LoadData("NoSuchFile.xml");
        }

        [TestMethod]
        public void Data_ctor_CorrectMethodName()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "ctor.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", ".ctor", 1)));
        }

        [TestMethod]
        public void Data_EmptyClass_CtorIsAdded()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "EmptyClass.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CCDataEntry("class2", ".ctor", 1)));
        }

        [TestMethod]
        public void Data_GenericMethod_CorrectNameIsParsed()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "GenericMethod.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "GenericMethod<T>", 1)));
        }

        [TestMethod]
        [Ignore]
        public void Data_OverLoadedMethod_AllVariantsAreParsed()
        {
            Assert.Inconclusive("not supported");
        }

        [TestMethod]
        public void Data_Property_SetAndGetAreParsed()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "Property.xml");
            var actual = target.Data;

            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "get_SomeProperty", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "set_SomeProperty", 1)));
        }

        [TestMethod]
        public void Data_cctor_CorrectMaethodNameIsParsed()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "StaticCtor.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", ".cctor", 1)));
        }

        [TestMethod]
        public void Data_StaticMethod_IsPArsedCorrectly()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "StaticMethod.xml");
            var actual = target.Data;

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "staticMethod", 1)));
        }

        [TestMethod]
        public void Data_AnonymousType_IsParsedCorrectly()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "AnonymousType.xml");
            var actual = target.Data;

            Assert.AreEqual(6, actual.Count);
            var typename = "<>f__AnonymousType0<<Method>j__TPar,<Complexity>j__TPar>";

            Assert.IsTrue(actual.Contains(new CCDataEntry(typename, ".ctor", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry(typename, "get_Method", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry(typename, "get_Complexity", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry(typename, "ToString", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry(typename, "Equals", 3)));
            Assert.IsTrue(actual.Contains(new CCDataEntry(typename, "GetHashCode", 1)));
        }

        [TestMethod]
        public void Data_TypeWithoutNameSpace_IsParsedCorrectly()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "NoNameSpace.xml");
            var actual = target.Data;

            Assert.AreEqual(2, actual.Count);

            Assert.IsTrue(actual.Contains(new CCDataEntry("ClassWithNoNameSpace", "StameMethod", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("ClassWithNoNameSpace", ".ctor", 1)));
        }

        [TestMethod]
        public void Data_TypeWithoutComplexNameSpace_IsParsedCorrectly()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "ComplexNameSpace.xml");
            var actual = target.Data;

            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains(new CCDataEntry("ClassName", "StameMethod", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("ClassName", ".ctor", 1)));
        }

        [TestMethod]
        [Ignore]
        public void Data_FullFile_RetrievesCorrectData()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "FullReport.xml");
            var actual = target.Data;

            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", ".ctor", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method1", 2)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method2", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method2(Int32)", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method2(Double)", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method2(Single)", 1)));
            Assert.AreEqual(6, actual.Count);
        }

        [TestMethod]
        [Ignore]
        public void Data_ValidFile_RetrievesCorrectData()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "ReflectorCCReport.xml");
            var actual = target.Data;

            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", ".ctor", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method1", 2)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method2", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method2(Int32)", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method2(Double)", 1)));
            Assert.IsTrue(actual.Contains(new CCDataEntry("Class1", "method2(Single)", 1)));
            Assert.AreEqual(6, actual.Count);
        }

        [TestMethod]
        public void LoadData_FileFromRealUser_DoesNotCrash()
        {
            var target = new ReflectorParser();
            target.LoadData(REFLECTOR_FILE_PATH + "crap.xml");
            var actual = target.Data;

        }

    }
}
