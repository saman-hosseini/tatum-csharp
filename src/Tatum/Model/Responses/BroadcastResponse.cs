using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class BroadcastResponse
    {
        [JsonPropertyName("completed")]
        public bool Completed { get; set; }

        [JsonPropertyName("txId")]
        public string TxId { get; set; }
    }
}
