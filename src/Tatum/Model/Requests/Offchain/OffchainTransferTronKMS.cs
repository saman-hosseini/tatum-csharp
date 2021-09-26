using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Tatum.Model.Requests
{
    public class OffchainTransferTronKMS : OffchainTransferKMS
    {
        [StringLength(34, MinimumLength = 34)]
        [JsonPropertyName("address")]
        public string BlockchainAddress { get; set; }

        [StringLength(34, MinimumLength = 34)]
        [JsonPropertyName("from")]
        public string From { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public string Index { get; set; }
    }
}
