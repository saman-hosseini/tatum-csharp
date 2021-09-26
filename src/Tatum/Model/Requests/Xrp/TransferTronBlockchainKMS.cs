using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Tatum.Model.Requests
{
    public class TransferXrpBlockchainKMS
    {
        [Required]
        [StringLength(33, MinimumLength = 34)]
        [JsonPropertyName("fromAccount")]
        public string FromAccount { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [Required]
        [StringLength(33, MinimumLength = 34)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("fee")]
        public string Fee { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("sourceTag")]
        public int SourceTag { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("destinationTag")]
        public int DestinationTag { get; set; }
    }
}
