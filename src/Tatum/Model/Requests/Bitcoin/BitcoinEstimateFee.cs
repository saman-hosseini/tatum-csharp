using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class BitcoinEstimateFee
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [JsonPropertyName("senderAccountId")]
        public string SenderAccountId { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 1)]
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [Required]
        [StringLength(38, MinimumLength = 38)]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("multipleAmounts")]
        public List<string> MultipleAmounts { get; set; }

        [JsonPropertyName("attr")]
        public string Attr { get; set; }

        [JsonPropertyName("xpub")]
        public string Xpub { get; set; }
    }
}
