using System;
using System.Collections.Generic;

namespace HseAr.Data.Entities
{
    public class Building
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Address { get; set; }
        
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public Guid CompanyId { get; set; }
        
        public List<Floor> Floors { get; set; } = new List<Floor>();
    }
}