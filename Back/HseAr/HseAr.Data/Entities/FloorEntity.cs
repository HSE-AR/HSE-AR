using System;
using System.ComponentModel.DataAnnotations.Schema;
using HseAr.Data.DataProjections;

namespace HseAr.Data.Entities
{
    public class FloorEntity
    {
        public Guid Id { get; set; }
        
        public int Number { get; set; }
        
        public string Title { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public string SceneId { get; set; }
        
        public Guid BuildingEntityId { get; set; }
    }
}