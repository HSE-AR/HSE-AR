using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Infrastructure;
using HseAr.WebPlatform.Api.Attributes;
using HseAr.WebPlatform.Api.Helpers;
using HseAr.WebPlatform.Api.Models.Building;
using HseAr.WebPlatform.Api.ViewModelConstructors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HseAr.WebPlatform.Api.Controllers
{
    [Authorize]
    [AccessToCompany]
    [Route("wapi/[controller]")]
    public class BuildingController : ControllerBase
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
        /// Получение списка всех зданий доступных компании
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<BuildingsViewModel>> Get()
        {
            var companyId = this.GetCompanyId();

            var buildingContext = await _buildingService.GetBuildingsByCompanyId(companyId);
            
            return _buildingConstructor.ConstructModels(buildingContext);
        }

        /// <summary>
        /// Создание здания 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BuildingsViewModel>> Create([FromBody] BuildingCreationForm form)
        {
            var companyId = this.GetCompanyId();
            
            var buildingContext = _mapper.Map<BuildingCreationForm, BuildingContext>(form);
            await _buildingService.CreateBuilding(buildingContext, companyId);

            return _buildingConstructor.ConstructModels(await _buildingService.GetBuildingsByCompanyId(companyId));
        }
        
        /// <summary>
        /// Получение детальной информации определенного здания
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingCurrentViewModel>> Get(Guid id)
        {
            var companyId = this.GetCompanyId();
            var buildingContext = await _buildingService.GetBuildingById(id, companyId);
            
            return await _buildingConstructor.ConstructCurrentModel(buildingContext);
        }
        
        /// <summary>
        /// Удаление здания вместе с его этажами
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<BuildingsViewModel>> Delete(Guid id)
        {
            var companyId = this.GetCompanyId();
            await _buildingService.DeleteBuilding(id, companyId);
            
            return _buildingConstructor.ConstructModels(await _buildingService.GetBuildingsByCompanyId(companyId));
        }
        
    }
}