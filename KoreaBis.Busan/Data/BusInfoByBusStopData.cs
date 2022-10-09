using System;
using System.Collections.Generic;
using System.Text;

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
        public int? BusStopNo { get; set; }
        public long? BusStopID { get; set; }
        public string? BusKind { get; set; }
        public int? OrderOfBusStop { get; set; }
        public string? BusName { get; set; }
        public string? BusID { get; set; }
        public Geolocation? Location { get; set; }
    }

    public class BusLocationItem : IResponseData
    {

    }
}
