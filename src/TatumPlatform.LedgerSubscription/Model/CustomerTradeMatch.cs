using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription.Model
{
    public class CustomerTradeMatch : BaseEntity<long>
    {
        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("pair")]
        public string Pair { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("currency1AccountId")]
        public string Currency1AccountId { get; set; }

        [JsonPropertyName("currency2AccountId")]
        public string Currency2AccountId { get; set; }

        [JsonPropertyName("fee")]
        public string Fee { get; set; }

        [JsonPropertyName("feeAccountId")]
        public string FrefeeAccountIdated { get; set; }

        [JsonPropertyName("isMaker")]
        public string IsMaker { get; set; }

        [JsonPropertyName("expiredWithoutMatch")]
        public string ExpiredWithoutMatch { get; set; }
    }
}
