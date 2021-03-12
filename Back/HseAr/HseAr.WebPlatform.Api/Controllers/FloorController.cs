using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService;
using HseAr.BusinessLayer.FloorService;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.BusinessLayer.SceneService;
using HseAr.Data;
using HseAr.Data.DataProjections;
using HseAr.Infrastructure;
using HseAr.WebPlatform.Api.Attributes;
using HseAr.WebPlatform.Api.Helpers;
using HseAr.WebPlatform.Api.Models.Building;
using HseAr.WebPlatform.Api.Models.Floor;
using HseAr.WebPlatform.Api.ViewModelConstructors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [AccessToCompany]
    [Route("wapi/[controller]")]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly IBuildingModelConstructor _buildingConstructor;
        private readonly IBuildingService _buildingService;
        
        public FloorController(
            IFloorService floorService,
            IUnitOfWork data,
            IMapper mapper,
            IBuildingModelConstructor buildingConstructor,
            IBuildingService buildingService)
        {
            _floorService = floorService;
            _mapper = mapper;
            _buildingConstructor = buildingConstructor;
            _buildingService = buildingService;
            _data = data;
        }
        
        /// <summary>
        /// Создание этажа вместе с пустой сценой
        /// </summary>
        /// <param name="floorCreationForm"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BuildingCurrentViewModel>> Create([FromBody] FloorCreationForm floorCreationForm)
        {
            var floorContext = _mapper.Map<FloorCreationForm, FloorContext>(floorCreationForm);
            
            var floorResult = await _floorService.CreateFloor(
                floorContext,
                floorCreationForm.FloorPlanImg,
                this.GetCompanyId());
            
            var buildingContext = await _buildingService.GetBuildingById(floorResult.BuildingId, this.GetCompanyId());
            return await _buildingConstructor.ConstructCurrentModel(buildingContext);
        }
        
        /// <summary>
        /// Удаление этажа
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AddMissingContentType]
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<BuildingCurrentViewModel>> Delete([FromBody] FloorDeletionForm request)
        {
            await _floorService.DeleteFloor(
                    request.FloorId,
                this.GetCompanyId());
            
            var buildingContext = await _buildingService.GetBuildingById(request.BuildingId, this.GetCompanyId());
            return await _buildingConstructor.ConstructCurrentModel(buildingContext);
        }
    }
}