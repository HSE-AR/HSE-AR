using HseAr.Data.DataProjections;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HseAr.Data.Interfaces
{
    public interface ISceneModificationHandler<T>
    {
        Task<UpdateResult> Modify(SceneModification sceneMod);
    }
}
