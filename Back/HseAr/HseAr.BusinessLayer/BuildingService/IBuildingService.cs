using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;

namespace HseAr.BusinessLayer.BuildingService
{
    public interface IBuildingService
    {
        Task<BuildingContext> CreateBuilding(BuildingContext buildingContext, Guid userId);
        
        Task<List<BuildingContext>> GetBuildingsByUserId(Guid userId);
        
        Task<BuildingContext> GetUserBuildingById(Guid id, Guid userId);
        
        
    }
}