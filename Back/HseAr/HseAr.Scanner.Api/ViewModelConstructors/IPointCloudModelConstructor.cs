using HseAr.BusinessLayer.PointCloudService.Models;
using HseAr.Scanner.Api.Models.PointCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HseAr.Scanner.Api.ViewModelConstructors
{
    public interface IPointCloudModelConstructor
    {
        PointCloudCurrentViewModel ConstructCurrentModel(PointCloudContext cloudContext);
    }
}
