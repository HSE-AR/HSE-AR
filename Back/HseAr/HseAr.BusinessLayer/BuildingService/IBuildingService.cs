using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.BuildingService
{
    public interface IBuildingService
    {
        Task<BuildingContext> CreateBuilding(BuildingContext buildingContext, Guid userId);
        
        Task<List<BuildingContext>> GetBuildingsByUserId(Guid userId);
        
        Task<BuildingContext> GetBuildingById(Guid id);
    }
}