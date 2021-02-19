using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.Entities;

namespace HseAr.Data.Interfaces
{
    public interface IBuildingRepository
    {
        Task<List<Building>> GetList();

        Task<List<Building>> GetListByCompanyId(Guid companyId);
        
        Task<Building> GetById(Guid id);

        Task<Building> Add(Building entity);

        Task Update(Building entity);

        Task Delete(Guid id);
    }
}