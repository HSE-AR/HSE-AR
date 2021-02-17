using HseAr.Data.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace HseAr.Data.Interfaces
{
    public interface ISceneModificationHandler
    {
        Task<UpdateResult> Modify(SceneModification sceneMod);

        bool CatchTypeMatch(string modificationName);
    }
}
