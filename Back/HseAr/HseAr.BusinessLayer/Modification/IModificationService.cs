using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DTO;

namespace HseAr.BusinessLayer.Modification
{
    public interface IModificationService
    {
        Task<bool> ModifyModel(ModificationDto modificationDto, Guid userId);

        Task<bool> ModifyModels(IEnumerable<ModificationDto> modificationDtos, Guid userId);

        Task<IEnumerable<ModificationDto>> GetAsync();
    }
}