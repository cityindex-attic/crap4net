using dummyProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace dummyTests
{
    /// <summary>
    ///This is a test class for Class1Test and is intended
    ///to contain all Class1Test Unit Tests
    ///</summary>
    [TestClass()]
    public class Class1Test
    {


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


        /// <summary>
        ///A test for method1
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void method1Test()
        {
            Class1 target = new Class1(); // TODO: Initialize to an appropriate value
            target.method1();
        }

        [TestMethod]
        [Ignore]
        public void method2Test()
        {
            Class1 target = new Class1();
            target.GenericMethod<int>();
            target.GenericMethod<string>();
        }

        [TestMethod]
        [Ignore]
        public void usenoNameSpaceClass()
        {
            ClassWithNoNameSpace target = new ClassWithNoNameSpace();
            target.StameMethod();
        }
    }
}
