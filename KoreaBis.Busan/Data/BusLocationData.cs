/* 3. BusLocation (busInfoByRouteId)
 * 
 * 노선 ID(lineid)를 검색조건으로 노선 정보 및 실시간 버스 위치 정보를 제공합니다.
 * Provides the detailed information and realtime location of the bus by searching with Bus ID (lineid)
 * 
 */

using System;
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
        public int? BusStopNo { get; private set; }
        public TimeSpan? AverageETA { get; private set; }
        public int? BusStopOrder { get; private set; }
        public string? BusStopName { get; private set; }
        public string? BusNumberPlate { get; private set; }
        public BusDirection? Direction { get; private set; }
        public Geolocation? Location { get; private set; }
        public string? BusName { get; private set; }
        public long? NodeID { get; private set; }
        public NodeType? NodeType { get; private set; }
        public bool? IsTerminalPoint { get; private set; }
        public bool? IsLowFloorBus { get; private set; }

        internal BusLocationResponseData(XmlNode xml)
        {
            int.TryParse(xml["arsno"]?.InnerText, out int arsno);
            double.TryParse(xml["avgym"]?.InnerText, out double avgym);
            int.TryParse(xml["bstopidx"]?.InnerText, out int bstopidx);
            byte.TryParse(xml["direction"]?.InnerText, out byte direction);
            double.TryParse(xml["lin"]?.InnerText, out double lin);
            double.TryParse(xml["lat"]?.InnerText, out double lat);
            long.TryParse(xml["nodeid"]?.InnerText, out long nodeid);
            byte.TryParse(xml["nodekn"]?.InnerText, out byte nodekn);
            byte.TryParse(xml["rpoint"]?.InnerText, out byte rpoint);
            byte.TryParse(xml["lowplate"]?.InnerText, out byte lowplate);

            BusStopNo = arsno;
            AverageETA = TimeSpan.FromMinutes(avgym);
            BusStopOrder = bstopidx;
            BusStopName = xml["bstopnm"]?.InnerText;
            BusNumberPlate = xml["carno"]?.InnerText;
            Direction = (BusDirection)direction;
            Location = new Geolocation(lat, lin);
            BusName = xml["lineno"]?.InnerText;
            NodeID = nodeid;
            NodeType = (NodeType)nodekn;
            IsTerminalPoint = rpoint != 0;
            IsLowFloorBus = lowplate != 0;
        }

    }

}
