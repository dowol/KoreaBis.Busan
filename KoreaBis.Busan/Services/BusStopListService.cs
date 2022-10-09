using KoreaBis.Busan.Data;
using KoreaBis.Busan.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KoreaBis.Busan.Services
{
    public class BusStopListService : ApiServiceBase<BusStopListRequestData, BusStopListResponseData>
    {
        internal BusStopListService(BusanBisClient client) : base("busStopList", client) { }

        public override async Task<BusStopListResponseData> GetResponse(BusStopListRequestData requestData)
        {

            return BusStopListResponseData.FromXml(await client.CallAsync(ApiServiceName, requestData.ToQueryDictionary()));
        }
    }
}
