using System;
using System.Collections.Generic;
using HseAr.Data.Enums;

namespace HseAr.Data.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        
        public TariffPlanType TariffPlan { get; set; }
        
        public Guid ArClientId { get; set; }

        public List<Building> Buildings { get; set; } = new List<Building>();
        
        public List<Position> Positions { get; set; } = new List<Position>();

        public List<PointCloud> PointClouds { get; set; } = new List<PointCloud>();
    }
}