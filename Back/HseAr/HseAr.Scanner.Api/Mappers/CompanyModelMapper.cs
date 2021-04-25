using HseAr.BusinessLayer.CompanyService.Models;
using HseAr.Data.Entities;
using HseAr.Infrastructure;
using HseAr.Scanner.Api.Models;

namespace HseAr.Scanner.Api.Mappers
{
    public class CompanyModelMapper : IMapper<CompanyContext, CompanyModel>
    {
        public CompanyModel Map(CompanyContext source)
        {
            return new CompanyModel()
            {
                Id = source.Id,
                TariffPlan = source.TariffPlan
            };
        }
    }
}