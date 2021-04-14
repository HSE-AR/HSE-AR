using System;
using System.Collections.Generic;

namespace HseAr.ArClient.Api.Models
{
    public class ArPlaceModel
    {
        public Guid BuildingId { get; set; }
        
        public Guid CompanyId { get; set; }
        
        public string BuildingTitle { get; set; }

        public string Address { get; set; }
        
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
    }
}