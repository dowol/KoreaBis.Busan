using KoreaBis.Busan.Data;
using KoreaBis.Busan.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KoreaBis.Busan.Services
{
    public class BusInfoService : ApiServiceBase<BusInfoRequestData, List<BusInfoResponseData>>
    {
        public BusInfoService(BusanBisClient client) : base("busInfo", client) { }

        public override async Task<List<BusInfoResponseData>> GetResponse(BusInfoRequestData requestData)
        {
            return BusInfoResponseData.FromXml(await client.CallAsync(ApiServiceName, requestData.ToQueryDictionary()));
        }
    }
}
