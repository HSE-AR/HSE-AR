using System;
using HseAr.Data.Enums;

namespace HseAr.Data.Entities
{
    public class Position
    {
        public Guid UserId { get; set; }
        
        public Guid CompanyId { get; set; }
        
        public PositionType PositionType { get; set; }
    }
}