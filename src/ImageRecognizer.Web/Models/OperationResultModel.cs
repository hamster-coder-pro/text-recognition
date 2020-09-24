using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImageRecognizer.Web.Models
{
    public class OperationResultModel
    {
        public OperationResultModel()
        {
            RecognitionResultCollection = new List<RecognitionResultModel>();
        }

        [JsonProperty("recognitionResults")]
        public ICollection<RecognitionResultModel> RecognitionResultCollection { get; }

        [JsonProperty("status")]
        public RecognitionResultEnum Status { get; set; }
    }
}