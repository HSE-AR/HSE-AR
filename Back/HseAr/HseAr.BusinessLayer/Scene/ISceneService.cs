using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.BusinessLayer.Scene
{
    public interface ISceneService
    {
        Task<Data.DataProjections.Scene> AddEmptyScene();
    }
}