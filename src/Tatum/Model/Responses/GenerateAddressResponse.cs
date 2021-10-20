using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TatumPlatform.Model.Responses
{
    public class GenerateAddressResponse : IValidatableObject
    {
        public string Address { get; set; }
        public int? Index { get; set; }
        public int? TagId { get; set; }
        public int IndexOrTagId { get { return Index ?? TagId.Value; } }
        public BlockchainAddressType BlockchainAddressType { get; set; }
        public bool IsTagBased()
        {
            return TagId.HasValue;
        }

        public bool Validate()
        {
            if (Index.HasValue && TagId.HasValue)
                return false;
            if (!Index.HasValue && !TagId.HasValue)
                return false;
            if (BlockchainAddressType == BlockchainAddressType.ReceiveAddress && TagId.HasValue)
                return false;
            if (BlockchainAddressType == BlockchainAddressType.TagId && Index.HasValue)
                return false;
            return true;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Index != null && TagId != null)
            {
                yield return new ValidationResult("Either Index or TagId must be present. Not both at the same time.");
            }
            else if (Index == null && TagId == null)
            {
                yield return new ValidationResult("One of Index or TagId Should be present.");
            }
            else if (BlockchainAddressType == BlockchainAddressType.ReceiveAddress && TagId.HasValue)
            {
                yield return new ValidationResult("Wrong BlockchainAddressType has been set");
            }
            else if (BlockchainAddressType == BlockchainAddressType.TagId && Index.HasValue)
            {
                yield return new ValidationResult("Wrong BlockchainAddressType has been set");
            }
        }
    }

    public enum BlockchainAddressType
    {
        ReceiveAddress = 1,
        TagId = 2
    }
}
