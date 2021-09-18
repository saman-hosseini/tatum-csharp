using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class TronAccount
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("freeNetUsage")]
        public long FreeNetUsage { get; set; }

        [JsonPropertyName("balance")]
        public long Balance { get; set; }

        [JsonPropertyName("createTime")]
        public long CreateTime { get; set; }

        [JsonPropertyName("trc10")]
        public List<object> Trc10 { get; set; }

        [JsonPropertyName("trc20")]
        public List<object> Trc20 { get; set; }

        [JsonPropertyName("assetIssuedId")]
        public string AssetIssuedId { get; set; }

        [JsonPropertyName("assetIssuedName")]
        public long AssetIssuedName { get; set; }
    }
}
