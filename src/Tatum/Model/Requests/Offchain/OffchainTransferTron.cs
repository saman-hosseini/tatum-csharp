using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tatum.Model.Requests
{
    public class OffchainTransferTron
    {
        [StringLength(34, MinimumLength = 34)]
        [JsonPropertyName("address")]
        public string BlockchainAddress { get; set; }

        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("compliant")]
        public bool Compliant { get; set; }

        [StringLength(64, MinimumLength = 64)]
        [JsonPropertyName("fromPrivateKey")]
        public string PrivateKey { get; set; }

        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("fee")]
        public string Fee { get; set; }

        [StringLength(1, MinimumLength = 100)]
        [JsonPropertyName("paymentId")]
        public string PaymentId { get; set; }

        [StringLength(24, MinimumLength = 24)]
        [JsonPropertyName("senderAccountId")]
        public string SenderAccountId { get; set; }

        [StringLength(1, MinimumLength = 500)]
        [JsonPropertyName("senderNote")]
        public string SenderNote { get; set; }
    }
}
