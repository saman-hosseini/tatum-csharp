using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tatum.Model.Requests
{
    public class TransferEgldBlockchainKMS
    {
        [StringLength(62, MinimumLength = 62)]
        [JsonPropertyName("from")]
        public string From { get; set; }

        [StringLength(62, MinimumLength = 62)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("fee")]
        public Fee Fee { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }
    }
}
