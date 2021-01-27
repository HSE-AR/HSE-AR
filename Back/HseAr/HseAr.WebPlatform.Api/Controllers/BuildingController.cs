using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Building;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.WebPlatform.Api.Models.Building;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/[controller]")]
    public class BuildingController : BaseAuthorizeController
    {
        private readonly IBuildingService _buildingService;
        
        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }
        
        /// <summary>
        /// Получение списка всех зданий доступных пользователю
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Building>>> GetList()
        {
            var userId = GetUserIdFromToken();
            
            return await _buildingService.GetBuildingsByUserId(userId);
        }

        /// <summary>
        /// Создание здания 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Building>> Create([FromBody] Building item)
        {
            var userId = GetUserIdFromToken();
            return await _buildingService.CreateBuilding(item, userId);
        }
        
        /// <summary>
        /// Получение детальной информации определенного здания
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Building>> GetByBuildingId(Guid id)
        {
            return await _buildingService.GetBuildingById(id);
        }
        
    }
}