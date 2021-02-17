using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            => (await _context.Buildings.AsNoTracking().ToListAsync()).ToList();

        public async Task<Building> GetById(Guid id)
            => await _context.Buildings.Include(b=> b.UserBuildings).
                    FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Building>> GetListByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }
        
        public async Task<Building> AddFromUser(Building building, Guid userId)
        {
            var result = await _context.Buildings.AddAsync(building);

            await _context.UserBuildings.AddAsync(
                new UserBuilding() 
                {
                    BuildingId = result.Entity.Id,
                    UserId = userId
                });
            
            await _context.SaveChangesAsync();
            
            return result.Entity;
        }
        
        public async Task Update(Building building)
        {
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