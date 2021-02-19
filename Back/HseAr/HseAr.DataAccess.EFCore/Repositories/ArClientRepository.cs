using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Data.Entities;
using HseAr.Data.Enums;
using HseAr.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharpCompress.Archives;

namespace HseAr.DataAccess.EFCore.Repositories
{
    public class ArClientRepository : IArClientRepository
    {
        private readonly EFCoreContext _context;
        
        public ArClientRepository(EFCoreContext context)
        {
            _context = context;
        }

        public async Task<List<ArClient>> GetList()
            => await _context.ArClients.ToListAsync();

        public async Task<ArClient> GetByType(ArClientType type)
            => await _context.ArClients.FirstOrDefaultAsync(ar => ar.ArClientType == type);

        public async Task<ArClient> GetById(Guid id)
            => await _context.ArClients.Include(ar => ar.Companies)
                .FirstOrDefaultAsync(ar => ar.Id == id);
    }
}