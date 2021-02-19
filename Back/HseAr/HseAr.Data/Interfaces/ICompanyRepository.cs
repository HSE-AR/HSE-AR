using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.Entities;
using HseAr.Data.Enums;

namespace HseAr.Data.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<Company> GetById(Guid id);

        Task<List<Company>> GetList();

        Task<List<Company>> GetListByUserId(Guid userId);

        Task<Company> Add(Company entity);

        Task Update(Company entity);

        Task Delete(Guid id);
    }
}