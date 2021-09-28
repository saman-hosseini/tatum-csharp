using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class TronBlock
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("blockNumber")]
        public long BlockNumber { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("parentHash")]
        public string ParentHash { get; set; }

        [JsonPropertyName("witnessAddress")]
        public string WitnessAddress { get; set; }

        [JsonPropertyName("witnessSignature")]
        public string WitnessSignature { get; set; }

        [JsonPropertyName("transactions")]
        public List<TronTx> Transactions { get; set; }
    }
}
