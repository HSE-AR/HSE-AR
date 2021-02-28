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
        
        public JObject DataJson { get; set; }

        public string SceneId { get; set; }
    }
}