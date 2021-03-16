using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Data.Entities;
using HseAr.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HseAr.BusinessLayer.Mappers
{
    public class PointCloudContextMapper : 
        IMapper<PointCloud, PointCloudContext>, IMapper<PointCloudContext, PointCloud>
    {
        public PointCloudContext Map(PointCloud source)
            => new PointCloudContext()
            {
                Id = source.Id,
                Name = source.Name,
                CompanyId = source.CompanyId,
                FloorId = source.FloorId,
                FilePath = source.FilePath
            };

        public PointCloud Map(PointCloudContext source)
            => new PointCloud()
            {
                Id = source.Id,
                Name = source.Name,
                CompanyId = source.CompanyId,
                FloorId = source.FloorId,
                FilePath = source.FilePath
            };
    }
}
