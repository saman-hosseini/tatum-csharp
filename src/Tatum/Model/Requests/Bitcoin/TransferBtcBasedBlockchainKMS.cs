using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class TransferBtcBasedBlockchainKMS : IValidatableObject
    {
        [JsonPropertyName("fromAddress")]
        public List<FromAddressKMS> FromAddresses { get; set; }

        [JsonPropertyName("fromUTXO")]
        public List<FromUtxoKMS> FromUtxos { get; set; }

        [JsonPropertyName("to")]
        public List<To> Tos { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FromAddresses != null && FromUtxos != null)
            {
                yield return new ValidationResult("Either FromAddresses, or FromUtxos must be present. Not both at the same time.");
            }
            else
            {
                if (FromAddresses != null)
                {
                    foreach (FromAddressKMS address in FromAddresses)
                    {
                        var addressContext = new ValidationContext(address);
                        Validator.ValidateObject(address, addressContext, validateAllProperties: true);
                    }
                }

                if (FromUtxos != null)
                {
                    foreach (FromUtxoKMS utxo in FromUtxos)
                    {
                        var utxoContext = new ValidationContext(utxo);
                        Validator.ValidateObject(utxo, utxoContext, validateAllProperties: true);
                    }
                }
            }

            if (Tos != null && Tos.Count > 0)
            {
                foreach (To to in Tos)
                {
                    var toContext = new ValidationContext(to);
                    Validator.ValidateObject(to, toContext, validateAllProperties: true);
                }
            }
            else
            {
                yield return new ValidationResult("Tos can not be empty.");
            }
        }
    }

    public class FromAddressKMS
    {
        [Required]
        [StringLength(60, MinimumLength = 30)]
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }
    }

    public class FromUtxoKMS
    {
        [Required]
        [StringLength(64, MinimumLength = 64)]
        [JsonPropertyName("txHash")]
        public string TxHash { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [Required]
        [StringLength(52, MinimumLength = 52)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }
    }
}
