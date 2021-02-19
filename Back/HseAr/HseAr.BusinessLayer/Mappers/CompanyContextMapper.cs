using System.Linq;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.CompanyService.Models;
using HseAr.Data.Entities;
using HseAr.Infrastructure;

namespace HseAr.BusinessLayer.Mappers
{
    public class CompanyContextMapper : IMapper<Company,CompanyContext>, IMapper<CompanyContext,Company>
    {
        public CompanyContext Map(Company source)
            => new CompanyContext() 
            {
                Id = source.Id,
                TariffPlan = source.TariffPlan,
                ArClientId = source.ArClientId,
                BuildingIds = source.Buildings.Select(b =>b.Id).ToList(),
                PositionIds = source.Positions.Select(p => (p.UserId,p.CompanyId)).ToList()
            };
        
        public Company Map(CompanyContext source)
            => new Company() 
            {
                Id = source.Id,
                TariffPlan = source.TariffPlan,
                ArClientId = source.ArClientId,
            };
        
    }
}