using System;
using HseAr.Data.Enums;

namespace HseAr.Scanner.Api.Models
{
    public class CompanyModel
    {
        public Guid Id { get; set; }
        
        public TariffPlanType TariffPlan { get; set; }
    }
}