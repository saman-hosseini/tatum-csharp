using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class OffchainTransactionResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("txId")]
        public string TxId { get; set; }

        [JsonPropertyName("completed")]
        public bool Completed { get; set; }
    }
}
