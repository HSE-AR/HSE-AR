using System;
using System.Collections.Generic;
using HseAr.Data.Entities;
using HseAr.Data.Enums;

namespace HseAr.BusinessLayer.CompanyService.Models
{
    public class CompanyContext
    {
        public Guid Id { get; set; }
        
        public TariffPlanType TariffPlan { get; set; }
        
        public Guid ArClientId { get; set; }
        
        public List<Guid> BuildingIds { get; set; } = new List<Guid>();
        
        public List<(Guid,Guid)> PositionIds { get; set; } = new List<(Guid,Guid)>();
    }
}