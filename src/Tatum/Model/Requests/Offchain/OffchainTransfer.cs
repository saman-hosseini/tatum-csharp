using System.Text.Json.Serialization;

namespace Tatum.Model.Requests
{
    public class OffchainTransfer
    {
        [JsonPropertyName("address")]
        public string BlockchainAddress { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("compliant")]
        public bool Compliant { get; set; }

        [JsonPropertyName("fromPrivateKey")]
        public string PrivateKey { get; set; }

        [JsonPropertyName("fee")]
        public string Fee { get; set; }

        [JsonPropertyName("paymentId")]
        public string PaymentId { get; set; }

        [JsonPropertyName("senderAccountId")]
        public string SenderAccountId { get; set; }

        [JsonPropertyName("senderNote")]
        public string SenderNote { get; set; }
    }
}
