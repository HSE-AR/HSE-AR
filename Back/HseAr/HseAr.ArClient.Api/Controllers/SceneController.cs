using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.ArClient.Api.Attributes;
using HseAr.Data.Entities;
using HseAr.ArClient.Api.Helpers;
using HseAr.ArClient.Api.Models;
using HseAr.BusinessLayer.ArClientService;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.SceneService;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.ArClient.Api.Controllers
{
    [ArClientAuthorize]
    [Route("arapi/[controller]")]
    public class SceneController : ControllerBase
    {
        private readonly IArClientService _arClientService;
        
        
        public SceneController(IArClientService arClientService)
        {
            _arClientService = arClientService;
        }
        
        /// <summary>
        /// Получение url сцены по id этажа
        /// </summary>
        /// <param name="floorId"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SceneModel>> Get(Guid floorId)
        {
            return new SceneModel()
            {
                SceneUrl = await _arClientService.GetStartScene(floorId, this.GetArClientKey())
            };
            
        }
    }
}