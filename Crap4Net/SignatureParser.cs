using System;
using System.Linq;

namespace Crap4Net
{
    class SignatureParser
    {
        public static string GetMethodName(string fullname)
        {
            if (fullname.Contains(".ctor"))
                return ".ctor";
            if (fullname.Contains(".cctor"))
                return ".cctor";
            int end = fullname.LastIndexOf(':');
            int start = fullname.LastIndexOf('.')+1;
            var name = fullname.Substring(start, end-start).Trim();
            if (name.Contains('('))
                name = name.Substring(0, name.LastIndexOf('('));
            return name;
        }
        public static string GetClassName(string fullname)
        {
            int end = fullname.LastIndexOf('.');

            if ((fullname.Contains(".ctor")) ||(fullname.Contains(".cctor")))
                end=end-1;
            int start = fullname.Substring(0, end).LastIndexOf('.')+1;
            var classname = fullname.Substring(start, end - start).Trim();
            return classname;
        }
    }
}
