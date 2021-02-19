using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Core.Guard;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.BuildingService
{
    public class BuildingService : IBuildingService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        
        public BuildingService(IUnitOfWork data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
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
    }
}