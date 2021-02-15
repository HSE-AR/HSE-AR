using System;
using HseAr.Data.DataProjections;

namespace HseAr.Data.Entities
{
    public class UserBuildingEntity
    {
        public Guid UserId { get; set; }
        public Guid BuildingEntityId { get; set; }
    }
}