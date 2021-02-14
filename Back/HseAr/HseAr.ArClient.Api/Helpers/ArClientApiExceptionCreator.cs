using System;
using HseAr.ArClient.Api.Exceptions;

namespace HseAr.ArClient.Api.Helpers
{
    public class ArClientApiExceptionCreator
    {
        internal static ArClientApiException GetWebApiException(Exception exception)
            => exception switch
            {
                ArClientApiException apiException => apiException,
                _ => new ArClientApiException(ArClientApiErrorCode.UnspecifiedError,
                    "К сожалению, система в данный момент недоступна")
            };
    }
}