using KoreaBis.Busan.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace KoreaBis.Busan.Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ApiPropertyAttribute : Attribute 
    { 
        public string? PropertyName { get; }
        public bool Required { get; }
        public ApiPropertyAttribute(string? name = null, bool required = false)
        {
            PropertyName = name;
            Required = required;
        }
    }

    public interface IBisData { }
    public interface IRequestData : IBisData { }
    public interface IResponseData : IBisData { }

    public struct Geolocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Geolocation(double latitude, double longitude)
        {
            Latitude = Math.Round(latitude, 6);
            Longitude = Math.Round(longitude, 6);
        }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }
    }
}
