using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HseAr.Data.Entities
{
    public class PointCloud
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid? FloorId { get; set; } 

        public string Name { get; set; }

        public string FilePath { get; set; }
    }
}
