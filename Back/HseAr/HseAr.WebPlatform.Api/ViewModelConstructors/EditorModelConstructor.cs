using HseAr.BusinessLayer.FloorService.Models;
using HseAr.Data.DataProjections;
using HseAr.WebPlatform.Api.Models.Editor;

namespace HseAr.WebPlatform.Api.ViewModelConstructors
{
    public class EditorModelConstructor : IEditorModelConstructor
    {
        public EditorInfoModel ConstructInfoModel(FloorContext floorContext, Scene scene)
        {
            return new EditorInfoModel()
            {
                Scene = scene,
                FloorPlan = new FloorPlanModel()
                {
                    FloorPlanImg = floorContext.FloorPlanImg,
                    ImgHeight = floorContext.ImgHeight,
                    ImgWidth = floorContext.ImgWidth,
                    FloorPlanGltf = floorContext.FloorPlanGltf
                }
            };
        }
        
    }
}