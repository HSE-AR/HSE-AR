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
        
        public List<FloorEntity> FloorEntities { get; set; } = new List<FloorEntity>();
        
        public List<UserBuildingEntity> UserBuildingEntities { get; set; } = new List<UserBuildingEntity>();
    }
}