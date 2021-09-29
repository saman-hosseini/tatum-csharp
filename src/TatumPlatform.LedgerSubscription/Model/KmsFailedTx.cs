using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription.Model
{
    public class KmsFailedTx : BaseEntity<long>
    {
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
