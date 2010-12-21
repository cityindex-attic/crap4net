using System;
using System.Linq;

namespace Crap4Net
{
    class SignatureParser
    {
        private const string ANONYMOUS_CTOR = "<.ctor>";
        private const string ANONYMOUS_CCTOR = "<.cctor>";

        public static string GetMethodName(string fullname)
        {
            if (fullname.Contains(".ctor"))
                return HandleCtor(fullname);
            if (fullname.Contains(".cctor"))
                return HandleCctors(fullname);
            int end = fullname.LastIndexOf(':');
            int start = fullname.LastIndexOf('.')+1;
            var name = fullname.Substring(start, end-start).Trim();
            if (name.Contains('('))
                name = name.Substring(0, name.LastIndexOf('('));
            return name;
        }
        private static string HandleCtor(string fullname)
        {
            if (!fullname.Contains(ANONYMOUS_CTOR))
                return ".ctor";
            else
            {
                fullname = fullname.Remove(fullname.IndexOf(ANONYMOUS_CTOR), ANONYMOUS_CTOR.Length);
                return ANONYMOUS_CTOR + GetMethodName(fullname);
            }
        }

        private static string HandleCctors(string fullname)
        {
            if (!fullname.Contains(ANONYMOUS_CCTOR))
                return ".cctor";
            else
            {
                fullname = fullname.Remove(fullname.IndexOf(ANONYMOUS_CCTOR), ANONYMOUS_CCTOR.Length);
                return ANONYMOUS_CCTOR + GetMethodName(fullname);
            }
        }
        public static string GetClassName(string fullname)
        {
            fullname = RemoveAnonymousCtorsIfExists(fullname);
            int end = fullname.LastIndexOf('.');

            if ((fullname.Contains(".ctor")) ||(fullname.Contains(".cctor")))
                end=end-1;
            int start = fullname.Substring(0, end).LastIndexOf('.')+1;
            var classname = fullname.Substring(start, end - start).Trim();
            return classname;
        }

        private static string RemoveAnonymousCtorsIfExists(string fullname)
        {

            if (fullname.Contains(ANONYMOUS_CTOR))
                return fullname.Remove(fullname.IndexOf(ANONYMOUS_CTOR), ANONYMOUS_CTOR.Length);
            if (fullname.Contains(ANONYMOUS_CCTOR))
                return fullname.Remove(fullname.IndexOf(ANONYMOUS_CCTOR), ANONYMOUS_CCTOR.Length);
            return fullname;
        }
    }
}
