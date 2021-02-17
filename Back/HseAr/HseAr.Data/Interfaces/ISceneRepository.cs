using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using MongoDB.Driver;

namespace HseAr.Data.Interfaces
{
    public interface ISceneRepository
    {
        Task<List<Scene>> GetList();

        Task<Scene> GetById(string id);

        Task<Scene> Create(Scene scene);

        Task<DeleteResult> Remove(string id);

        Task<bool> ApplyModification(SceneModification sceneModification);

    }
}