using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Tatum.Model.Requests
{
    public class OffchainTransferXrpKMS : OffchainTransferKMS
    {
        [StringLength(1, MinimumLength = 100)]
        [JsonPropertyName("account")]
        public string Account { get; set; }

        [StringLength(1, MinimumLength = 100)]
        [JsonPropertyName("address")]
        public string BlockchainAddress { get; set; }

        [Range(1, int.MaxValue)]
        [JsonPropertyName("sourceTag")]
        public int? SourceTag { get; set; }
    }
}
