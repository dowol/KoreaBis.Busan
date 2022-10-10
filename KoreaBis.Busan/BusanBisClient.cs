using KoreaBis.Busan.Data;
using KoreaBis.Busan.Resources;
using KoreaBis.Busan.Services;
using KoreaBis.Busan.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace KoreaBis.Busan
{
    public class BusanBisClient
    {
        private HttpClient http { get; }
        private string ServiceKey { get; }

        public event HttpResponseEventHandler? OnHttpResponse;
        public event BusanBisResponseEventHandler? OnResponse;

        public BusanBisClient(HttpClient client, string serviceKey)
        {
            http = client;
            ServiceKey = Uri.UnescapeDataString(serviceKey);
        }
        public BusanBisClient(string serviceKey) : this(new HttpClient() { Timeout = TimeSpan.FromSeconds(30)}, serviceKey) { }

        #region BIS API Methods
        public Task<BusStopListResponseData> GetBusStopList(BusStopListRequestData requestData)
        {
            return new BusStopListService(this).GetResponse(requestData);
        }

        public Task<List<BusInfoResponseData>> GetBusInfo(BusInfoRequestData requestData)
        {
            return new BusInfoService(this).GetResponse(requestData);
        }

        public Task<List<BusLocationResponseData>> GetBusLocation(BusLocationRequestData requestData)
        {
            return new BusLocationService(this).GetResponse(requestData);
        }

        

        #endregion

        internal async Task<XmlNode> CallAsync(string serviceName, IDictionary<string, object?> requestData)
        {
            requestData.Add("serviceKey", ServiceKey);
            Uri endpoint = new Uri("http://apis.data.go.kr/6260000/BusanBIMS/");
            
            Uri req = new Uri(endpoint, serviceName + requestData.ToQueryString());
#if DEBUG
            Console.WriteLine($"API Request URL: {req}");
#endif
            HttpResponseMessage response = await http.GetAsync(req);
            OnHttpResponse?.Invoke(response);

            XmlDocument xml = new XmlDocument();
            xml.Load(await response.Content.ReadAsStreamAsync());

#if DEBUG
            Console.WriteLine($"HTTP Status: {(int)response.StatusCode} {response.ReasonPhrase}");
#endif

            try
            {
                XmlElement header = xml["response"]["header"];
                XmlElement body = xml["response"]["body"];

                BusanBisResponseEventArgs e = new BusanBisResponseEventArgs(Enum.Parse<ApiStatusCode>(header["resultCode"].InnerText));

                OnResponse?.Invoke(e);

#if DEBUG
                Console.WriteLine("API Status: " + e.Message);
                Console.WriteLine();
#endif
                return body;
            }
            catch
            {
                ApiStatusCode status = Enum.Parse<ApiStatusCode>(xml["OpenAPI_ServiceResponse"]["cmmMsgHeader"]["returnReasonCode"].InnerText);
                OnResponse?.Invoke(new BusanBisResponseEventArgs(status));
                throw new BusanBisException(status);
            }
        }

    }

    public delegate void HttpResponseEventHandler(HttpResponseMessage response);

    public delegate void BusanBisResponseEventHandler(BusanBisResponseEventArgs e);


}
