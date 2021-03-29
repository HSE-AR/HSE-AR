using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HseAr.Data.Entities
{
    public class Floor
    {
        [NotMapped]
        private const string GltfScenesStore = "/scenes/gltfs/";
        
        public Guid Id { get; set; }

        public int Number { get; set; }
        
        public string Title { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public string SceneId { get; set; }
        
        public Guid BuildingId { get; set; }
        
        public Guid? PointCloudId { get; set; }

        public string FloorPlanImg { get; set; }
        
        public int ImgHeight { get; set; }
        
        public int ImgWidth { get; set; }
        
        public bool IsLatestVersion { get; set; }

        [NotMapped]
        public string GltfScene => GltfScenesStore + SceneId +".gltf";
    }
}
