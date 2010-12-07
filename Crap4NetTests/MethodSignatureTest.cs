using Crap4Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Crap4NetTests
{
    [TestClass()]
    public class MethodSignatureTest
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

        const string SOME_TYPE_NAME = "SomeType";
        const string SOME_METHOD_NAME = "SomeMethod";

        [TestMethod]
        public void ctor_LegalParameters_InitializeFieldsWithValues()
        {
            var target = new MethodSignature(SOME_TYPE_NAME, SOME_METHOD_NAME);

            Assert.AreEqual(SOME_TYPE_NAME, target.TypeName);
            Assert.AreEqual(SOME_METHOD_NAME, target.MethodName);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void ctor_NullTypeName_ThrowsException()
        {
            new MethodSignature(null, SOME_METHOD_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ctor_NullMethodName_ThrowsException()
        {
            new MethodSignature(SOME_TYPE_NAME, null);
        }

        [TestMethod()]
        public void Equals_IdenticalValues_ReturnsTrue()
        {
            var target1 = new MethodSignature(SOME_TYPE_NAME, SOME_METHOD_NAME);
            var target2 = new MethodSignature(SOME_TYPE_NAME, SOME_METHOD_NAME);

            var actual = target1.Equals(target2);

            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void Equals_DifferentMethodName_ReturnsFalse()
        {
            var target1 = new MethodSignature(SOME_TYPE_NAME, SOME_METHOD_NAME);
            var target2 = new MethodSignature(SOME_TYPE_NAME, SOME_METHOD_NAME+"a");

            var actual = target1.Equals(target2);

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void Equals_DifferentTypeName_ReturnsFalse()
        {
            var target1 = new MethodSignature(SOME_TYPE_NAME, SOME_METHOD_NAME + "a");
            var target2 = new MethodSignature(SOME_TYPE_NAME, SOME_METHOD_NAME);

            var actual = target1.Equals(target2);

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void Equals_NullObject_ReturnsFalse()
        {
            var target = new MethodSignature(SOME_TYPE_NAME, SOME_METHOD_NAME + "a");

            var actual = target.Equals(null);

            Assert.IsFalse(actual);
        }
    }
}
