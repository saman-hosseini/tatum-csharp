using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tatum.Model.Requests
{
    public class TransferTronTrc20BlockchainKMS
    {
        [Required]
        [StringLength(34, MinimumLength = 34)]
        [JsonPropertyName("from")]
        public string From { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [Required]
        [StringLength(34, MinimumLength = 34)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [StringLength(34, MinimumLength = 34)]
        [JsonPropertyName("tokenAddress")]
        public string TokenAddress { get; set; }

        /// <summary>
        /// Fee in TRX to be paid
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        [JsonPropertyName("feeLimit")]
        public int FeeLimit { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }
    }
}
