using HseAr.Data.DataProjections;

namespace HseAr.WebPlatform.Api.Models.Editor
{
    public class EditorInfoModel
    {
        public Scene Scene { get; set; }
        
        public FloorPlanModel FloorPlan { get; set; }
    }

    public class FloorPlanModel
    {
        public string FloorPlanImg { get; set; }
        
        public int ImgWidth { get; set; }
        
        public int ImgHeight { get; set; }
        
        public string FloorPlanGltf { get; set; }
    }
    
}