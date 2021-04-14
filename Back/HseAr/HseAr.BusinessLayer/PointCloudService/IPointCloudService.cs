using HseAr.BusinessLayer.PointCloudService.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HseAr.BusinessLayer.PointCloudService
{
    public interface IPointCloudService
    {
        Task<PointCloudContext> AddPointCloudToCompany(PointCloudContext cloudContext, IFormFile file, Guid companyId);

        Task<List<PointCloudContext>> GetPointClouds(Guid companyId);

        Task DeletePointCloud(Guid pointCloudId, Guid companyId);
        
    }
}
