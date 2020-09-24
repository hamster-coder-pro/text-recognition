using System.IO;

namespace ImageRecognizer.Web.Services
{
    public interface IImageUtilsProvider
    {
        byte[] GetImageAsByteArray(string imageFilePath);
        byte[] GetImageAsByteArray(Stream stream);
        byte[] GetImageAsByteArray(BinaryReader reader, int length);
    }
}