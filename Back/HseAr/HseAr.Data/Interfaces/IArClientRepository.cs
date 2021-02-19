using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.Entities;
using HseAr.Data.Enums;

namespace HseAr.Data.Interfaces
{
    public interface IArClientRepository
    {
        Task<List<ArClient>> GetList();

        Task<ArClient> GetById(Guid id);
        
        Task<ArClient> GetByType(ArClientType type);
    }
}