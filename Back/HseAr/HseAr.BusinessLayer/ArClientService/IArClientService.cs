using System;
using System.Threading.Tasks;

namespace HseAr.BusinessLayer.ArClientService
{
    public interface IArClientService
    {
        Task<string> GetStartScene(Guid floorId, Guid clientKey);
    }
}