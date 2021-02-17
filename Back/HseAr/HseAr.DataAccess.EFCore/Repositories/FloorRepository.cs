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
    public class FloorRepository : IFloorRepository 
    {
        private readonly EFCoreContext _context;
        private readonly IMapper _mapper;

        public FloorRepository(EFCoreContext context,IMapper mapper)
        {    
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Floor>> Get() 
            => (await _context.Floors.AsNoTracking().ToListAsync()).ToList();

        public async Task<Floor> GetById(Guid id)
            => await _context.Floors.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Floor> Add(Floor floor)
        {
            var result =await _context.Floors.AddAsync(floor);
            await _context.SaveChangesAsync();

            return result.Entity;
        }
        
        public async Task Update(Floor floor)
        {
            _context.Floors.Update(floor);
            await _context.SaveChangesAsync();
        }
        
        public async Task Delete(Guid id)
        {
            var floor = await _context.Floors.FirstOrDefaultAsync(x => x.Id == id);
            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync();
        }
    }
}