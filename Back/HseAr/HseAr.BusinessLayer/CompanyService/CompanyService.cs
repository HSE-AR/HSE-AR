using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HseAr.BusinessLayer.CompanyService.Models;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Data.Enums;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        
        public CompanyService(IUnitOfWork data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        
        public async Task<CompanyContext> CreateOwnCompany(Guid userId, Guid? arClientId)
        {
            var result = await _data.Companies.Add(
                new Company() 
                {
                    TariffPlan = TariffPlanType.OwnTariff, 
                    ArClientId = arClientId ?? (await _data.ArClients.GetByType(ArClientType.WebAr)).Id
                });

            await _data.Positions.Add(new Position()
            {
                UserId = userId,
                CompanyId = result.Id,
                PositionType = PositionType.Admin
            });

            return _mapper.Map<Company, CompanyContext>(result);
        }
        

        public async Task<List<CompanyContext>> GetCompaniesByUserId(Guid userId)
        {
            var companies = await _data.Companies.GetListByUserId(userId);
            return companies.Select(company => _mapper.Map<Company, CompanyContext>(company)).ToList();
        }
    }
}