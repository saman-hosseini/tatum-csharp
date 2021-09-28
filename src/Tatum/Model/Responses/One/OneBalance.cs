using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class OneBalance
    {
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
    }
}
