using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService;
using HseAr.BusinessLayer.FloorService;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.BusinessLayer.SceneService;
using HseAr.Data.DataProjections;
using HseAr.Infrastructure;
using HseAr.WebPlatform.Api.Models.Building;
using HseAr.WebPlatform.Api.Models.Floor;
using HseAr.WebPlatform.Api.ViewModelConstructors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/[controller]")]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;
        private readonly ISceneService _sceneService;
        private readonly IMapper _mapper;
        private readonly IBuildingModelConstructor _buildingConstructor;
        private readonly IBuildingService _buildingService;
        
        public FloorController(
            IFloorService floorService, 
            ISceneService sceneService,
            IMapper mapper,
            IBuildingModelConstructor buildingConstructor,
            IBuildingService buildingService)
        {
            _floorService = floorService;
            _sceneService = sceneService;
            _mapper = mapper;
            _buildingConstructor = buildingConstructor;
            _buildingService = buildingService;
        }
        
        /// <summary>
        /// Создание этажа вмесе с пустой сценой
        /// </summary>
        /// <param name="floorCreationForm"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BuildingCurrentViewModel>> Create([FromBody] FloorCreationForm floorCreationForm)
        {
            var floorContext = _mapper.Map<FloorCreationForm, FloorContext>(floorCreationForm);
            var floorResult = await _floorService.CreateFloor(floorContext);

            var buildingContext = await _buildingService.GetBuildingById(floorResult.BuildingId);
            return _buildingConstructor.ConstructCurrentModel(buildingContext);
        }

        /// <summary>
        /// Получение сцены по id этажа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("scene/{id}")]
        [Authorize]
        public async Task<ActionResult<Scene>> GetSceneByFloorId(Guid floorId)
        {
            return await _sceneService.GetSceneByFloorId(floorId);
        }
    }
}