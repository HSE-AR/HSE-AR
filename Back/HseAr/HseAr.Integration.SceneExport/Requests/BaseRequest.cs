using HseAr.Integration.SceneExport.Models;

namespace HseAr.Integration.SceneExport.Requests
{
    public abstract class BaseRequest<TResponse> where TResponse : class
    {
        public string TokenAccess { get; set; }
        public abstract string Path { get; }
        
        public abstract object Content { get; }
    }
}