using Newtonsoft.Json.Linq;

namespace HseAr.Data.DataProjections
{
    public class Scene
    {
        public string Id { get; set; }

        public JObject Metadata { get; set; }

        public JArray Geometries { get; set; }

        public JArray Materials { get; set; }

        public JObject Object { get; set; }
    }
}