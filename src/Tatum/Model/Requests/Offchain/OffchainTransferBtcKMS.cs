using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Tatum.Model.Requests
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

    public class OffchainTransferDogecoinKMS : OffchainTransferBtcKMS
    {

    }
}
