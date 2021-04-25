using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Data.Entities;
using HseAr.WebPlatform.Api.Models.PointCloud;

namespace HseAr.WebPlatform.Api.ViewModelConstructors
{
    public class PointCloudModelConstructor : IPointCloudModelConstructor
    {
        public PointCloudViewModel ConstructModel(PointCloudContext pcdContext, Building building, Floor floor)
        {
            string floorName = null;
            if (pcdContext.FloorId != null && building!= null && floor!= null)
            {
                floorName = $"{building.Title}, {floor.Title}";
            }

            return new PointCloudViewModel()
            {
                Id = pcdContext.Id,
                FloorId = pcdContext.FloorId,
                CompanyId = pcdContext.CompanyId,
                FloorName = floorName ?? "pcd has not floor",
                Name = pcdContext.Name,
                FilePath = pcdContext.FilePath
            };
        }
    }
}