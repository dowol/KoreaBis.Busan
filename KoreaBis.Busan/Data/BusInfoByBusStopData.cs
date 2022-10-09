using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KoreaBis.Busan.Data
{
    public class BusInfoByBusStopRequestData : IRequestData
    {
        [ApiProperty("Bstopid")]
        public long BusStopID { get; set; }

        public BusInfoByBusStopRequestData(long busStopId)
        {
            BusStopID = busStopId;
        }
    }

    public class BusInfoByBusStopResponseData : IResponseData
    {
        public int? BusStopNo { get; private set; }
        public long? BusStopID { get; private set; }
        public string? BusStopName { get; private set; }
        public string? BusKind { get; private set; }
        public int? OrderOfBusStop { get; private set; }
        public string? BusName { get; private set; }
        public string? BusID { get; private set; }
        public Geolocation? Location { get; private set; }
        public IReadOnlyList<BusLocationItem>? Info { get; private set; }

        internal BusInfoByBusStopResponseData(XmlNode xml)
        {
            
        }
    }

    public class BusLocationItem : IResponseData
    {
        internal BusLocationItem(XmlNode xml)
        {

        }
    }
}
