using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using MongoDB.Driver;

namespace HseAr.Data.Interfaces
{
    public interface IBuildingRepository
    {
        Task<List<Building>> GetList();

        Task<List<Building>> GetListByUserId(Guid userId);
        
        Task<Building> GetById(Guid id);

        Task<Building> AddFromUser(Building entity, Guid id);

        Task Update(Building entity);

        Task Delete(Guid id);
    }
}