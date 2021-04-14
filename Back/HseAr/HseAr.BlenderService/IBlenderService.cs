using System.Threading.Tasks;

namespace HseAr.BlenderService
{
    public interface IBlenderService
    {
        Task CreateFloorplanGltf(string imgPath, string newFloorplanGltfPath);
    }
}