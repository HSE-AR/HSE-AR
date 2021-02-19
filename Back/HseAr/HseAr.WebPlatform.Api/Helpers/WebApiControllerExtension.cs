using System;
using System.Linq;
using System.Security.Claims;
using HseAr.WebPlatform.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Helpers
{
    internal static class WebApiControllerExtension
    {
        /// <summary>
        /// Получить id пользователя по токену
        /// </summary>
        internal static Guid GetUserIdFromToken(this ControllerBase apiController)
        {
            var nameIdentifier = apiController.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifier == null)
            {
                throw new Exception("User not found");
            }

            return Guid.Parse(nameIdentifier.Value);
        }

        internal static Guid GetCompanyId(this ControllerBase apiController)
        {

            if (apiController.Request.Headers.TryGetValue(WebApiHeaders.CompanyKey, out var companyKey)
                && !string.IsNullOrEmpty(companyKey.First()))
            {
                return Guid.Parse(companyKey);
            }

            throw new WebApiException(WebApiErrorCode.AccessDenied, "требуется ключ компании");
        }
    }
}