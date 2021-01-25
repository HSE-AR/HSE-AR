using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Floor;
using HseAr.BusinessLayer.Scene;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.WebPlatform.Api.Models.Building;
using HseAr.WebPlatform.Api.Models.Floor;
using HseAr.WebPlatform.Api.Models.Scene;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/[controller]")]
    public class FloorController : Controller
    {
        private readonly IFloorService _floorService;
        private readonly ISceneService _sceneService;
        
        public FloorController(IFloorService floorService, ISceneService sceneService)
        {
            _floorService = floorService;
            _sceneService = sceneService;

        }
        
        /// <summary>
        /// Создание этажа вмесе с пустой сценой
        /// </summary>
        /// <param name="floorCreationForm"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Floor>> Create([FromBody] Floor floorCreationForm )
        {
            return await _floorService.CreateFloor(floorCreationForm);
        }

        /// <summary>
        /// Получение сцены по id этажа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("scene/{id}")]
        [Authorize]
        public async Task<ActionResult<Scene>> GetSceneByFloorId(Guid id)
        {
            return await _sceneService.GetSceneByFloorId(id);
        }
    }
}