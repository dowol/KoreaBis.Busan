using KoreaBis.Busan.Data;
using KoreaBis.Busan.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KoreaBis.Busan.Services
{
    public class BusLocationService : ApiServiceBase<BusLocationRequestData, List<BusLocationResponseData>>
    {
        public BusLocationService(BusanBisClient client) : base("busInfoByRouteId", client) { }

        public override async Task<List<BusLocationResponseData>> GetResponse(BusLocationRequestData requestData)
        {
            return BusLocationResponseData.Parse(await client.CallAsync(ApiServiceName, requestData.ToQueryDictionary()));
        }
    }
}
