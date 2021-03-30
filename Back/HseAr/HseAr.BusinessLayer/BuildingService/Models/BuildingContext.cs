using System;
using System.Collections.Generic;

namespace HseAr.BusinessLayer.BuildingService.Models
{
    public class BuildingContext
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Address { get; set; }
        
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public Guid CompanyId { get; set; }
        
        public List<Guid> FloorIds { get; set; } =new List<Guid>();
    }
}