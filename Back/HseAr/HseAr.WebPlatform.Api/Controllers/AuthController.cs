﻿using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AuthService;
using HseAr.WebPlatform.Api.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/[controller]/[action]")]
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

        /// <summary>
        /// Регистрация пользователя, автоматически добавляется приватная команда использующая WebArClient 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Register([FromBody]RegisterForm form)
        {
            return new JsonResult(await _authService.Register(
                    form.Email,
                    form.Password,
                    form.Name));
        }
    }
}