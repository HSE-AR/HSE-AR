using System;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService.Models;

namespace HseAr.BusinessLayer.AccountService
{
    public interface IAccountService
    {
        Task<AccountContext> GetAccountById(Guid id);
    }
}