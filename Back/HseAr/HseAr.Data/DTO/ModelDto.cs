using System;
using Newtonsoft.Json.Linq;

namespace HseAr.Data.DTO
{
    public class ModelDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public JObject Scene { get; set; } 
    }
}