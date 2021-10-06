using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Responses
{
    public class AdaUtxo
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("txHash")]
        public string TxHash { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
