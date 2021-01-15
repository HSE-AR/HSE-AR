using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HseAr.BusinessLayer.Storage
{
    public interface IStorageService
    {
        Task<string> UploadAsync(IFormFile file, string fileName, string storagePrefix);

        void RemoveFile(string fileName);

        void RemoveFileByFullPath(string filePath);
    }
}