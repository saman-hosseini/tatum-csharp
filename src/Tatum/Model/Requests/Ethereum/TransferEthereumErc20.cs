using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class TransferEthereumErc20 : IValidatableObject
    {
        [Required]
        [StringLength(66, MinimumLength = 66)]
        [JsonPropertyName("fromPrivateKey")]
        public string FromPrivateKey { get; set; }

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
        [JsonPropertyName("nonce")]
        public uint Nonce { get; set; }

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
