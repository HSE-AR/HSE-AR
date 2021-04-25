using System;

namespace HseAr.WebPlatform.Api.Models.PointCloud
{
    public class PointCloudViewModel
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid? FloorId { get; set; }
        
        public string FloorName { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }
    }
}