using System;

namespace HseAr.WebPlatform.Api.Exceptions
{
    public class WebApiException : Exception
    {
        public WebApiException(WebApiErrorCode code, string message)
            : base(message ?? string.Empty)
        {
            Code = code;
        }

        public WebApiErrorCode Code { get; set; }
        
    }
}