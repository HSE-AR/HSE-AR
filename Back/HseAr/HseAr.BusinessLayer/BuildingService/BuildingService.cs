﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.Data;
using HseAr.Data.DataProjections;
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
        
        public async Task<BuildingContext> CreateBuilding(BuildingContext buildingContext, Guid userId)
        {
            var building = _mapper.Map<BuildingContext, Building>(buildingContext);
            var buildingResult = await _data.Buildings.AddFromUser(building, userId);
            return _mapper.Map<Building, BuildingContext>(buildingResult);
        }

        public async Task<List<BuildingContext>> GetBuildingsByUserId(Guid userId)
        {
            var buildings =await _data.Buildings.GetListByUserId(userId);
            return buildings.Select(building => _mapper.Map<Building, BuildingContext>(building)).ToList();
        }

        public async Task<BuildingContext> GetBuildingById(Guid id)
        {
            var buildingResult = await _data.Buildings.GetById(id);
            return _mapper.Map<Building, BuildingContext>(buildingResult);
        }
    }
}