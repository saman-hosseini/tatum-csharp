using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class EthereumEstimateFee
    {
        [Required]
        [StringLength(42, MinimumLength = 42)]
        [JsonPropertyName("from")]
        public string From { get; set; }

        [Required]
        [StringLength(42, MinimumLength = 42)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [StringLength(50000)]
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
