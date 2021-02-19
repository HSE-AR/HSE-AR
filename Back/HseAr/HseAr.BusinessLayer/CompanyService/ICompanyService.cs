using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HseAr.BusinessLayer.AccountService.Models;
using HseAr.BusinessLayer.CompanyService.Models;
using HseAr.Data.Entities;

namespace HseAr.BusinessLayer.CompanyService
{
    public interface ICompanyService
    {
        Task<CompanyContext> CreateOwnCompany(Guid userId, Guid? arClientId);
        
        Task<List<CompanyContext>> GetCompaniesByUserId(Guid userId);
    }
}