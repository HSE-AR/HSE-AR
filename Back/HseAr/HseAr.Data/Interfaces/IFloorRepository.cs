using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.Entities;

namespace HseAr.Data.Interfaces
{
    public interface IFloorRepository
    {
        Task<List<Floor>> Get();

        Task<Floor> GetById(Guid id);

        Task<Floor> Add(Floor entity);

        Task Update(Floor entity);

        Task Delete(Guid id);
    }
}