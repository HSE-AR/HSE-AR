using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;

namespace HseAr.BusinessLayer.SceneService
{
    public interface ISceneService
    {
        Task<Scene> GetSceneByFloorId(Guid id, Guid companyId);
        
        Task<Scene> AddEmptyScene();
        
        Task<bool> ApplyAndSaveSceneModifications(IEnumerable<SceneModification> sceneModifications, Guid floorId, Guid companyId);

        Task UploadScene(Scene scene, Floor floor);
    }
}