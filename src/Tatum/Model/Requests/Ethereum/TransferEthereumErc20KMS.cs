using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class TransferEthereumErc20KMS : IValidatableObject
    {
        [Required]
        [StringLength(42, MinimumLength = 42)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [StringLength(50000)]
        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (Fee != null)
            {
                var feeContext = new ValidationContext(Fee);
                Validator.ValidateObject(Fee, feeContext, validateAllProperties: true);
            }

            if (results.Count == 0)
            {
                results.Add(ValidationResult.Success);
            }

            return results;
        }
    }
}
