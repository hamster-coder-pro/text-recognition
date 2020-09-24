using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ImageRecognizer.Web.Services
{
    internal class RecognizeTextService: ServiceBase<RecognizeTextService>, IRecognizeTextService
    {
        private IImageUtilsProvider ImageUtils { get; }
        private ICognitiveServicesApiClient ApiClient { get; }
        private IRecognitionReporter Reporter { get; }

        public RecognizeTextService(ILogger<RecognizeTextService> logger, IImageUtilsProvider imageUtils, ICognitiveServicesApiClient apiClient, IRecognitionReporter reporter)
            : base(logger)
        {
            ImageUtils = imageUtils;
            ApiClient = apiClient;
            Reporter = reporter;
        }

        public async Task<string> ExecuteAsync(Stream stream)
        {
            byte[] byteData = ImageUtils.GetImageAsByteArray(stream);
            var operationUrl = await ApiClient.BatchAnalyzeAsync(byteData).ConfigureAwait(false);
            return await GetOperationResultAsync(operationUrl).ConfigureAwait(false);
        }

        public async Task<string> ExecuteAsync(Uri imageUri)
        {
            var operationUrl = await ApiClient.BatchAnalyzeAsync(imageUri).ConfigureAwait(false);
            return await GetOperationResultAsync(operationUrl).ConfigureAwait(false);
        }

        private async Task<string> GetOperationResultAsync(string operationResultUrl)
        {
            var operationResult = await ApiClient.ReadOperationResultAsync(operationResultUrl).ConfigureAwait(false);
            var result = await Reporter.ReportAsync(operationResult).ConfigureAwait(false);
            return result;
        }
    }
}