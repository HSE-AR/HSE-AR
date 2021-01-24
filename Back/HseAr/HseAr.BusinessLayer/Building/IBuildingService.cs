using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.Building
{
    public interface IBuildingService
    {
        Task<Data.DataProjections.Building> CreateBuilding(Data.DataProjections.Building modelDto, Guid userId);
        
        Task<List<Data.DataProjections.Building>> GetBuildingsByUserId(Guid userId);
        
        Task<Data.DataProjections.Building> GetBuildingById(Guid id);
        
    }
}