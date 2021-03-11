using System;

namespace HseAr.WebPlatform.Api.Models.Floor
{
    public class FloorDeletionForm
    {
        public Guid FloorId { get; set; }
        
        public Guid BuildingId { get; set; }
    }
}