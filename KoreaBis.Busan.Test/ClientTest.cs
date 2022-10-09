using KoreaBis.Busan.Data;
using KoreaBis.Busan.Utilities;

namespace KoreaBis.Busan.Test
{
    [TestClass]
    public class ClientTest
    {
        //BusanBisClient bis = new("BoRlu05tzlDrYGVIIps53DsQovckCrsL%2BG9JDFY%2B7avHmvWrX7Xcv%2F%2BeaKY6ZbwjnIPQCTkwUbN9t%2FI30h87ow%3D%3D");
        BusanBisClient bis = new("Borlu05tzlDrYGVIIps53DsQovckCrsL%2BG9JDFY%2B7avHmvWrX7Xcv%2F%2BeaKY6ZbwjnIPQCTkwUbN9t%2FI30h87ow%3D%3D");


        [TestMethod]
        public async Task GetBusStopList()
        {
            BusStopListResponseData busStopList = 
                await bis.GetBusStopList(new() { CountPerPage = 30, BusStopName = "부산외국어대학교" });
            busStopList.WriteToConsole();
            
        }

        //[TestMethod]
        public async Task GetBusInfo()
        {
            foreach(BusInfoResponseData info in await bis.GetBusInfo(new() { BusName = "80" }))
            {
                info.WriteToConsole();
            }
        }

        //[TestMethod]
        public async Task GetBusLocation()
        {
            
        }

        //[TestMethod]
        public async Task GetBusInfoOfBusStop()
        {
            
        }

        //[TestMethod]
        public async Task GetBusInfoOfLine()
        {
            
        }
        //[TestMethod]
        public async Task GetBusInfoByNumber()
        {
            
        }

    }
}
