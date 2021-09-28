using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class TronInternalTransaction
    {
        [JsonPropertyName("internal_tx_id")]
        public string InternalTxID { get; set; }

        [JsonPropertyName("to_address")]
        public string ToAddress { get; set; }

        [JsonPropertyName("from_address")]
        public string FromAddress { get; set; }
    }
}
