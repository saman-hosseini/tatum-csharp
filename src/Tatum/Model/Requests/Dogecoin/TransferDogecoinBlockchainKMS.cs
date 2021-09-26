using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tatum.Model.Requests
{
    public class TransferDogecoinBlockchainKMS
    {
        [JsonPropertyName("fromUTXO")]
        public List<FromUtxoDogecoinKMS> FromUTXO { get; set; }

        [JsonPropertyName("to")]
        public List<ToDogecoin> To { get; set; }

        [JsonPropertyName("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// Address, where unspent funds will be transferred.
        /// </summary>
        [StringLength(30, MinimumLength = 60)]
        [JsonPropertyName("changeAddress")]
        public string ChangeAddress { get; set; }
    }

    public class ToDogecoin
    {
        [Required]
        [StringLength(30, MinimumLength = 60)]
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [Required]
        [JsonPropertyName("value")]
        public int Value { get; set; }
    }

    public class FromUtxoDogecoinKMS
    {
        [Required]
        [StringLength(64, MinimumLength = 64)]
        [JsonPropertyName("txHash")]
        public string TxHash { get; set; }

        [Required]
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 60)]
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }
    }
}
