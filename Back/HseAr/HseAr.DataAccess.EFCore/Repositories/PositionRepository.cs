using System;
using System.Threading.Tasks;
using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HseAr.DataAccess.EFCore.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly EFCoreContext _context;
        
        public PositionRepository(EFCoreContext context)
        {
            _context = context;
        }

        public async Task<Position> Add(Position position)
        {
            var result = await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task Delete(Guid userId, Guid companyId)
        {
            var position = await _context.Positions
                .FirstOrDefaultAsync(p => p.UserId == userId && p.CompanyId == companyId);

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Position position)
        {
            var result = _context.Positions.Update(position);
            await _context.SaveChangesAsync();
        }
    }
}