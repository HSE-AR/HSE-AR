using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;

namespace HseAr.BusinessLayer.BuildingService
{
    public interface IBuildingService
    {
        Task<BuildingContext> CreateBuilding(BuildingContext buildingContext, string imgBase64, Guid companyId);
        
        Task<List<BuildingContext>> GetBuildingsByCompanyId(Guid companyId);
        
        Task<BuildingContext> GetBuildingById(Guid id, Guid companyId);

        Task DeleteBuilding(Guid id, Guid companyId);

        Task<BuildingContext> UpdateBuilding(BuildingContext buildingContext, string imgBase64, Guid companyId);
    }
}