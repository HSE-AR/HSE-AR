using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data;
using HseAr.Data.DataProjections;
using HseAr.Data.Interfaces;
using HseAr.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace HseAr.BusinessLayer.BuildingService
{
    public class BuildingService : IBuildingService
    {
        private readonly IUnitOfWork _data;

        public BuildingService( IUnitOfWork data)
        {
            _data = data;
        }
        
        public async Task<Building> CreateBuilding(Building building, Guid userId)
        {
            return await _data.Buildings.AddFromUser(building, userId);
        }

        public async Task<List<Building>> GetBuildingsByUserId(Guid userId)
        {
            return await _data.Buildings.GetListByUserId(userId);
        }

        public async Task<Building> GetBuildingById(Guid id)
        {
            return await _data.Buildings.GetById(id);
        }
    }
}