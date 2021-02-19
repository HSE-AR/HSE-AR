using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.CompanyService;
using HseAr.BusinessLayer.CompanyService.Models;
using HseAr.Data.Interfaces;
using HseAr.WebPlatform.Api.Attributes;
using HseAr.WebPlatform.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Authorize]
    [Route("wapi/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Получение списка команд, доступных пользователю
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CompanyContext>>> Get()
        {
            return await _companyService.GetCompaniesByUserId(this.GetUserIdFromToken());
        }
        
    }
}