using Newtonsoft.Json;

namespace ImageRecognizer.Web.Models
{
    internal class CognitiveServicesErrorModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}