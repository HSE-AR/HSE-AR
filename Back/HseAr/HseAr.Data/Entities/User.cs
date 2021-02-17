using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HseAr.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        
        public List<UserBuilding> UserBuildings { get; set; } = new List<UserBuilding>();
    }
}