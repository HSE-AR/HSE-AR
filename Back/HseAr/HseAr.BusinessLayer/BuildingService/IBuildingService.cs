﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;

namespace HseAr.BusinessLayer.BuildingService
{
    public interface IBuildingService
    {
        Task<BuildingContext> CreateBuilding(BuildingContext buildingContext, Guid companyId);
        
        Task<List<BuildingContext>> GetBuildingsByCompanyId(Guid companyId);
        
        Task<BuildingContext> GetBuildingById(Guid id, Guid companyId);
        
        
    }
}