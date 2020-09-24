using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImageRecognizer.Web.Models
{
    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public sealed class RecognitionResultLineModel : RecognitionResultItemModel
    {
        public RecognitionResultLineModel()
        {
            WordCollection = new List<RecognitionResultWordModel>();
        }

        [JsonProperty("words")]
        public ICollection<RecognitionResultWordModel> WordCollection { get; }
    }
}