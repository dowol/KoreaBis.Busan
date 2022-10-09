using System;
using System.Collections.Generic;
using System.Text;

namespace KoreaBis.Busan.Utilities
{
    public static class UriExtensions
    {
        public static bool IsUriEncodedString(this string str)
        {
            return str == Uri.UnescapeDataString(str);
        }
    }
}
