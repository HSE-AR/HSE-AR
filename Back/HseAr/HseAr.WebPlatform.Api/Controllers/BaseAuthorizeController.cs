using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    public abstract class BaseAuthorizeController : ControllerBase
    {
        protected Guid GetUserIdFromToken()
        {
            var nameIdentifier = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifier == null)
            {
                throw new Exception("User not found");
            }
            
            return Guid.Parse(nameIdentifier.Value);
        }
    }
}