using Microsoft.AspNetCore.Http;
using System;

namespace HseAr.BusinessLayer.PointCloudService.Models
{
    public class PointCloudContext
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid? FloorId { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }
    }
}
