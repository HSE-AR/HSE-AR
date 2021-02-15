using System.Linq;
using HseAr.BusinessLayer.AccountService.Models;
using HseAr.Data.DataProjections;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.Mappers
{
    public class AccountContextMapper : IMapper<User,AccountContext>
    {
        public AccountContext Map(User source)
            => new AccountContext() 
            {
                Id = source.Id,
                Name = source.Name,
                Email = source.Email,
                UserBuildingIds = source.UserBuildingEntities.Select(ub => (ub.UserId, ub.BuildingEntityId))
                    .ToList()
            };
    }
}