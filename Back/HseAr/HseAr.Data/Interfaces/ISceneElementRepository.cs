using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using MongoDB.Driver;

namespace HseAr.Data.Interfaces
{
    public interface ISceneElementRepository
    {
        Task<UpdateResult> InsertElementToModel(SceneModification sceneMod);

        Task<UpdateResult> DeleteElementFromScene(SceneModification sceneMod);

        Task<UpdateResult> UpdateElement(SceneModification sceneMod);
    }
}