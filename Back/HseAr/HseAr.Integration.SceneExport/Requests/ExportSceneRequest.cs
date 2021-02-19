using HseAr.Data.DataProjections;
using HseAr.Integration.SceneExport.Models;

namespace HseAr.Integration.SceneExport.Requests
{
    public class ExportSceneRequest : BaseRequest<ExportScene>
    {
        private readonly Scene _scene;

        public override string Path => "/exporter/gltf";

        public override object Content => _scene;
        
        public ExportSceneRequest(Scene scene)
        {
            _scene = scene;
        }
    }
}