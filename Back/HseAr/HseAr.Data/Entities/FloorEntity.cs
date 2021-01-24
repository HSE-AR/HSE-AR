using System;
using MongoDB.Bson;

namespace HseAr.Data.Entities
{
    public class FloorEntity
    {
        public Guid Id { get; set; }
        
        public int Number { get; set; }
        
        public string Title { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public string SceneId { get; set; }
        
        public Guid BuildingId { get; set; }
    }
}