using Newtonsoft.Json;

namespace ImageRecognizer.Web.Models
{
    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public sealed class RecognitionResultWordModel : RecognitionResultItemModel
    {
    }
}