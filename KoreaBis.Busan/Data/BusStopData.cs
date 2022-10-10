/* 3. BusLocation (busInfoByRouteId)
 * 
 * 노선 ID(lineid)를 검색조건으로 노선 정보 및 실시간 버스 위치 정보를 제공합니다.
 * Provides the detailed information and realtime location of the bus by searching with Bus ID (lineid)
 * 
 */

using KoreaBis.Busan.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace KoreaBis.Busan.Data
{
    public class BusLocationRequestData : IRequestData
    {
        [ApiProperty("lineid")]
        public string? BusID { get; set; }
    }

    public class BusLocationResponseData : IResponseData
    {
        public int? BusStopNo { get; }
        public TimeSpan? AverageETA { get; }
        public int? BusStopOrder { get; }
        public string? BusStopName { get; }
        public string? BusNumberPlate { get; }
        public int? Direction { get; }
        public DateTime? GPSTime { get; }
        public Geolocation? Location { get; }
        public string? BusName { get; }
        public long? NodeID { get; }
        public NodeType? NodeType { get; }
        public bool? IsTerminalPoint { get; }
        public bool? IsLowFloorBus { get; }

        private BusLocationResponseData(int? busStopNo, TimeSpan? averageETA, int? busStopOrder, string? busStopName, string? busNumberPlate, int? direction, DateTime? gpsTime, Geolocation? location, string? busName, long? nodeID, NodeType? nodeType, bool? isTerminalPoint, bool? isLowFloorBus)
        {
            BusStopNo = busStopNo;
            AverageETA = averageETA;
            BusStopOrder = busStopOrder;
            BusStopName = busStopName;
            BusNumberPlate = busNumberPlate;
            Direction = direction;
            GPSTime = gpsTime;
            Location = location;
            BusName = busName;
            NodeID = nodeID;
            NodeType = nodeType;
            IsTerminalPoint = isTerminalPoint;
            IsLowFloorBus = isLowFloorBus;
        }

        internal static List<BusLocationResponseData> Parse(XmlNode xml)
        {
            List<BusLocationResponseData> result = new List<BusLocationResponseData>();

            foreach(XmlNode item in xml["items"])
            {
                /*
                int.TryParse(item["arsno"]?.InnerText, out arsno);
                double.TryParse(item["avgym"]?.InnerText, out avgym);
                int.TryParse(item["bstopidx"]?.InnerText, out bstopidx);
                byte.TryParse(item["direction"]?.InnerText, out direction);
                DateTime.TryParseExact(item["gpsym"]?.InnerText, "HHmmss", null, DateTimeStyles.None, out gpsym);
                double.TryParse(item["lin"]?.InnerText, out lin);
                double.TryParse(item["lat"]?.InnerText, out lat);
                long.TryParse(item["nodeid"]?.InnerText, out nodeid);
                byte.TryParse(item["nodekn"]?.InnerText, out nodekn);
                byte.TryParse(item["rpoint"]?.InnerText, out rpoint);
                byte.TryParse(item["lowplate"]?.InnerText, out lowplate);
                */

                int? arsno = item["arsno"]?.InnerText.ParseTo(typeof(int)) as int?;
                double? t_avgym = item["avgym"]?.InnerText.ParseTo(typeof(double)) as double?;
                int? bstopidx = item["bstopidx"]?.InnerText.ParseTo(typeof(int)) as int?;
                byte? direction = item["direction"]?.InnerText.ParseTo(typeof(byte)) as byte?;
                double? t_lin = item["lin"]?.InnerText.ParseTo(typeof(double)) as double?;
                double? t_lat = item["lat"]?.InnerText.ParseTo(typeof(double)) as double?;
                long? nodeid = item["nodeid"]?.InnerText.ParseTo(typeof(long)) as long?;
                byte? nodekn = item["nodekn"]?.InnerText.ParseTo(typeof(byte)) as byte?;
                byte? rpoint = item["rpoint"]?.InnerText.ParseTo(typeof(byte)) as byte?;
                byte? lowplate = item["lowplate"]?.InnerText.ParseTo(typeof(byte)) as byte?;

                TimeSpan? avgym;
                DateTime? gpsym;
                Geolocation? geolocation;

                if (t_avgym == null) avgym = null;
                else avgym = TimeSpan.FromSeconds(t_avgym.Value);

                if (int.TryParse(item["gpsym"]?.InnerText[0..2], out int gpsymh) && gpsymh >= 24)
                    item["gpsym"].InnerText = (gpsymh - 24).ToString("00") + item["gpsym"].InnerText[2..];
                if (item["gpsym"]?.InnerText.StartsWith("24") == true) item["gpsym"].InnerText = "00" + item["gpsym"].InnerText[2..];
                
                if (DateTime.TryParseExact(item["gpsym"]?.InnerText, "HHmmss", null, DateTimeStyles.None, out var t_gpsym)) gpsym = t_gpsym;
                else gpsym = null;

                if (t_lat == null || t_lin == null) geolocation = null;
                else geolocation = new Geolocation(t_lat.Value, t_lin.Value);

                if (item["lineno"]?.InnerText.EndsWith("-F", StringComparison.InvariantCultureIgnoreCase) == true)
                    item["lineno"].InnerText = item["lineno"].InnerText.Replace("-F", "(심야)", StringComparison.InvariantCultureIgnoreCase);


                result.Add(new BusLocationResponseData(
                    arsno, avgym, bstopidx, 
                    item["bstopnm"]?.InnerText, item["carno"]?.InnerText, direction,
                    gpsym, geolocation,
                    item["lineno"]?.InnerText, nodeid, (NodeType?)nodekn, rpoint.HasValue && rpoint.Value != 0, lowplate.HasValue && lowplate.Value != 0
                ));
            }

            return result;
        }

    }

}
