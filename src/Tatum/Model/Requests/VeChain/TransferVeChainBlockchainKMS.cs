using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Requests
{
    public class TransferVeChainBlockchainKMS
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string SignatureId { get; set; }

        [StringLength(10000)]
        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("fee")]
        public VeChainFee Fee { get; set; }
    }

    public class VeChainFee
    {
        [Required]
        [RegularExpression(@"^[+]?\d+$")]
        [JsonPropertyName("gasLimit")]
        public string GasLimit { get; set; }
    }
}
