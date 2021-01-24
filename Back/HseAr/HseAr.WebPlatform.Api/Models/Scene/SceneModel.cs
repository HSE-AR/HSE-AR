using Newtonsoft.Json.Linq;

namespace HseAr.WebPlatform.Api.Models.Scene
{
    public class SceneModel
    {
        public string Id { get; set; }

        public JObject Metadata { get; set; }

        public JObject Geometries { get; set; }

        public JObject Materials { get; set; }

        public JObject Object { get; set; }
    }
}