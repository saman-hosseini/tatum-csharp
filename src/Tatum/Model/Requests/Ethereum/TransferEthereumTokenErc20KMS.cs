using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class TransferEthereumTokenErc20KMS
    {
        [Required]
        [StringLength(3, MinimumLength = 5)]
        [JsonPropertyName("chain")]
        public string Chain { get; set; }

        [StringLength(2, MinimumLength = 7)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 50)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [StringLength(43, MinimumLength = 42)]
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; }

        [Range(0, 30)]
        [JsonPropertyName("digits")]
        public int Digits { get; set; }

        [JsonPropertyName("fee")]
        public Fee Fee { get; set; }

        [Range(0, uint.MaxValue)]
        //[JsonPropertyName("nonce")]
        public uint Nonce { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }
    }
}
