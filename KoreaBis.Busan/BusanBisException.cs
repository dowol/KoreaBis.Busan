using KoreaBis.Busan.Data;
using KoreaBis.Busan.Resources;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KoreaBis.Busan
{
    public class BusanBisException : Exception
    {
        public ApiStatusCode StatusCode
        {
            get => (ApiStatusCode)Data["StatusCode"];
            protected set => Data["StatusCode"] = value;
        }
        public override string Message { get; }
        public override string HelpLink => $"https://dowol.github.io/KoreaBis.Busan/help/exception/{StatusCode}";
        

        public BusanBisException(ApiStatusCode status) : base()
        {
            StatusCode = status;
            Messages.Culture = new CultureInfo(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            Message = Messages.ResourceManager.GetString($"ApiStatus{(int)StatusCode}");
        }

    }
}
