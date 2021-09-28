using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class XdcBalance
    {
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
    }
}
