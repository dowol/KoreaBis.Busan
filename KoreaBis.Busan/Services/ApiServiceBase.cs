using KoreaBis.Busan.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KoreaBis.Busan.Services
{
    public abstract class ApiServiceBase
    {
        internal string ApiServiceName { get; }

        protected readonly BusanBisClient client;

        protected ApiServiceBase(string serviceName, BusanBisClient client)
        {
            ApiServiceName = serviceName;
            this.client = client;
        }

    }

    public abstract class ApiServiceBase<TRequestData, TResponseData> : ApiServiceBase
    {
        protected ApiServiceBase(string serviceName, BusanBisClient client) : base(serviceName, client) { }

        public abstract Task<TResponseData> GetResponse(TRequestData requestData);
    }
}
