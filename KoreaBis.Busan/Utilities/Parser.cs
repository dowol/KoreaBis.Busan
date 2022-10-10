using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace KoreaBis.Busan.Utilities
{
    internal static class Parser
    {
        internal static object? ParseTo(this string str, Type typeToParse)
        {
            MethodInfo parser = typeToParse.GetMethod("Parse", new Type[] { typeof(string) });
            try
            {
                return parser.Invoke(null, new object[] { str });
            }
            catch
            {
                return null;
            }
        }
    }
}
