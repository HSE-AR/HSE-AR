using System;

namespace HseAr.Data.Entities
{
    public class Floor
    {
        public Guid Id { get; set; }
        
        public int Number { get; set; }
        
        public string Title { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public string SceneId { get; set; }
        
        public Guid BuildingId { get; set; }

        public string FloorPlanImg { get; set; }
        
        public int ImgHeight { get; set; }
        
        public int ImgWidth { get; set; }
    }
}
