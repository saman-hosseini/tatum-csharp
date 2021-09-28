using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class TronPrivateKey
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}
