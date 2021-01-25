using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using MongoDB.Driver;

namespace HseAr.Data.Interfaces
{
    public interface ISceneElementRepository
    {
        Task<UpdateResult> InsertElementToModel(SceneModification modificationEntity);

        Task<UpdateResult> DeleteElementFromScene(SceneModification sceneMod);

        Task<UpdateResult> UpdateElement(SceneModification modificationEntity);
    }
}