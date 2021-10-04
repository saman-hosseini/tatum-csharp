using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class TransferBscBlockchainKMS
    {
        [StringLength(0, MinimumLength = 50000)]
        [JsonPropertyName("data")]
        public string Data { get; set; }

        [Range(0, long.MaxValue)]
        //[JsonPropertyName("nonce")]
        public long Nonce { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [Required]
        [StringLength(42, MinimumLength = 42)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 10)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("fee")]
        public Fee Fee { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// If signatureId is mnemonic-based, this is the index to the specific address from that mnemonic.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}
