using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class VeChainAccountBalance
    {
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
    }
}
