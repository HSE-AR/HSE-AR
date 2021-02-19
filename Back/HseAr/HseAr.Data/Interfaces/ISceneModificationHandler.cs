using MongoDB.Driver;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;

namespace HseAr.Data.Interfaces
{
    public interface ISceneModificationHandler
    {
        Task<UpdateResult> Modify(SceneModification sceneMod);

        bool CatchTypeMatch(string modificationName);
    }
}
