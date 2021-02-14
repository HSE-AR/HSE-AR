using System;

namespace HseAr.ArClient.Api.Exceptions
{
    public class ArClientApiException : Exception
    {
        public ArClientApiException(ArClientApiErrorCode code, string message)
            : base(message ?? string.Empty)
        {
            Code = code;
        }

        public ArClientApiErrorCode Code { get; set; }
    }
}