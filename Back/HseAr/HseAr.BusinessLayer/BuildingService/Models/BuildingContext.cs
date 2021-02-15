using System;
using System.Collections.Generic;

namespace HseAr.BusinessLayer.BuildingService.Models
{
    public class BuildingContext
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Address { get; set; }
        
        public string Coordinate { get; set; }
        
        public List<(Guid,Guid)> UserBuildingIds { get; set; } = new List<(Guid, Guid)>();
        public List<Guid> FloorIds { get; set; } =new List<Guid>();
    }
}