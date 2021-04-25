using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.Integration.SceneExport
{
    public interface ISceneExportApiClient
    {
        Task<bool> ExportScene(Scene scene);
    }
}