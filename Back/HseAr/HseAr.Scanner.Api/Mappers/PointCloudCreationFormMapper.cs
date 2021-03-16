using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Infrastructure;
using HseAr.Scanner.Api.Models.PointCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HseAr.Scanner.Api.Mappers
{
    public class PointCloudCreationFormMapper : IMapper<PointCloudCreationForm, PointCloudContext>
    {
        public PointCloudContext Map(PointCloudCreationForm source)
            => new PointCloudContext()
            {
                Name = source.Name
            };
    }
}
