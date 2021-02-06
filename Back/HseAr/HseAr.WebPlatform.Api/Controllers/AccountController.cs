using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService;
using HseAr.BusinessLayer.AuthService;
using HseAr.WebPlatform.Api.Models.Account;
using HseAr.WebPlatform.Api.Models.Auth;
using HseAr.WebPlatform.Api.ViewModelConstructors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/[controller]")]
    public class AccountController : BaseAuthorizeController
    {
        private readonly IAccountService _accountService;
        private readonly IAccountModelConstructor _accountConstructor;
        
        public AccountController(
            IAccountService accountService,
            IAccountModelConstructor accountConstructor)
        {
            _accountService = accountService;
            _accountConstructor = accountConstructor;
        }
        
        /// <summary>
        /// Получение информации об аккаунте 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AccountViewModel>> Get()
        {
            var userId = GetUserIdFromToken();
            var accountContext = await _accountService.GetAccountById(userId);

            return _accountConstructor.ConstructModel(accountContext);
        }
        
    }
}