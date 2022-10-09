using KoreaBis.Busan.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KoreaBis.Busan.Data
{
    public enum ApiStatusCode
    {
        OK = 0,
        ApplicationError = 1,
        InvalidRequestParameter = 10,
        ServiceNotFound = 12,
        ServiceAccessDenied = 20,
        QuotaExceeded = 22,
        UnregisteredServiceKey = 30,
        DeadlineHasExpired = 31,
        UnregisteredIPAddress = 32,
        Unknown = 99
    }

    public class BusanBisEventArgs : EventArgs
    {
        
    }

    public class BusanBisResponseEventArgs : BusanBisEventArgs
    {
        public ApiStatusCode StatusCode { get; set; }

        public string Message { get; }

        public BusanBisResponseEventArgs(ApiStatusCode statusCode)
        {
            StatusCode = statusCode;
            Message = GetMessage(CultureInfo.CurrentUICulture);
        }

        public string GetMessage(CultureInfo culture)
        {
            Messages.Culture = culture;
            return Messages.ResourceManager.GetString($"ApiStatus{(int)StatusCode}");
        }
    }
}
