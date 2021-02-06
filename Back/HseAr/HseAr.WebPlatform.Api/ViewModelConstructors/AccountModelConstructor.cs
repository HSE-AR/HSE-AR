using HseAr.BusinessLayer.AccountService.Models;
using HseAr.WebPlatform.Api.Models.Account;

namespace HseAr.WebPlatform.Api.ViewModelConstructors
{
    public class AccountModelConstructor : IAccountModelConstructor
    {
        
        public AccountViewModel ConstructModel(AccountContext accountContext)
            => new AccountViewModel() 
            {
                Id = accountContext.Id,
                Name = accountContext.Name,
                Email = accountContext.Email,
            };
    }
}