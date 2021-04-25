using System.Threading.Tasks;
using HseAr.BusinessLayer.BuildingService.Models;
using HseAr.BusinessLayer.FloorService.Models;
using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Data.Entities;
using HseAr.WebPlatform.Api.Models.PointCloud;

namespace HseAr.WebPlatform.Api.ViewModelConstructors
{
    public interface IPointCloudModelConstructor
    {
        PointCloudViewModel ConstructModel(PointCloudContext buildingContext, Building building, Floor floor);
    }
}