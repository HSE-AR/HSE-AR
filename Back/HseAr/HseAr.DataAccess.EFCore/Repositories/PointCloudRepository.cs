using HseAr.Data.Entities;
using HseAr.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HseAr.DataAccess.EFCore.Repositories
{
    public class PointCloudRepository : IPointCloudRepository
    {
        private readonly EFCoreContext _context;

        public PointCloudRepository(EFCoreContext context)
        {
            _context = context;
        }

        public async Task<List<PointCloud>> Get()
            => await _context.PointClouds.AsNoTracking().ToListAsync();

        public async Task<PointCloud> GetById(Guid id)
            => await _context.PointClouds.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<PointCloud>> GetListByCompanyId(Guid companyId)
            => await _context.PointClouds.Where(cloud => cloud.CompanyId == companyId).ToListAsync();

        public async Task<PointCloud> Add(PointCloud pointCloud)
        {
            var result = await _context.PointClouds.AddAsync(pointCloud);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task Update(PointCloud pointCloud)
        {
            _context.PointClouds.Update(pointCloud);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var pointCloud = await _context.PointClouds.FirstOrDefaultAsync(x => x.Id == id);
            _context.PointClouds.Remove(pointCloud);
            await _context.SaveChangesAsync();
        }
        
    }
}
