using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.Modification
{
    public interface IModificationService
    {
        Task<bool> ModifyModel(SceneModification sceneModificationDto, Guid userId);

        Task<bool> ModifyModels(IEnumerable<SceneModification> modificationDtos, Guid userId);

        Task<IEnumerable<SceneModification>> GetAsync();
    }
}