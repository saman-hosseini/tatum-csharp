using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Responses
{
    public class AdaAccount
    {
        [JsonPropertyName("summary")]
        public AdaSummary Summary { get; set; }
    }

    public class AdaSummary
    {
        [JsonPropertyName("utxosCount")]
        public long UtxosCount { get; set; }

        [JsonPropertyName("assetBalances")]
        public List<AdaAssetBalance> AssetBalances { get; set; }
    }

    public class AdaAssetBalance
    {
        [JsonPropertyName("asset")]
        public AdaAsset Asset { get; set; }

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; }
    }

    public class AdaAsset
    {
        [JsonPropertyName("assetId")]
        public string AssetId { get; set; }

        [JsonPropertyName("assetName")]
        public string AssetName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("metadataHash")]
        public string MetadataHash { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
