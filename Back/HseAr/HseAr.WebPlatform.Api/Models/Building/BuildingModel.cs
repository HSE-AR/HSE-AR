using System;
using System.Collections.Generic;
using HseAr.WebPlatform.Api.Models.Floor;

namespace HseAr.WebPlatform.Api.Models.Building
{
    public class BuildingModel
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Address { get; set; }
        
        public string Coordinate { get; set; }
        
    }
}