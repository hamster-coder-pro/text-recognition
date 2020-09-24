using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImageRecognizer.Web.Models
{
    public sealed class RecognitionResultModel
    {
        public RecognitionResultModel()
        {
            LineCollection = new List<RecognitionResultLineModel>();
        }

        // other properties skipped atm

        [JsonProperty("lines")]
        public ICollection<RecognitionResultLineModel> LineCollection { get; }
    }
}