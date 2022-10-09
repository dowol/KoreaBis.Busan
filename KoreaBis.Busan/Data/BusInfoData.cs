using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace KoreaBis.Busan.Data
{
    public class BusInfoRequestData : IRequestData
    {
        [ApiProperty("lineid")]
        public long? BusID { get; set; }

        [ApiProperty("lineno")]
        public string? BusName { get; set; }
    }



    public class BusInfoResponseData : IResponseData
    {
        public string? BusID { get; }
        public string? BusName { get; }
        public string? BusKind { get; }
        public string? Company { get; }
        public string? StartingPoint { get; }
        public string? TerminalPoint { get; }

        public TimeSpan? FirstBus { get; }
        public TimeSpan? LastBus { get; }
        public IntervalRange? WeekdayInterval { get; }
        public IntervalRange? RushHourInterval { get; }
        public IntervalRange? WeekendInterval { get; }

        internal BusInfoResponseData(string? busID, string? busName, string? busKind, string? company, string? startingPoint, string? terminalPoint, TimeSpan? firstBus, TimeSpan? lastBus, IntervalRange? weekdayInterval, IntervalRange? rushHourInterval, IntervalRange? weekendInterval)
        {
            BusID = busID;
            BusName = busName;
            BusKind = busKind;
            Company = company;
            StartingPoint = startingPoint;
            TerminalPoint = terminalPoint;
            FirstBus = firstBus;
            LastBus = lastBus;
            WeekdayInterval = weekdayInterval;
            RushHourInterval = rushHourInterval;
            WeekendInterval = weekendInterval;
        }

        public static List<BusInfoResponseData> FromXml(XmlNode xml)
        {
            List<BusInfoResponseData> result = new List<BusInfoResponseData>();

            foreach (XmlNode item in xml["items"])
            {
                TimeSpan.TryParse(item["firsttime"]?.InnerText, out var fb);
                TimeSpan.TryParse(item["endtime"]?.InnerText, out var lb);
                IntervalRange? ivn = IntervalRange.Parse(item["headwaynorm"]?.InnerText);
                IntervalRange? ivr = IntervalRange.Parse(item["headwaypeak"]?.InnerText);
                IntervalRange? ive = IntervalRange.Parse(item["headwayholi"]?.InnerText);

                result.Add(new BusInfoResponseData(
                    item["lineid"]?.InnerText,
                    item["buslinenum"]?.InnerText,
                    item["bustype"]?.InnerText,
                    item["companyid"]?.InnerText,
                    item["startpoint"]?.InnerText,
                    item["endpoint"]?.InnerText,
                    fb, lb, ivn, ivr, ive
                ));
            }

            return result;
        }
    }

    public struct IntervalRange
    {
        public TimeSpan MinInterval { get; set; }
        public TimeSpan MaxInterval { get; set; }

        public IntervalRange(TimeSpan min, TimeSpan max)
        {
            MinInterval = new TimeSpan(Math.Min(min.Ticks, max.Ticks));
            MaxInterval = new TimeSpan(Math.Max(min.Ticks, max.Ticks));
        }

        public IntervalRange(TimeSpan interval) : this(interval, interval) { }

        public static IntervalRange? Parse(string? str)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(str) && Regex.IsMatch(str, @"^\d+(\b?[~-]\b?\d+)?$"))
                {
                    string[] tokens = str.Split(new char[] { '-', '~' }, StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim()).ToArray();
                    if (tokens.Length >= 2)
                        return new IntervalRange(TimeSpan.FromMinutes(int.Parse(tokens[0])), TimeSpan.FromMinutes(int.Parse(tokens[1])));
                    else
                        return new IntervalRange(TimeSpan.FromMinutes(int.Parse(tokens[0])));
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public override string ToString()
        {
            if (MinInterval == MaxInterval) return MinInterval.TotalMinutes.ToString();
            return $"{MinInterval.TotalMinutes}-{MaxInterval.TotalMinutes}";
        }
    }
    
}
