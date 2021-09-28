using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Requests
{
    public class TransferFlowBlockchainKMS
    {
        [Required]
        [StringLength(18, MinimumLength = 18)]
        [JsonPropertyName("account")]
        public string Account { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [StringLength(18, MinimumLength = 18)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [StringLength(38, MinimumLength = 1)]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        /// <summary>
        /// If signatureId is mnemonic-based, this is the index to the specific address from that mnemonic.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}
