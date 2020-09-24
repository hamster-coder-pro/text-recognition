using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImageRecognizer.Web.Models
{
    public abstract class RecognitionResultItemModel
    {
        protected RecognitionResultItemModel()
        {
            BoundingBox = new List<int>();
        }

        [JsonProperty("boundingBox")]
        public ICollection<int> BoundingBox { get; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}