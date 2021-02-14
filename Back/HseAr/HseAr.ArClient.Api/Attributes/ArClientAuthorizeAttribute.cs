using System;
using System.Linq;
using HseAr.ArClient.Api.Exceptions;
using HseAr.ArClient.Api.Helpers;
using HseAr.Core.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HseAr.ArClient.Api.Attributes
{
    public class ArClientAuthorizeAttribute : TypeFilterAttribute
    {
        public ArClientAuthorizeAttribute() : base(typeof(ValidateApiTokenFilter))
        {
        }
        
        public class ValidateApiTokenFilter : IActionFilter
        {
            private readonly Configuration _configuration;
            
            public ValidateApiTokenFilter(Configuration configuration)
            {
                _configuration = configuration;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var httpContext = context.HttpContext;
                if (httpContext.Request.Headers.TryGetValue(CustomHeaders.AuthKey, out var authKey)
                    && !string.IsNullOrEmpty(authKey.First()))
                {
                    if(Guid.TryParse(authKey, out var guidKey) && 
                       _configuration.ArClients.Any(p => p.Key == guidKey))
                    {
                        return;
                    }
                }
                
                throw new ArClientApiException(ArClientApiErrorCode.AuthorizationError, "Ошибка доступа");
            }
            
            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }


    }
    
}