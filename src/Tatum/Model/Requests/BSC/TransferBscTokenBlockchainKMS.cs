using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class TransferBscTokenBlockchainKMS
    {
        [Required]
        [JsonPropertyName("chain")]
        public string Chain { get; set; }

        [Range(0, long.MaxValue)]
        //[JsonPropertyName("nonce")]
        public long Nonce { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 50)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 50)]
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; }

        [Required]
        [Range(1, 30)]
        [JsonPropertyName("digits")]
        public int Digits { get; set; }

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
