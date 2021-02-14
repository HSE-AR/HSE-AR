using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.SceneService
{
    public interface ISceneService
    {
        Task<Scene> GetSceneByFloorId(Guid id);
        
        Task<Scene> AddEmptyScene();
        
        Task<bool> ApplyAndSaveSceneModifications(IEnumerable<SceneModification> sceneModifications, Guid userId);

        Task<string> UploadScene(Scene scene);
    }
}