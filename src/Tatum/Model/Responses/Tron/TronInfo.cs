using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class TronInfo
    {
        [JsonPropertyName("blockNumber")]
        public long BlockNumber { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("testnet")]
        public bool Testnet { get; set; }
    }
}
