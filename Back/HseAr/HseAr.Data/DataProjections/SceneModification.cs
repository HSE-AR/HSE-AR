using System;
using HseAr.Data.Enums;
using Newtonsoft.Json.Linq;

namespace HseAr.Data.DataProjections
{
    public class SceneModification
    {
        public string Id { get; set; }

        public DateTime EditedAtUtc { get; set; }

        public SceneModificationType Type { get; set; }

        public SceneElementType SceneElementType { get; set; }

        public SceneModificationType PropertyModificationType { get; set; }

        public JObject Object { get; set; }

        public JObject Geometry { get; set; }

        public JObject Material { get; set; }

        public JObject ObjectChild { get; set; }

        public string ModelId { get; set; }
        
    }
}