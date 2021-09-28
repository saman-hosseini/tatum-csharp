using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class EthereumAccountBalance
    {
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
    }
}
