using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Entities;
using MongoDB.Driver;

namespace HseAr.Data.Interfaces
{
    public interface ISceneModificationRepository
    {

        Task<SceneModification> CreateAsync(SceneModification sceneModificationEntity);

        Task<DeleteResult> RemoveAsync(string id);

    }
}