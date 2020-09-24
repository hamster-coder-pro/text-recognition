using System;
using System.Threading.Tasks;
using ImageRecognizer.Web.Models;

namespace ImageRecognizer.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICognitiveServicesApiClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteData"></param>
        /// <returns></returns>
        Task<string> BatchAnalyzeAsync(byte[] byteData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageUri"></param>
        /// <returns></returns>
        Task<string> BatchAnalyzeAsync(Uri imageUri);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultUrl"></param>
        /// <returns></returns>
        Task<OperationResultModel> ReadOperationResultAsync(string resultUrl);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultUrl"></param>
        /// <param name="tries"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        Task<OperationResultModel> ReadOperationResultAsync(string resultUrl, int tries, int interval);
    }
}