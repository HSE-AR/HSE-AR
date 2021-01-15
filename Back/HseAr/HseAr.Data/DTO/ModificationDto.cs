using System;
using HseAr.Data.Entities;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace HseAr.Data.DTO
{
    public class ModificationDto
    {
        public string Id { get; set; }

        public DateTime EditedAtUtc { get; set; }

        public ModificationTypes Type { get; set; }

        public ObjectTypes ObjectType { get; set; }

        public ModificationTypes PropertyModificationType { get; set; }

        public JObject Object { get; set; }

        public JObject Geometry { get; set; }

        public JObject Material { get; set; }

        public JObject ObjectChild { get; set; }

        public string ModelId { get; set; }

        public ModificationDto()
        {

        }

        public ModificationDto(SceneModification sceneModification)
        {
            Id = sceneModification.Id;
            EditedAtUtc = sceneModification.EditedAtUtc;

            if (sceneModification.Object != null)
                Object = JObject.Parse(sceneModification.Object.ToJson());

            if (sceneModification.Geometry != null)
                Geometry = JObject.Parse(sceneModification.Geometry.ToJson());

            if (sceneModification.ObjectChild != null)
                ObjectChild = JObject.Parse(sceneModification.ObjectChild.ToJson());

            if (sceneModification.Material != null)
                Material = JObject.Parse(sceneModification.Material.ToJson());

            ModelId = sceneModification.ModelId;
            ObjectType = sceneModification.ObjectType;
            Type = sceneModification.Type;
            PropertyModificationType = sceneModification.PropertyModificationType;
        }
    }
}