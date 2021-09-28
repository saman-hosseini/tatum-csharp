using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Requests.One
{
    public class TransferOneBlockchainKMS
    {
        [StringLength(50000)]
        [JsonPropertyName("data")]
        public string Data { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("nonce")]
        public int Nonce { get; set; }

        [Required]
        [StringLength(42, MinimumLength = 42)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("fee")]
        public Fee Fee { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }
    }
}
