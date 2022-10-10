using KoreaBis.Busan.Data;
using KoreaBis.Busan.Utilities;

namespace KoreaBis.Busan.Test
{
    [TestClass]
    public class ClientTest
    {
        private readonly BusanBisClient bis
            = new("Borlu05tzlDrYGVIIps53DsQovckCrsL%2BG9JDFY%2B7avHmvWrX7Xcv%2F%2BeaKY6ZbwjnIPQCTkwUbN9t%2FI30h87ow%3D%3D");


        [TestMethod] // OK
        public async Task GetBusStopList()
        {
            BusStopListResponseData busStopList = 
                await bis.GetBusStopList(new() { CountPerPage = 30, BusStopName = "부산외국어대학교" });
            busStopList.WriteToConsole();
        }

        [TestMethod] // OK
        public async Task GetBusInfo()
        {
            foreach(BusInfoResponseData info in await bis.GetBusInfo(new() { BusName = "심야" }))
            {
                info.WriteToConsole();
            }
        }

        [TestMethod] // OK
        public async Task GetBusLocation()
        {
            foreach(BusLocationResponseData info in await bis.GetBusLocation(new() { BusID = "5201003F00" }))
            {
                info.WriteToConsole();
            }
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
