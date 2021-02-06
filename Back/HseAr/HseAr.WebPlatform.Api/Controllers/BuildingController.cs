using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Infrastructure;
using HseAr.WebPlatform.Api.Models.Building;
using HseAr.WebPlatform.Api.ViewModelConstructors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Route("wapi/[controller]")]
    public class BuildingController : BaseAuthorizeController
    {
        private readonly IBuildingService _buildingService;
        private readonly IBuildingModelConstructor _buildingConstructor;
        private readonly IMapper _mapper;
        
        public BuildingController(
            IBuildingService buildingService, 
            IBuildingModelConstructor buildingConstructor,
            IMapper mapper)
        {
            _buildingService = buildingService;
            _buildingConstructor = buildingConstructor;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Получение списка всех зданий доступных пользователю
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<BuildingsViewModel>> GetList()
        {
            var userId = GetUserIdFromToken();
            
            var buildingContext = await _buildingService.GetBuildingsByUserId(userId);
            
            return _buildingConstructor.ConstructModels(buildingContext);
        }

        /// <summary>
        /// Создание здания 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BuildingsViewModel>> Create([FromBody] BuildingCreationForm form)
        {
            var userId = GetUserIdFromToken();
            var buildingContext = _mapper.Map<BuildingCreationForm, BuildingContext>(form);
            await _buildingService.CreateBuilding(buildingContext, userId);

            return _buildingConstructor.ConstructModels(await _buildingService.GetBuildingsByUserId(userId));
        }
        
        /// <summary>
        /// Получение детальной информации определенного здания
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BuildingCurrentViewModel>> GetByBuildingId(Guid id)
        {
            var buildingContext = await _buildingService.GetBuildingById(id);
            return _buildingConstructor.ConstructCurrentModel(buildingContext);
        }
        
    }
}