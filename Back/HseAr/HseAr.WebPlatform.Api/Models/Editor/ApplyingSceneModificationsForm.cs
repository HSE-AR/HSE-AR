using System;
using System.Collections.Generic;
using HseAr.Data.DataProjections;

namespace HseAr.WebPlatform.Api.Models.Editor
{
    public class ApplyingSceneModificationsForm
    {
        public IEnumerable<SceneModification> SceneModifications { get; set; }
        
        public Guid FloorId { get; set; }
    }
}