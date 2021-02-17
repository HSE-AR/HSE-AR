using System.Threading;
using System.Threading.Tasks;
using HseAr.Data.Entities;

namespace HseAr.Integration.SceneExport
{
    public interface ISceneExportApiClient
    {
        Task<string> ExportScene(Scene scene);
        
    }
}