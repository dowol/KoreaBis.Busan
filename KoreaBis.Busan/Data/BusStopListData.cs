using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace KoreaBis.Busan.Data
{
    public class BusStopListRequestData : IRequestData
    {
        [ApiProperty("numOfRows")]
        public int? CountPerPage { get; set; }
        [ApiProperty]
        public int? PageNo { get; set; }
        [ApiProperty("bstopnm")]
        public string? BusStopName { get; set; }
        [ApiProperty("arsno")]
        public int? BusStopNo { get; set; }

    }

    public class BusStopListResponseData : IResponseData
    {
        public int CountPerPage { get; }
        public int PageNo { get; }
        public int TotalCount { get; }
        public List<BusStopListItem> Items { get; }

        public BusStopListResponseData(int countPerPage, int pageNo, int totalCount, List<BusStopListItem> items)
        {
            CountPerPage = countPerPage;
            PageNo = pageNo;
            TotalCount = totalCount;
            Items = items;
        }

        public static BusStopListResponseData FromXml(XmlNode xml)
        {
            List<BusStopListItem> items = new List<BusStopListItem>();
            foreach(XmlNode item in xml["items"])
            {
                if(item.Name == "item")
                    items.Add(BusStopListItem.FromXml(item));
            }

            return new BusStopListResponseData(
                int.Parse(xml["numOfRows"].InnerText),
                int.Parse(xml["pageNo"].InnerText),
                int.Parse(xml["totalCount"].InnerText),
                items
            );
        }
    }

    public class BusStopListItem : IResponseData
    {
        public string BusStopNo { get; }

        public long BusStopID { get; }

        public string BusStopName { get; }
        public Geolocation Location { get; }

        public string BusStopType { get; }

        internal BusStopListItem(string busStopNo, long busStopID, string busStopName, double latitude, double longitude, string busStopType)
        {
            BusStopNo = busStopNo;
            BusStopID = busStopID;
            BusStopName = busStopName;
            Location = new Geolocation(latitude, longitude); 
            BusStopType = busStopType;
        }

        public static BusStopListItem FromXml(XmlNode xml)
        {
            return new BusStopListItem(
                xml["arsno"]?.InnerText ?? "N/A",
                long.Parse(xml["bstopid"].InnerText),
                xml["bstopnm"].InnerText,
                double.Parse(xml["gpsy"].InnerText),
                double.Parse(xml["gpsx"].InnerText),
                xml["stoptype"].InnerText
            );
        }
    }
}
