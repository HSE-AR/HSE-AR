using System;

namespace HseAr.WebPlatform.Api.Models.Floor
{
    public class FloorModel
    {
        public Guid Id { get; set; }
        
        public int Number { get; set; }
        
        public string Name { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        
    }
}