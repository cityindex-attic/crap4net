using Crap4Net;
using NUnit.Framework;

namespace Crap4NetTests
{
    [TestFixture]
    public class SignatureParserTest
    {

        string[] inputs = 
        {
            //regular
            "NameSpace.ClassName.StamMethod() : Void",
            //anonymous
            "<>f__AnonymousType0<<Method>j__TPar,<Complexity>j__TPar>..ctor(<Method>j__TPar, <Complexity>j__TPar)",
            "<>f__AnonymousType0<<Method>j__TPar,<Complexity>j__TPar>.get_Method() : <Method>j__TPar",
            "<>f__AnonymousType0<<Method>j__TPar,<Complexity>j__TPar>.get_Complexity() : <Complexity>j__TPar",
            //complex namespace
            "NameSpace1.NameSpace2.ClassName.StameMethod() : Void",
            "NameSpace1.NameSpace2.ClassName..ctor()",
            //generics
            "dummyProject.Class1.GenericMethod<T>() : T",
            //no namespacea
            "ClassWithNoNameSpace.StameMethod() : Void",
            "ClassWithNoNameSpace..ctor()",
            //overload
            "dummyProject.Class1.method2() : Void",
            "dummyProject.Class1.method2(Int32) : Int32",
            "dummyProject.Class1.method2(Double) : Double",
            "dummyProject.Class1.method2(Single) : Void",
            //properties
            "dummyProject.Class1.get_SomeProperty() : Object",
            "dummyProject.Class1.set_SomeProperty(Object) : Void",
            //static ctor
            "dummyProject.Class1..cctor()",
            //static method
            "dummyProject.Class1.staticMethod() : Void",
        };
        string[] expectedClasses = 
        {
            //regular
            "ClassName",
            //anonymous
            "<>f__AnonymousType0<<Method>j__TPar,<Complexity>j__TPar>",
            "<>f__AnonymousType0<<Method>j__TPar,<Complexity>j__TPar>",
            "<>f__AnonymousType0<<Method>j__TPar,<Complexity>j__TPar>",
            //complex namespace
            "ClassName",
            "ClassName",
            //generics
            "Class1",
            //no namespacea
            "ClassWithNoNameSpace",
            "ClassWithNoNameSpace",
            //overload
            "Class1",
            "Class1",
            "Class1",
            "Class1",
            //properties
            "Class1",
            "Class1",
            //static ctor
            "Class1",
            //static method
            "Class1",
        };

        string[] expectedMethods = 
        {
            //regular
            "StamMethod",
            //anonymous
            ".ctor",
            "get_Method",
            "get_Complexity",
            //complex namespace
            "StameMethod",
            ".ctor",
            //generics
            "GenericMethod<T>",
            //no namespacea
            "StameMethod",
            ".ctor",
            //overload
            "method2",
            "method2",
            "method2",
            "method2",
            //properties
            "get_SomeProperty",
            "set_SomeProperty",
            //static ctor
            ".cctor",
            //static method
            "staticMethod",
        };
        //anonymous method inside constructor
        [TestCase("Dal.ExceptionsSqlServerDal.<.ctor>b__0(IMemberConfigurationExpression<Policy>) : Void", "<.ctor>b__0")]
        [TestCase("Dal.ExceptionsSqlServerDal.<.cctor>b__0(IMemberConfigurationExpression<Policy>) : Void", "<.cctor>b__0")]
        public void GetMethodName_VariousInputs_NameIsParsedCorrectly1(string input, string expected)
        {
            string actual = SignatureParser.GetMethodName(input);

            Assert.AreEqual(expected, actual);
        }

        //anonymous method inside constructor
        [TestCase("Dal.ExceptionsSqlServerDal.<.ctor>b__0(IMemberConfigurationExpression<Policy>) : Void", "ExceptionsSqlServerDal")]
        [TestCase("Dal.ExceptionsSqlServerDal.<.cctor>b__0(IMemberConfigurationExpression<Policy>) : Void", "ExceptionsSqlServerDal")]
        public void GetClassName_VariousInputs_NameIsParsedCorrectly(string input, string expected)
        {
            string actual = SignatureParser.GetClassName(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMethodName_VariousInputs_NameIsParsedCorrectly(
            [Values(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)] int x)
        {
            string actual = SignatureParser.GetMethodName(inputs[x]);

            Assert.AreEqual(expectedMethods[x], actual);
        }

        [Test]
        public void GetClassName_VariousInputs_NameIsParsedCorrectly(
            [Values(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)] int x)
        {
            string actual = SignatureParser.GetClassName(inputs[x]);

            Assert.AreEqual(expectedClasses[x], actual);
        }


    }
}
