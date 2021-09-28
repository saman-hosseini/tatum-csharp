using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class OffchainTransferBnbKMS : OffchainTransferKMS
    {
        [StringLength(1, MinimumLength = 100)]
        [JsonPropertyName("address")]
        public string BlockchainAddress { get; set; }

        [StringLength(42, MinimumLength = 50)]
        [JsonPropertyName("fromAddress")]
        public string FromAddress { get; set; }

        [JsonPropertyName("attr")]
        public string Attr { get; set; }
    }
}
