using KoreaBis.Busan.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace KoreaBis.Busan.Utilities
{
    public static class BisDataExtensions
    {
#if DEBUG
        public static void WriteToConsole(this IBisData data)
        {
            Console.WriteLine($"Type {data.GetType().FullName} :");
            foreach(PropertyInfo prop in data.GetType().GetProperties())
            {
                object? value = prop.GetValue(data);
                if (value is string)
                {
                    Console.WriteLine($"    {prop.Name} = {value}");
                }
                else if(value is IEnumerable enumerable)
                {
                    Console.WriteLine($"    {prop.Name} = [");
                    foreach(var item in enumerable)
                    {
                        //Console.WriteLine(item.GetType().GetInterfaces().Contains(typeof(IResponseData));
                        if (item is IResponseData responseData)
                        {
                            
                            responseData.WriteToConsole();
                            Console.WriteLine();
                        }
                        else 
                            Console.WriteLine(item.ToString() + " ,");
                    }
                    Console.WriteLine($"  ]");

                }
                else
                    Console.WriteLine($"    {prop.Name} = {value?.ToString() ?? "N/A"}");
            }
            Console.WriteLine();

        }
#endif

        public static IDictionary<string, object?> ToQueryDictionary(this IBisData data, bool appendIfNull = false)
        {
            IDictionary<string, object?> result = new Dictionary<string, object?>();

            IEnumerable<PropertyInfo> props = data.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(ApiPropertyAttribute)));
            foreach (PropertyInfo prop in props)
            {
                string key = ((ApiPropertyAttribute)Attribute.GetCustomAttribute(prop, typeof(ApiPropertyAttribute))).PropertyName ?? prop.Name.ToCamelCase();
                object? value = prop.GetValue(data, null);
                if (appendIfNull || value != null) result.Add(key, value);
            }

            return result;
        }

        public static IDictionary<string, object?> Normalize(this IDictionary<string, object?> qd)
        {
            foreach (KeyValuePair<string, object?> item in qd)
                if (item.Value == null) qd.Remove(item.Key);
            return qd;
        }

        public static string ToQueryString(this IDictionary<string, object?> qd)
        {
            StringBuilder sb = new StringBuilder("?");
            qd.Normalize();
            foreach(KeyValuePair<string, object?> item in qd)
            {
                string key = Uri.EscapeDataString(item.Key);
                string value = Uri.EscapeDataString(item.Value?.ToString() ?? "");
                sb.Append($"{key}={value}&");
            }
            string result = sb.ToString();
            return result[0..result.LastIndexOf('&')];
        }
    }
}
