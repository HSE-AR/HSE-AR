using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DTO;

namespace HseAr.BusinessLayer.Model
{
    public interface IModelService
    {
        Task CreateModelAsync(ModelDto modelDto, Guid userId);

        Task DeleteModelAsync(string id, Guid userId);

        Task<ICollection<ModelDto>> GetUserModelsAsync(Guid userId);
    }
}