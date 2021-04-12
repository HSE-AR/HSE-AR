using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using HseAr.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HseAr.DataAccess.EFCore.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly EFCoreContext _context;

        public BuildingRepository(EFCoreContext context)
        {
            _context = context;
        }

        public async Task<List<Building>> GetList() 
            => await _context.Buildings.AsNoTracking().ToListAsync();

        public async Task<Building> GetById(Guid id)
            => await _context.Buildings.Include(b => b.Floors).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Building>> GetListByCompanyId(Guid companyId)
            => await _context.Buildings
                .Where(building => building.CompanyId == companyId)
                .ToListAsync();
        
        public async Task<List<Building>> GetListWithFloorsByCompanyId(Guid companyId)
            => await _context.Buildings
                .Include(b => b.Floors)
                .Where(building => building.CompanyId == companyId)
                .ToListAsync();
        

  
        public async Task<Building> Add(Building building)
        {
            var result = await _context.Buildings.AddAsync(building);
            await _context.SaveChangesAsync();
            
            return result.Entity;
        }
        
        public async Task<Building> Update(Building building)
        {
            var result = _context.Buildings.Update(building);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        
        public async Task Delete(Guid id)
        {
            var building = await _context.Buildings.FirstOrDefaultAsync(x => x.Id == id);
            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();
        }
    }
}