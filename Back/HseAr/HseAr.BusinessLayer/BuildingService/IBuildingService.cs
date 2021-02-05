using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.BuildingService
{
    public interface IBuildingService
    {
        Task<Building> CreateBuilding(Building modelDto, Guid userId);
        
        Task<List<Building>> GetBuildingsByUserId(Guid userId);
        
        Task<Building> GetBuildingById(Guid id);
        
    }
}