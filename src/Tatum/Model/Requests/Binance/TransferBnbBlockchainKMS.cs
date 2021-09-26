using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Tatum.Model.Requests.Binance
{
    public class TransferBnbBlockchainKMS
    {
        [Required]
        [StringLength(42, MinimumLength = 56)]
        [JsonPropertyName("fromAddress")]
        public string FromAddress { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 100)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [Required]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
