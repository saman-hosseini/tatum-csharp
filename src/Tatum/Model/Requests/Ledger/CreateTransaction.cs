using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class CreateTransaction
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        [JsonPropertyName("senderAccountId")]
        public string SenderAccountId { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 24)]
        [JsonPropertyName("recipientAccountId")]
        public string RecipientAccountId { get; set; }

        [Required]
        [StringLength(38)]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("anonymous")]
        public bool Anonymous { get; set; }

        [JsonPropertyName("compliant")]
        public bool Compliant { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [JsonPropertyName("transactionCode")]
        public string TransactionCode { get; set; }

        [StringLength(100, MinimumLength = 1)]
        [JsonPropertyName("paymentId")]
        public string PaymentId { get; set; }

        [StringLength(500, MinimumLength = 1)]
        [JsonPropertyName("recipientNote")]
        public string RecipientNote { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("baseRate")]
        public int BaseRate { get; set; }

        [StringLength(500, MinimumLength = 1)]
        [JsonPropertyName("senderNote")]
        public string SenderNote { get; set; }
    }

}
