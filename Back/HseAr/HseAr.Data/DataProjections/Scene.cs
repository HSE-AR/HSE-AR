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
        
        public JArray Textures { get; set; }
        
        public JArray Images { get; set; }
        
        public JArray Animations { get; set; }
    }
}