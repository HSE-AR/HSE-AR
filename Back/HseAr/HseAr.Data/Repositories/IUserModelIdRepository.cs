using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.Entities;

namespace HseAr.Data.Repositories
{
    public interface IUserModelIdRepository
    {
        Task AddAsync(UserModelId item);

        Task DeleteAsync(UserModelId item);

        Task<UserModelId> GetAsync(string modelId, Guid userId);

        Task<ICollection<UserModelId>> GetAsync();
    }
}