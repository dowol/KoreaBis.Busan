using System;
using System.Collections.Generic;
using System.Text;

namespace KoreaBis.Busan.Data
{
    public class BusLocationRequestData : IRequestData
    {
        [ApiProperty("lineid")]
        public string BusID { get; set; }

        public BusLocationRequestData(string busID)
        {
            BusID = busID;
        }
    }

    public class BusLocationResponseData : IResponseData
    {
        public int? BusStopNo { get; set; }
        public int? AverageTime { get; set; }
        public int? BusStopOrder { get; set; }
        public string? BusStopName { get; set; }
        public string? BusNumberPlate { get; set; }
        public BusDirection? Direction { get; set; }
        public Geolocation? Location { get; set; }
        public string? BusName { get; set; }
        public long? NodeID { get; set; }
        public bool? IsTerminalPoint { get; set; }
        public bool? IsNonStepBus { get; set; }
    
    }

}
