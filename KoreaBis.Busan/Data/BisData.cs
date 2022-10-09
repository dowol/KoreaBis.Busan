using System;
using System.Text;

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
            return ToString("dec:n");
        }
        
        public string ToString(string? format, IFormatProvider? fp = null)
        {
            switch (format?.ToLower())
            {
                case "dms":
                case "dms:c":
                    throw new NotImplementedException();

                case "dms:n":
                    throw new NotImplementedException();

                default:
                    StringBuilder sb = new StringBuilder();

                    if (Latitude > 0) sb.Append("N ");
                    else if (Latitude < 0) sb.Append("S ");
                    sb.AppendFormat("{0:#.000000}", Math.Abs(Latitude));
                    sb.Append(", ");
                    if (Longitude > 0) sb.Append("E ");
                    else if (Longitude < 0) sb.Append("W ");
                    sb.AppendFormat("{0:#.000000}", Math.Abs(Longitude));

                    return sb.ToString();

                case "dec:c":
                    return $"{Latitude}, {Longitude}";

            }
        }
    }

    public enum BusDirection : byte
    {
        ToTerminalPoint,
        ToStartingPoint
    }
    
    public enum NodeType : byte
    {
        Junction,
        BusStop = 3
    }
}
