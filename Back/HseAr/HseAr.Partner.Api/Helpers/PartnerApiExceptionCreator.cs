using System;
using HseAr.Partner.Api.Exceptions;

namespace HseAr.Partner.Api.Helpers
{
    public class PartnerApiExceptionCreator
    {
        internal static PartnerApiException GetWebApiException(Exception exception)
            => exception switch
            {
                PartnerApiException apiException => apiException,
                _ => new PartnerApiException(PartnerApiErrorCode.UnspecifiedError,
                    "К сожалению, система в данный момент недоступна")
            };
    }
}