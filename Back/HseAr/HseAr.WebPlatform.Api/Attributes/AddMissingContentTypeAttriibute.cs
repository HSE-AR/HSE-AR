using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HseAr.WebPlatform.Api.Attributes
{
    public class AddMissingContentType : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Request.Headers["Content-Type"] = "application/json";
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}