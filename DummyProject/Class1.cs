using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dummyProject
{
    
    public class class2
    {
    }

    public class Class1
    {
        bool flag = false;
        public Class1()
        {

        }
        public void method1()
        {
            if (flag)
            {
                Console.WriteLine("Method1");
            }
            else
            {
                Console.WriteLine("Method1");
            }

        }
        public void method2()
        {
            Console.WriteLine("Method2");
        }

        public int method2(int a)
        {
            return a+5;
        }

        public double method2(double a)
        {
            return a + 5;
        }

        public void method2(float a)
        {
            Console.WriteLine(a);
        }

        public T GenericMethod<T>()
        {
            return default(T);
        }
        
        private object _SomeProperty;

        public object SomeProperty
        {
            get { return _SomeProperty; }
            set { _SomeProperty = value; }
        }

        static void staticMethod()
        {
        }

        static Class1()
        {
        }
    }
}
