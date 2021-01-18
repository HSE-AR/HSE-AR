using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Auth;
using HseAr.Data.DTO;
using HseAr.WebPlatform.Api.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Login([FromBody] LoginViewModel form)
        {
            try
            {
                return new JsonResult(await _authService.Login(form.Email, form.Password));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Register([FromBody]UserDto item)
        {
            return new JsonResult(await _authService.Register(item));
        }
    }
}