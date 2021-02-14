using System;
using System.Linq;
using HseAr.ArClient.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.ArClient.Api.Helpers
{
    internal static class ArClientApiControllerExtension
    {
        internal static Guid GetArClientKey(this ControllerBase apiController)
        {

            if (apiController.Request.Headers.TryGetValue(CustomHeaders.AuthKey, out var authKey)
                && !string.IsNullOrEmpty(authKey.First()))
            {
                return Guid.Parse(authKey);
            }
            
            throw new ArClientApiException(ArClientApiErrorCode.AuthorizationError, "не удалось получить id партнера");
           
        }
    }
}