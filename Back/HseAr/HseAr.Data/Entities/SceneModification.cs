using System;
using HseAr.Data.Enums;
using Newtonsoft.Json.Linq;

namespace HseAr.Data.Entities
{
    public class SceneModification
    {
        public string Id { get; set; }

        public DateTime EditedAtUtc { get; set; }

        public SceneModificationType Type { get; set; }
        
        public JObject Object { get; set; }

        public string ModelId { get; set; }
    }
}