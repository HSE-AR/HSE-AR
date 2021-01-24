using System;
using System.Collections.Generic;

namespace HseAr.Data.Entities
{
    public class BuildingEntity
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Address { get; set; }
        
        public string Coordinate { get; set; }
        
        public List<FloorEntity> Floors { get; set; } = new List<FloorEntity>();
        
        public List<UserBuildingEntity> UserBuildings { get; set; } = new List<UserBuildingEntity>();
    }
}