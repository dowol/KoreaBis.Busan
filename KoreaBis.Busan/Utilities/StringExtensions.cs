using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace KoreaBis.Busan.Utilities
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            return char.ToLower(str[0]) + str[1..];
        }

        public static string ToPascalCase(this string str)
        {
            return char.ToUpper(str[0]) + str[1..];
        }
    }
}
