using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tatum.Model.Responses
{
    public class BnbAccount
    {
        [JsonPropertyName("account_number")]
        public long AccountNumber { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("balances")]
        public List<BnbBalance> Balances { get; set; }

        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        [JsonPropertyName("flags")]
        public long Flags { get; set; }
    }

    public class BnbBalance
    {
        [JsonPropertyName("free")]
        public string Free { get; set; }

        [JsonPropertyName("frozen")]
        public string Frozen { get; set; }

        [JsonPropertyName("locked")]
        public string Locked { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }
}
