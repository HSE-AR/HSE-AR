using System.Threading;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.Integration.SceneExport
{
    public interface ISceneExportApiClient
    {
        Task<string> ExportScene(Scene scene);
        
    }
}