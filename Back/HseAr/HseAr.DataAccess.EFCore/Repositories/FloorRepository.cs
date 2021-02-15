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
            => (await _context.Floors.AsNoTracking().ToListAsync())
                .Select(x=>_mapper.Map<FloorEntity,Floor>(x))
                .ToList();

        public async Task<Floor> GetById(Guid id)
        {
            var floorEntity = await _context.Floors.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<FloorEntity, Floor>(floorEntity);
        }

        public async Task<Floor> Add(Floor floor)
        {
            var floorEntity = _mapper.Map<Floor, FloorEntity>(floor);
            
            var result =await _context.Floors.AddAsync(floorEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<FloorEntity, Floor>(result.Entity);
        }
        
        public async Task Update(Floor floor)
        {
            var floorEntity = _mapper.Map<Floor, FloorEntity>(floor);
            
            _context.Floors.Update(floorEntity);
            await _context.SaveChangesAsync();
        }
        
        public async Task Delete(Guid id)
        {
            var floorEntity = await _context.Floors.FirstOrDefaultAsync(x => x.Id == id);
            _context.Floors.Remove(floorEntity);
            await _context.SaveChangesAsync();
        }
    }
}