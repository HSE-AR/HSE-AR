using System;
using System.Collections.Generic;

namespace HseAr.BusinessLayer.AccountService.Models
{
    public class AccountContext
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public List<(Guid,Guid)> UserBuildingIds { get; set; } = new List<(Guid, Guid)>();
        
        public Guid? ArClientKey { get; set; }
    }
}