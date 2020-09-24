using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageRecognizer.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecognizeTextService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task<string> ExecuteAsync(Stream stream);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageUri"></param>
        /// <returns></returns>
        Task<string> ExecuteAsync(Uri imageUri);
    }
}