using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace ImageRecognizer.Web.Services
{
    internal sealed class ImageUtilsProvider: ServiceBase<ImageUtilsProvider>, IImageUtilsProvider
    {
        public ImageUtilsProvider(ILogger<ImageUtilsProvider> logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        public byte[] GetImageAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using var fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            return GetImageAsByteArray(fileStream);
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>The byte array of the image data.</returns>
        public byte[] GetImageAsByteArray(Stream stream)
        {
            stream = stream ?? throw new ArgumentNullException(nameof(stream));
            // Read the file's contents into a byte array.
            using var binaryReader = new BinaryReader(stream);
            return GetImageAsByteArray(binaryReader, (int) stream.Length);
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="length"></param>
        /// <returns>The byte array of the image data.</returns>
        public byte[] GetImageAsByteArray(BinaryReader reader, int length)
        {
            reader = reader ?? throw new ArgumentNullException(nameof(reader));
            return reader.ReadBytes(length);
        }
    }
}