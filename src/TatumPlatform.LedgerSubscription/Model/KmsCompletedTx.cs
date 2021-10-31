using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription.Model
{
    public class KmsCompletedTx : BaseEntity<long>
    {
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [JsonPropertyName("txId")]
        public string TxId { get; set; }
    }
}
