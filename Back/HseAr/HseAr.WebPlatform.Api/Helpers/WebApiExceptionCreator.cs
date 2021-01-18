using System;
using HseAr.WebPlatform.Api.Exceptions;
using Microsoft.OpenApi.Models;

namespace HseAr.WebPlatform.Api.Helpers
{
    internal static class WebApiExceptionCreator
    {
        internal static WebApiException GetWebApiException(Exception exception)
            => exception switch
            {
                WebApiException apiException => apiException,
                _ => new WebApiException(WebApiErrorCode.UnspecifiedError,
            "К сожалению, система в данный момент недоступна")
            };
    }
}