using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Data.Enums;
using HseAr.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeHandlers.FullTextSearchHandlers;

namespace HseAr.DataAccess.EFCore.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly EFCoreContext _context;
        
        public CompanyRepository(EFCoreContext context)
        {
            _context = context;
        }

        public async Task<Company> GetById(Guid id)
            => await _context.Companies
                .Include(c=> c.Positions)
                .Include(c => c.Buildings)
                .FirstOrDefaultAsync(company => company.Id == id);

        public async Task<List<Company>> GetList()
            => await _context.Companies.ToListAsync();
        
        public async Task<List<Company>> GetListByUserId(Guid userId)
        {
            var query =
                from c in _context.Companies // List<TABLE_1>
                join p in _context.Positions
                    on c.Id equals p.CompanyId
                join u in _context.Users
                    on p.UserId equals u.Id
                    where p.UserId == userId
                select c;

            return await query.ToListAsync();
        }

        public async Task<Company> Add(Company entity)
        {
            var result =await _context.Companies.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            return result.Entity;
        }

        public async Task Update(Company entity)
        {
            var result = _context.Companies.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }
    }
}