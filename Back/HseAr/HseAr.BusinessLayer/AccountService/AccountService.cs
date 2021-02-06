using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService.Models;
using HseAr.Data;
using HseAr.Data.DataProjections;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        
        public AccountService(IUnitOfWork data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        
        public async Task<AccountContext> GetAccountById(Guid id)
        {
            var user = await _data.Users.FindByIdAsync(id.ToString());

            return _mapper.Map<User, AccountContext>(user);
        }
    }
}