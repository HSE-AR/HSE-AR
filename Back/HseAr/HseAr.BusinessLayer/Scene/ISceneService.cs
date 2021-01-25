using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.Scene
{
    public interface ISceneService
    {
        Task<Data.DataProjections.Scene> GetSceneByFloorId(Guid id);
        
        Task<Data.DataProjections.Scene> AddEmptyScene();
        
        Task<bool> ApplyAndSaveSceneModifications(IEnumerable<SceneModification> modifications, Guid userId);
    }
}