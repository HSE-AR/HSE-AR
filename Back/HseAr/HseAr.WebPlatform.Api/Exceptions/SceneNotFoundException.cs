namespace HseAr.WebPlatform.Api.Exceptions
{
    public class SceneNotFoundException : WebApiException
    {
        public SceneNotFoundException() 
            : base(WebApiErrorCode.SceneNotFoundError, "Не найдена сцена по указанному ключу")
        {
        }
    }
}