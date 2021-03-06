using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace HseAr.BusinessLayer.Helpers
{
    public static class ImageManager
    {
        public static Image GetImage(string imageData)
        {
            var base64Str = imageData.Substring(imageData.IndexOf(',') + 1);  
            var bytes = Convert.FromBase64String(base64Str);

            return Image.FromStream(new MemoryStream(bytes));
        }

        public static ImageFormat UploadImage(Image image, string imagePath)
        {
            if (ImageFormat.Jpeg.Equals(image.RawFormat))
            {
                image.Save(imagePath + ".jpg", ImageFormat.Jpeg);
                return ImageFormat.Jpeg;
            }
            else if (ImageFormat.Png.Equals(image.RawFormat))
            {
                image.Save(imagePath + ".png", ImageFormat.Png);
                return ImageFormat.Png;
            }
                
            throw new Exception("не поддерживающий формат файла");
        }

        public static string GetFormatString(ImageFormat imageFormat)
        {
            if (ImageFormat.Jpeg.Equals(imageFormat))
            {
                return "jpg";
            }
            else if (ImageFormat.Png.Equals(imageFormat))
            {
                return "png";
            }
            
            throw new Exception("не поддерживающий формат файла");
        }
        

    }
}