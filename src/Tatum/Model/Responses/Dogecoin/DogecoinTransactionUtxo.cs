using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Responses
{
    public class DogecoinTransactionUtxo
    {
        [JsonPropertyName("scriptPubKey")]
        public ScriptPubKey ScriptPubKey { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("coinbase")]
        public bool Coinbase { get; set; }

        [JsonPropertyName("bestblock")]
        public string Bestblock { get; set; }
    }
}
