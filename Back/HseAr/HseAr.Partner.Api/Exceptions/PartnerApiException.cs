using System;

namespace HseAr.Partner.Api.Exceptions
{
    public class PartnerApiException : Exception
    {
        public PartnerApiException(PartnerApiErrorCode code, string message)
            : base(message ?? string.Empty)
        {
            Code = code;
        }

        public PartnerApiErrorCode Code { get; set; }
    }
}