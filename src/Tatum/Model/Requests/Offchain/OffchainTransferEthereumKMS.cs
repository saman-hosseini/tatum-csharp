using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    public class OffchainTransferEthereumKMS : OffchainTransferKMS
    {
        [Range(0, long.MaxValue)]
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        [StringLength(42, MinimumLength = 42)]
        [JsonPropertyName("address")]
        public string BlockchainAddress { get; set; }

        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public string Index { get; set; }

        [RegularExpression(@"^[+]?\d+$")]
        [JsonPropertyName("gasLimit")]
        public string GasLimit { get; set; }

        [RegularExpression(@"^[+]?\d+$")]
        [JsonPropertyName("gasPrice")]
        public string GasPrice { get; set; }
    }

    public class OffchainTransferMaticKMS : OffchainTransferEthereumKMS
    {

    }

    public class OffchainTransferErc20KMS : OffchainTransferEthereumKMS
    {

    }

    public class OffchainTransferBscKMS : OffchainTransferEthereumKMS
    {

    }

    public class OffchainTransferOneKMS : OffchainTransferEthereumKMS
    {

    }
}
