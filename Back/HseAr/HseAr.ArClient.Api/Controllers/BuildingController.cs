using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.ArClient.Api.Attributes;
using HseAr.ArClient.Api.Helpers;
using HseAr.BusinessLayer.AccountService;
using HseAr.BusinessLayer.BuildingService;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.SceneService;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.ArClient.Api.Controllers
{
    [ArClientAuthorize]
    [Route("arapi/[controller]")]
    public class BuildingController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IBuildingService _buildingService;
        
        public BuildingController(IAccountService accountService, IBuildingService buildingService)
        {
            _accountService = accountService;
            _buildingService = buildingService;
        }
        
        /// <summary>
        /// Получение зданий с ids этажей по токену AR клиента 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<BuildingContext>>> Get()
        {
            var account = await _accountService.GetAccountByArClientKey(this.GetArClientKey());

            return await _buildingService.GetBuildingsByUserId(account.Id);
        }
    }
}