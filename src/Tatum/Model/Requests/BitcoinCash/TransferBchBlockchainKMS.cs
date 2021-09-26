using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tatum.Model.Requests
{
    public class TransferBchBlockchainKMS : IValidatableObject
    {
        public List<FromUtxoBcashKMS> FromUtxos { get; set; }
        public List<To> Tos { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FromUtxos != null && FromUtxos.Count > 0)
            {
                foreach (FromUtxoBcashKMS utxo in FromUtxos)
                {
                    //if (!double.TryParse(utxo.Value, out _))
                    //{
                    //    yield return new ValidationResult("Value must be a number.");
                    //}

                    var utxoContext = new ValidationContext(utxo);
                    Validator.ValidateObject(utxo, utxoContext, validateAllProperties: true);
                }
            }
            else
            {
                yield return new ValidationResult("FromUtxos can not be empty.");
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

    public class FromUtxoBcashKMS : FromUtxoKMS
    {
        //[Required]
        //public string Value { get; set; }
    }
}
