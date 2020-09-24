using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ImageRecognizer.Web.Exceptions;
using ImageRecognizer.Web.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ImageRecognizer.Web.Services
{
    internal sealed class CognitiveServicesApiClient : ServiceBase<CognitiveServicesApiClient>, ICognitiveServicesApiClient
    {
        private HttpClient HttpClient { get; }

        public CognitiveServicesApiClient(ILogger<CognitiveServicesApiClient> logger, IHttpClientFactory httpClientFactory)
            : base(logger)
        {
            HttpClient = httpClientFactory.CreateClient(HttpClients.CognitiveServices);
        }

        public async Task<string> BatchAnalyzeAsync(byte[] byteData)
        {
            using var content = new ByteArrayContent(byteData);
            // This example uses the "application/octet-stream" content type.
            // The other content types you can use are "application/json"
            // and "multipart/form-data".
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return await BatchAnalyzeAsync(content);
        }

        public async Task<string> BatchAnalyzeAsync(Uri imageUri)
        {
            var model = new BatchAnalyzeUrlModel() {ImageUrl = imageUri};
            using var content = new StringContent(JsonConvert.SerializeObject(model));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await BatchAnalyzeAsync(content);
        }

        private async Task<string> BatchAnalyzeAsync(HttpContent httpContent)
        {
            var response = await HttpClient.PostAsync("vision/v2.0/read/core/asyncBatchAnalyze", httpContent).ConfigureAwait(false);

            // The response header for the Batch Read method contains the URI
            // of the second method, Read Operation Result, which
            // returns the results of the process in the response body.
            // The Batch Read operation does not return anything in the response body.
            if (response.IsSuccessStatusCode)
            {
                return response.Headers.GetValues("Operation-Location").FirstOrDefault();
            }
            else
            {
                string errorJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var errorModel = JsonConvert.DeserializeObject<CognitiveServicesErrorModel>(errorJson);
                throw new ApplicationCodeException(errorModel.Code, errorModel.Message);
            }
        }


        public async Task<OperationResultModel> ReadOperationResultAsync(string operationLocation, int tries, int interval)
        {
            int i = 0;
            do
            {
                Logger.LogDebug($"ReadOperationResultAsync. Try #{i}");
                await Task.Delay(interval).ConfigureAwait(false);
                var response = await HttpClient.GetAsync(operationLocation).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = JsonConvert.DeserializeObject<OperationResultModel>(json);
                    Logger.LogDebug($"ReadOperationResultAsync. Status => {result.Status}");
                    switch (result.Status)
                    {
                        case RecognitionResultEnum.Succeeded:
                            return result;
                        case RecognitionResultEnum.Failed:
                            throw new ApplicationCodeException("RecognitionFailed", "The text recognition process failed.");
                        default:

                            break;
                    }
                }
            } while (++i < tries);

            throw new ApplicationCodeException("Timeout", "The text recognition timeout.");
        }

        public Task<OperationResultModel> ReadOperationResultAsync(string operationLocation)
        {
            return ReadOperationResultAsync(operationLocation, 10, 1000);
        }
    }

    internal class BatchAnalyzeUrlModel 
    {
        [JsonProperty("url")]
        public Uri ImageUrl { get; set; }
    }
}