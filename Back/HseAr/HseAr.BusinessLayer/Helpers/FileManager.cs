using System.IO;

namespace HseAr.BusinessLayer.Helpers
{
    public static class FileManager
    {
        public static bool DeleteFile(string imagePath)
        {
            if(File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return true;
            }

            return false;
        }
    }
}