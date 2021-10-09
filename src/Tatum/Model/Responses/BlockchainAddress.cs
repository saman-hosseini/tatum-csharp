using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class BlockchainAddress
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
