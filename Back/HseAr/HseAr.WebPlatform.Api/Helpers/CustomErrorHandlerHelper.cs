
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using HseAr.WebPlatform.Api.Exceptions;
using HseAr.WebPlatform.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace HseAr.WebPlatform.Api.Helpers
{
    public static class CustomErrorHandlerHelper
    {
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }

        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: true);

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: false);

        private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

            var webApiException = WebApiExceptionCreator.GetWebApiException(exception);
            
            if (exception != null && webApiException != null)
            {
                httpContext.Response.ContentType = "application/problem+json";
                
                var errorModel = new ErrorModel
                    {
                        Code = webApiException.Code,
                        Description = webApiException.Code == WebApiErrorCode.UnspecifiedError
                            ? exception.ToString()
                            : webApiException.Message
                    };

                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, errorModel);
            }
        }
    }
}