using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription.Model
{
    public class AccountPendingBlockchainTransaction : BaseEntity<long>
    {
        [JsonPropertyName("date")]
        public long Date { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("accountId")]
        public string AccountId { get; set; }

        [JsonPropertyName("txId")]
        public string TxId { get; set; }

        [JsonPropertyName("blockHash")]
        public string BlockHash { get; set; }

        [JsonPropertyName("blockHeight")]
        public int BlockHeight { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }
    }
}
