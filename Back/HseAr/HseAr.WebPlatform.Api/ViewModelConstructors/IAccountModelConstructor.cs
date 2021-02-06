using HseAr.BusinessLayer.AccountService.Models;
using HseAr.WebPlatform.Api.Models.Account;

namespace HseAr.WebPlatform.Api.ViewModelConstructors
{
    public interface IAccountModelConstructor
    {
        AccountViewModel ConstructModel(AccountContext accountContext);
    }
}