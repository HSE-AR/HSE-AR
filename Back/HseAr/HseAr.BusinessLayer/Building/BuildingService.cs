using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Interfaces;
using HseAr.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace HseAr.BusinessLayer.Building
{
    public class BuildingService : IBuildingService
    {
        private readonly IMapper _mapper;
        private readonly IBuildingRepository _buildingRepo;

        public BuildingService(IMapper mapper, IBuildingRepository buildingRepo)
        {
            _mapper = mapper;
            _buildingRepo = buildingRepo;
   
        }
        
        public async Task<Data.DataProjections.Building> CreateBuilding(Data.DataProjections.Building building, Guid userId)
        {
            return await _buildingRepo.AddFromUser(building, userId);
        }

        public async Task<List<Data.DataProjections.Building>> GetBuildingsByUserId(Guid userId)
        {
            return await _buildingRepo.GetListByUserId(userId);
        }

        public async Task<Data.DataProjections.Building> GetBuildingById(Guid id)
        {
            return await _buildingRepo.GetById(id);
        }
    }
}