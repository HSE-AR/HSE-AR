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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HseAr.BusinessLayer.Helpers;

namespace HseAr.BusinessLayer.PointCloudService
{
    public class PointCloudService : IPointCloudService
    {
        private const string StoragePointClouds = "/pointclouds/";

        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly Configuration _configuration;

        public PointCloudService(IUnitOfWork data, IMapper mapper, Configuration configuration)
        {
            _data = data;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task DeletePointCloud(Guid pointCloudId, Guid companyId)
        {
            var pointCloud = await _data.PointClouds.GetById(pointCloudId);
            
            Ensure.Equals(pointCloud.CompanyId,companyId,"ошибка доступа");

            await SetPcdIdInFloorAsNull(pointCloud);
            
            FileManager.DeleteFile($"{_configuration.STORAGE_PATH}{pointCloud.FilePath}");
            
            await _data.PointClouds.Delete(pointCloudId);
        }

        public async Task<List<PointCloudContext>> GetPointClouds(Guid companyId)
        {
            var company = await _data.Companies.GetById(companyId);
            return company.PointClouds.Select(pcd => _mapper.Map<PointCloud, PointCloudContext>(pcd)).ToList();
        }

        public async Task<PointCloudContext> AddPointCloudToCompany(PointCloudContext cloudContext, IFormFile file, Guid companyId)
        {
            cloudContext.Id = Guid.NewGuid();
            cloudContext.CompanyId = companyId;
            
            cloudContext.FilePath = await UploadPointCloudFile(cloudContext, file);
            
            var pointCloud = _mapper.Map<PointCloudContext, PointCloud>(cloudContext);

            var cloudResult = await _data.PointClouds.Add(pointCloud);
            return _mapper.Map<PointCloud, PointCloudContext>(cloudResult);
        }
        

        private async Task<string> UploadPointCloudFile(PointCloudContext cloudContext, IFormFile file)
        {
            if (!file.FileName.EndsWith(".xyz"))
            {
                throw new Exception("Не поддерживаемый формат файла");
            }

            var fullPath = $"{_configuration.STORAGE_PATH}{StoragePointClouds}{cloudContext.Id}.xyz";
            await using var outputFileStream = File.Create(fullPath);
            await file.CopyToAsync(outputFileStream);
            return $"{StoragePointClouds}{cloudContext.Id}.xyz";
        }
        
        private async Task SetPcdIdInFloorAsNull(PointCloud pointCloudContext)
        {
            if ( pointCloudContext.FloorId != null)
            {
                var floor = await _data.Floors.GetById((Guid)pointCloudContext.FloorId);
                Ensure.IsNotNull(floor, nameof(_data.Floors.GetById));

                floor.PointCloudId = null;
                await _data.Floors.Update(floor);
            }
        }
    }
}
