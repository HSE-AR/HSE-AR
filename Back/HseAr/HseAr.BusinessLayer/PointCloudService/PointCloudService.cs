using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Core.Guard;
using HseAr.Core.Settings;
using HseAr.Data;
using HseAr.Data.Entities;
using HseAr.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HseAr.BusinessLayer.PointCloudService
{
    public class PointCloudService : IPointCloudService
    {
        private const string storagePointClouds = "/pointclouds/";

        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly Configuration _configuration;

        public PointCloudService(IUnitOfWork data, IMapper mapper, Configuration configuration)
        {
            _data = data;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<PointCloudContext> AddPointCloudToCompany(PointCloudContext cloudContext, IFormFile file, Guid companyId)
        {
            var company = await _data.Companies.GetById(companyId);
            Ensure.IsNotNull(company, nameof(_data.Companies.GetById));

            cloudContext.Id = Guid.NewGuid();
            cloudContext.CompanyId = companyId;

            
            var pointCloud = _mapper.Map<PointCloudContext, PointCloud>(cloudContext);

            pointCloud.FilePath = await UploadPointCloudFile(cloudContext, file);

            var cloudResult = await _data.PointClouds.Add(pointCloud);
            return _mapper.Map<PointCloud, PointCloudContext>(cloudResult);
        }

        private async Task<string> UploadPointCloudFile(PointCloudContext cloudContext, IFormFile file)
        {
            if (!file.FileName.EndsWith(".xyz"))
            {
                throw new Exception("Не поддерживаемый формат файла");
            }

            var fullPath = $"{_configuration.STORAGE_PATH}{storagePointClouds}{cloudContext.Id}.xyz";
            await using var outputFileStream = File.Create(fullPath);
            await file.CopyToAsync(outputFileStream);
            return fullPath;
        }
    }
}
