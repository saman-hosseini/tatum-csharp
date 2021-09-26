using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tatum.Model.Responses
{
    public class QtumBalance
    {
        [JsonPropertyName("addrStr")]
        public string AddrStr { get; set; }

        [JsonPropertyName("balance")]
        public long Balance { get; set; }

        [JsonPropertyName("balanceSat")]
        public long BalanceSat { get; set; }

        [JsonPropertyName("totalReceived")]
        public long TotalReceived { get; set; }

        [JsonPropertyName("totalReceivedSat")]
        public long TotalReceivedSat { get; set; }

        [JsonPropertyName("totalSet")]
        public long TotalSet { get; set; }

        [JsonPropertyName("totalSentSat")]
        public long TotalSentSat { get; set; }

        [JsonPropertyName("unconfirmedBalance")]
        public long UnconfirmedBalance { get; set; }

        [JsonPropertyName("unconfirmedBalanceSat")]
        public long UnconfirmedBalanceSat { get; set; }

        [JsonPropertyName("unconfirmedTxApperances")]
        public long UnconfirmedTxApperances { get; set; }

        [JsonPropertyName("txApperances")]
        public long TxApperances { get; set; }

        [JsonPropertyName("transactions")]
        public List<string> Transactions { get; set; }
    }
}
