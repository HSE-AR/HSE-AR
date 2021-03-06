using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.Data.DataProjections;
using HseAr.WebPlatform.Api.Models.Editor;

namespace HseAr.WebPlatform.Api.ViewModelConstructors
{
    public interface IEditorModelConstructor
    {
        EditorInfoModel ConstructInfoModel(FloorContext buildingContext, Scene scene);
    }
}