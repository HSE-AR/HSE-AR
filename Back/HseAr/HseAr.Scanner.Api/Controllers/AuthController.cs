using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AuthService;
using HseAr.Scanner.Api.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.Scanner.Api.Controllers
{
    [Route("sapi/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Login([FromBody] LoginForm form)
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

    }
}