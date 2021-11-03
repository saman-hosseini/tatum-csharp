using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class TronTxs
    {
        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("transactions")]
        public List<TronTx> Transactions { get; set; }
    }
}
