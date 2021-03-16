using System;

namespace HseAr.Scanner.Api.Models.PointCloud
{
    public class PointCloudCurrentViewModel
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid FloorId { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }
    }
}
