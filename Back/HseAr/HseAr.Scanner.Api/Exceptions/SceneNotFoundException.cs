namespace HseAr.Scanner.Api.Exceptions
{
    public class SceneNotFoundException : ScannerApiException
    {
        public SceneNotFoundException() 
            : base(ScannerApiErrorCode.SceneNotFoundError, "Не найдена сцена по указанному ключу")
        {
        }
    }
}