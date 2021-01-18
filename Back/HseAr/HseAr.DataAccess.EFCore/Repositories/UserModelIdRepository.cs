using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.Entities;
using HseAr.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HseAr.DataAccess.EFCore.Repositories
{
    public class UserModelIdRepository : IUserModelIdRepository
    {
        private readonly EFCoreContext _context;

        public UserModelIdRepository(EFCoreContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserModelId>> GetAsync() =>
            await _context.UserModelIds.AsNoTracking().ToListAsync();

        public async Task<UserModelId> GetAsync(string modelId, Guid userId) =>
            await _context.UserModelIds.FirstOrDefaultAsync(x => x.UserId == userId && x.ModelId == modelId);

        public async Task AddAsync(UserModelId item)
        {
            await _context.UserModelIds.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserModelId item)
        {
            _context.Entry(item).State = EntityState.Detached;
            _context.UserModelIds.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}