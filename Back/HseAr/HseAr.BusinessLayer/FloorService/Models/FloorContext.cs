﻿using System;

namespace HseAr.BusinessLayer.FloorService.Models
{
    public class FloorContext
    {
        public Guid Id { get; set; }
        
        public int Number { get; set; }
        
        public string Title { get; set; }
        
        public string FloorPlanImg { get; set; }
        
        public int ImgHeight { get; set; }
        
        public int ImgWidth { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public string SceneId { get; set; }
        
        public Guid BuildingId { get; set; }
    }
}