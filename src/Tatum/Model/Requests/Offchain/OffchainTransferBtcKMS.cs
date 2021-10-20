using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class OffchainTransferBtcKMS : OffchainTransferKMS
    {
        [StringLength(1, MinimumLength = 10000)]
        [JsonPropertyName("address")]
        public string BlockchainAddress { get; set; }

        [JsonPropertyName("multipleAmounts")]
        public string[] MultipleAmounts { get; set; }

        [StringLength(1, MinimumLength = 256)]
        [JsonPropertyName("attr")]
        public string Attr { get; set; }

        [StringLength(1, MinimumLength = 150)]
        [JsonPropertyName("xpub")]
        public string Xpub { get; set; }
    }

    public class OffchainTransferBchKMS : OffchainTransferBtcKMS
    {

    }

    public class OffchainTransferLitecoinKMS : OffchainTransferBtcKMS
    {

    }

    public class OffchainTransferDogecoinKMS //: OffchainTransferBtcKMS
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("compliant")]
        public bool Compliant { get; set; }

        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("fee")]
        public string Fee { get; set; }

        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [StringLength(1, MinimumLength = 100)]
        [JsonPropertyName("paymentId")]
        public string PaymentId { get; set; }

        [StringLength(24, MinimumLength = 24)]
        [JsonPropertyName("senderAccountId")]
        public string SenderAccountId { get; set; }

        [StringLength(1, MinimumLength = 500)]
        [JsonPropertyName("senderNote")]
        public string SenderNote { get; set; }

        [StringLength(1, MinimumLength = 10000)]
        [JsonPropertyName("address")]
        public string BlockchainAddress { get; set; }

        [JsonPropertyName("multipleAmounts")]
        public string[] MultipleAmounts { get; set; }

        [StringLength(1, MinimumLength = 256)]
        [JsonPropertyName("attr")]
        public string Attr { get; set; }

        [StringLength(1, MinimumLength = 150)]
        [JsonPropertyName("xpub")]
        public string Xpub { get; set; }
    }
}
