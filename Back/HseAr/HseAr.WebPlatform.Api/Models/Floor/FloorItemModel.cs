using System;

namespace HseAr.WebPlatform.Api.Models.Floor
{
    public class FloorItemModel
    {
        public Guid Id { get; set; }
        
        public int Number { get; set; }
        
        public string Title { get; set; }
        
        public string FloorPlanImage { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public string SceneId { get; set; }
    }
}