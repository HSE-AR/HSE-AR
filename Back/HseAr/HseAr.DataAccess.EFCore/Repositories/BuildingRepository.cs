using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HseAr.DataAccess.EFCore.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly EFCoreContext _context;
        private readonly IMapper _mapper;

        public BuildingRepository(EFCoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Building>> GetList() 
            => (await _context.Buildings.AsNoTracking().ToListAsync())
                .Select(x=> _mapper.Map<BuildingEntity,Building>(x))
                .ToList();

        public async Task<Building> GetById(Guid id)
            => _mapper.Map<BuildingEntity, Building>(
                await _context.Buildings.FirstOrDefaultAsync(x => x.Id == id));

        public async Task<List<Building>> GetListByUserId(Guid userId)
        {
            var buildingIds = _context.UserBuildings
                .Where(ub => ub.UserId == userId)
                .Select(x => x.BuildingId);

            if (buildingIds.Count() == 0)
            {
                return new List<Building>();
            }
            

            return await _context.Buildings.Where(x => buildingIds.Contains(x.Id))
                .Select(x => _mapper.Map<BuildingEntity,Building>(x))
                .ToListAsync();
        }
        
        public async Task<Building> AddFromUser(Building buildingDto, Guid userId)
        {
            var building = _mapper.Map<Building, BuildingEntity>(buildingDto);
            var result = await _context.Buildings.AddAsync(building);

            await _context.UserBuildings.AddAsync(
                new UserBuildingEntity() 
                {
                    BuildingId = result.Entity.Id,
                    UserId = userId
                });
            
            await _context.SaveChangesAsync();
            
            return _mapper.Map<BuildingEntity,Building>(result.Entity);
        }
        
        public async Task Update(Building buildingDto)
        {
            var building = _mapper.Map<Building, BuildingEntity>(buildingDto);
            
            _context.Buildings.Update(building);
            await _context.SaveChangesAsync();
        }
        
        public async Task Delete(Guid id)
        {
            var building = await _context.Buildings.FirstOrDefaultAsync(x => x.Id == id);
            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();
        }
    }
}