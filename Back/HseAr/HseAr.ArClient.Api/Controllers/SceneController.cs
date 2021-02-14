using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.ArClient.Api.Attributes;
using HseAr.Data.DataProjections;
using HseAr.ArClient.Api.Helpers;
using HseAr.ArClient.Api.Models;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.SceneService;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.ArClient.Api.Controllers
{
    [ArClientAuthorize]
    [Route("arapi/[controller]")]
    public class SceneController : ControllerBase
    {
        private readonly ISceneService _sceneService;
        
        
        public SceneController(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }
        
        /// <summary>
        /// Получение url сцены по id этажа
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SceneModel>> Get(Guid floorId)
        {
            var scene = await _sceneService.GetSceneByFloorId(floorId);
            
            return new SceneModel()
            {
                SceneUrl = await _sceneService.UploadScene(scene)
            };
            
        }
    }
}