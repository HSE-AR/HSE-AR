using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using MongoDB.Driver;

namespace HseAr.Data.Interfaces
{
    public interface ISceneModificationRepository
    {

        Task<SceneModification> CreateAsync(SceneModification sceneModificationEntity);

        Task<DeleteResult> RemoveAsync(string id);

    }
}