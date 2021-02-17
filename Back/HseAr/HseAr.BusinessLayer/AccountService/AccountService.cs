using System;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService.Models;
using HseAr.Core.Guard;
using HseAr.Core.Settings;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly Configuration _configuration;
        
        public AccountService(IUnitOfWork data, IMapper mapper, Configuration configuration)
        {
            _data = data;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        public async Task<AccountContext> GetAccountById(Guid id)
        {
            var user = await _data.Users.FindByIdAsync(id.ToString());

            return _mapper.Map<User, AccountContext>(user);
        }

        public async Task<AccountContext> GetAccountByArClientKey(Guid arClientKey)
        {
            var arClient = _configuration.ArClients.Find(x => x.Key == arClientKey);
            Ensure.IsNotNull(arClient, nameof(arClient));
            
            var user = await _data.Users.FindByIdAsync(arClient?.UserId.ToString());
            
            return _mapper.Map<User, AccountContext>(user);
        }
    }
}