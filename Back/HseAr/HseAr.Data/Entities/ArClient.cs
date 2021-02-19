using System;
using System.Collections.Generic;
using HseAr.Data.Enums;

namespace HseAr.Data.Entities
{
    public class ArClient
    {
        public Guid Id { get; set; }

        public string Url { get; set; }
        
        public ArClientType ArClientType { get; set; }
        
        public List<Company> Companies { get; set; } = new List<Company>();
    }
}