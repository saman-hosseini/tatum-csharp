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
        public List<Trc10Info> Trc10 { get; set; }

        [JsonPropertyName("trc20")]
        public List<Dictionary<string, string>> Trc20 { get; set; }

        [JsonPropertyName("assetIssuedId")]
        public string AssetIssuedId { get; set; }

        [JsonPropertyName("assetIssuedName")]
        public long AssetIssuedName { get; set; }
    }

    public class Trc10Info
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public long Value { get; set; }
    }
}
