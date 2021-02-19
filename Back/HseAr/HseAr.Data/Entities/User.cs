using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HseAr.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        
        public List<Position> Positions { get; set; } = new List<Position>();
    }
}