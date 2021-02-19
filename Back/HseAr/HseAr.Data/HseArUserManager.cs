using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HseAr.Data
{
    public class HseArUserManager : UserManager<User>
    {
        public HseArUserManager(
            IUserStore<User> store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, 
            IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<List<User>> GetUsersByCompanyId(Guid companyId)
        {
            return await base.Users.Include(u => u.Positions)
                .Where(u => u.Positions.Any(p => p.CompanyId == companyId)).ToListAsync();
        }
    }
}