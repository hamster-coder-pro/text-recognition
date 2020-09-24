using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageRecognizer.Web.Models;
using ImageRecognizer.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ImageRecognizer.Web.Controllers
{
    [Route("recognition")]
    public class RecognitionController: ControllerBase
    {
        private IRecognizeTextService RecognizeService { get; }

        public RecognitionController(IRecognizeTextService recognizeService)
        {
            RecognizeService = recognizeService;
        }

        [HttpPost("file")]
        public async Task<string> RecognizeAsync(IFormFile file)
        {
            await using var stream = file.OpenReadStream();
            return await RecognizeService.ExecuteAsync(stream);
        }

        [HttpPost("url")]
        public async Task<string> RecognizeAsync([FromBody] RecognizeFromUrlModel model)
        {
            return await RecognizeService.ExecuteAsync(model.Url);
        }

    }

    public class RecognizeFromUrlModel
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
