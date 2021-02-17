using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.Entities;

namespace HseAr.BusinessLayer.SceneService
{
    public interface ISceneService
    {
        Task<Scene> GetUserSceneByFloorId(Guid id, Guid userId);
        
        Task<Scene> AddEmptyScene();
        
        Task<bool> ApplyAndSaveSceneModifications(IEnumerable<SceneModification> sceneModifications, Guid userId);

        Task<string> UploadScene(Scene scene);
    }
}