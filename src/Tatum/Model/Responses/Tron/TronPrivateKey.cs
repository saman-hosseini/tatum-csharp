using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class TronPrivateKey
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}
