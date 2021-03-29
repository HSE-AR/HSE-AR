using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.Helpers;
using HseAr.Core.Guard;
using HseAr.Core.Settings;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.BuildingService
{
    public class BuildingService : IBuildingService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly Configuration _configuration;
        
        public BuildingService(IUnitOfWork data, IMapper mapper, Configuration configuration)
        {
            _data = data;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        public async Task<BuildingContext> CreateBuilding(BuildingContext buildingContext, Guid companyId)
        {
            buildingContext.CompanyId = companyId;
            
            var building = _mapper.Map<BuildingContext, Building>(buildingContext);
            var buildingResult = await _data.Buildings.Add(building);
            return _mapper.Map<Building, BuildingContext>(buildingResult);
        }

        public async Task<List<BuildingContext>> GetBuildingsByCompanyId(Guid companyId)
        {
            var buildings = await _data.Buildings.GetListByCompanyId(companyId);
            return buildings.Select(building => _mapper.Map<Building, BuildingContext>(building)).ToList();
        }

        public async Task<BuildingContext> GetBuildingById(Guid buildingId, Guid companyId)
        {
            var building = await _data.Buildings.GetById(buildingId);
            
            Ensure.IsNotNull(building, nameof(building));
            Ensure.Equals(companyId,building.CompanyId, nameof(building));

            return _mapper.Map<Building, BuildingContext>(building);
        }

        public async Task DeleteBuilding(Guid id, Guid companyId)
        {
            var building = await _data.Buildings.GetById(id);
            Ensure.IsNotNull(building, nameof(building));
            Ensure.Equals(building.CompanyId, companyId, nameof(DeleteBuilding));
            
            foreach (var floor in building.Floors)
            {
                await SetFloorIdInPointCloud(floor.PointCloudId, null);
                FileManager.DeleteFile($"{_configuration.STORAGE_PATH}{floor.FloorPlanImg}");
                await _data.Scenes.Remove(floor.SceneId);
            }
            
            await _data.Buildings.Delete(building.Id);
        }
        
        private async Task SetFloorIdInPointCloud(Guid? pcdId, Guid? newValue)
        {
            if (pcdId != null)
            {
                var pointCloud = await _data.PointClouds.GetById((Guid) pcdId);
                Ensure.IsNotNull(pointCloud, nameof(_data.PointClouds.GetById));
                
                pointCloud.FloorId = newValue;
                await _data.PointClouds.Update(pointCloud);
            }
        }
    }
}