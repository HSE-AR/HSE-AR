using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Scanner.Api.Models.PointCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HseAr.Scanner.Api.ViewModelConstructors
{
    public class PointCloudModelConstructor : IPointCloudModelConstructor
    {
        public PointCloudModelConstructor()
        {

        }

        public PointCloudCurrentViewModel ConstructCurrentModel(PointCloudContext cloudContext)
            => new PointCloudCurrentViewModel
            {
                Id = cloudContext.Id,
                Name = cloudContext.Name,
                CompanyId = cloudContext.CompanyId,
                FilePath = cloudContext.FilePath
            };
    }
}
