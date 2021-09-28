using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Responses
{
    public class FlowAccount
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("balance")]
        public long Balance { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("contracts")]
        public object Contracts { get; set; }

        [JsonPropertyName("keys")]
        public List<FlowKey> FlowKeys { get; set; }
    }

    public class FlowKey
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }

        [JsonPropertyName("signAlgo")]
        public int SignAlgo { get; set; }

        [JsonPropertyName("hashAlgo")]
        public int HashAlgo { get; set; }

        [JsonPropertyName("sequenceNumber")]
        public int SequenceNumber { get; set; }

        [JsonPropertyName("revoked")]
        public bool Revoked { get; set; }

        [JsonPropertyName("weight")]
        public long Weight { get; set; }
    }
}
