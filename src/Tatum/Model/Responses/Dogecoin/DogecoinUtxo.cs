using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Responses
{
    public class DogecoinOutputs
    {
        [JsonPropertyName("unspent_outputs")]
        public List<DogecoinUtxo> UnspentOutputs { get; set; }

        [JsonPropertyName("success")]
        public int Success { get; set; }
    }
    public class DogecoinUtxo
    {
        [JsonPropertyName("tx_hash")]
        public string TxHash { get; set; }

        [JsonPropertyName("tx_output_n")]
        public int TxOutputIndex { get; set; }

        [JsonPropertyName("script")]
        public string Script { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("confirmations")]
        public int Confirmations { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
