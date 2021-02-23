using MongoDB.Driver;
using System.Threading.Tasks;
using HseAr.Data.DataProjections;
using HseAr.Data.Enums;

namespace HseAr.Data.Interfaces
{
    public interface ISceneModificationHandler
    {
        Task<UpdateResult> Modify(SceneModification sceneMod);

        bool CatchTypeMatch(SceneModificationType modificationType);
    }
}
