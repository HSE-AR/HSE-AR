using HseAr.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HseAr.Data.Interfaces
{
    public interface IPointCloudRepository
    {
        Task<List<PointCloud>> Get();

        Task<PointCloud> GetById(Guid id);

        Task<List<PointCloud>> GetListByCompanyId(Guid CompanyId);

        Task<PointCloud> Add(PointCloud entity);

        Task Update(PointCloud entity);

        Task Delete(Guid id);
    }
}
