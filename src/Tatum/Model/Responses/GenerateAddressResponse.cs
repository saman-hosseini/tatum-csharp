using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Responses
{
    public class GenerateAddressResponse
    {
        public string Address { get; set; }
        public int? TagId { get; set; }
        public BlockchainAddressType BlockchainAddressType { get; set; }
    }

    public enum BlockchainAddressType
    {
        ReceiveAddress,
        TagId
    }
}
