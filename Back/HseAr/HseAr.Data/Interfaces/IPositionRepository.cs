using System;
using System.Threading.Tasks;
using HseAr.Data.Entities;

namespace HseAr.Data.Interfaces
{
    public interface IPositionRepository
    { 
        Task<Position> Add(Position position);

        Task Delete(Guid userId, Guid companyId);

        Task Update(Position position);
    }
}