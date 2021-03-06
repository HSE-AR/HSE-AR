using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService;
using HseAr.BusinessLayer.CompanyService;
using HseAr.Core.Settings;
using HseAr.Data;
using HseAr.WebPlatform.Api.Exceptions;
using HseAr.WebPlatform.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HseAr.WebPlatform.Api.Attributes
{
    public class AccessToCompanyAttribute : TypeFilterAttribute
    {
        public AccessToCompanyAttribute() : base(typeof(AccessToCompanyFilter))
        {
        }
        
        public class AccessToCompanyFilter : IAsyncActionFilter
        {
            private readonly IUnitOfWork _data;
            
            
            public AccessToCompanyFilter(IUnitOfWork data)
            {
                _data = data;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, 
                ActionExecutionDelegate next)
            {
                var httpContext = context.HttpContext;
                
                var nameIdentifier = httpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (nameIdentifier == null)
                {
                    throw new Exception("User not found");
                }
            
                var userId = Guid.Parse(nameIdentifier.Value);
                
                if (httpContext.Request.Headers.TryGetValue(WebApiHeaders.CompanyKey, out var companyKey)
                    && !string.IsNullOrEmpty(companyKey.First()))
                {
                    if(Guid.TryParse(companyKey, out var companyId) )
                    {
                        var company = await _data.Companies.GetById(companyId);
                        if (company.Positions.Any(u => u.UserId == userId))
                        {
                            await next();
                        }
                    }
                }
                
                context.Result = new UnauthorizedObjectResult("не указан ключ компании");
            }
            
            /*public void OnActionExecuted(ActionExecutedContext context)
            {
            }*/
        }
    }
}