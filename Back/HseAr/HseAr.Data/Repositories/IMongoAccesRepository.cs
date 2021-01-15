using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace HseAr.Data.Repositories
{
    public interface IMongoAccessRepository<T>
    {
        Task<ICollection<T>> GetAsync();

        Task<T> GetAsync(string id);

        Task<T> CreateAsync(T entity);

        Task<ReplaceOneResult> UpdateAsync(T entityIn);

        Task<DeleteResult> RemoveAsync(T entityIn);

        Task<DeleteResult> RemoveAsync(string id);
    }
}