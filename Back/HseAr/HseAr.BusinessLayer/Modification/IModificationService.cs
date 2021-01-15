using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.Data.DTO;

namespace HseAr.BusinessLayer.Modification
{
    public interface IModificationService
    {
        Task<bool> ModifyModel(ModificationDto modificationDto);

        Task<bool> ModifyModels(IEnumerable<ModificationDto> modificationDtos);

        Task<IEnumerable<ModificationDto>> GetAsync();
    }
}