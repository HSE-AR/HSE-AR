using System;
using System.Collections.Generic;
using HseAr.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace HseAr.Data.DataProjections
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        
        public List<UserBuildingEntity> UserBuildingEntities { get; set; } = new List<UserBuildingEntity>();
        
    }
}