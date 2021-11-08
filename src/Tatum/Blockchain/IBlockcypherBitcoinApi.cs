using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Blockchain
{
    public interface IBlockcypherBitcoinApi
    {
        [Get("/v1/btc/main/addrs/{address}/balance")]
        Task<Btc> GetTxForAccount(string address);
    }

    public class Btc
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("total_received")]
        public long Total_received { get; set; }

        [JsonPropertyName("total_sent")]
        public long Total_sent { get; set; }

        [JsonPropertyName("balance")]
        public long Balance { get; set; }

        [JsonPropertyName("unconfirmed_balance")]
        public long unconfirmed_balance { get; set; }

        [JsonPropertyName("final_balance")]
        public long final_balance { get; set; }

        [JsonPropertyName("n_tx")]
        public long n_tx { get; set; }

        [JsonPropertyName("unconfirmed_n_tx")]
        public long unconfirmed_n_tx { get; set; }

        [JsonPropertyName("final_n_tx")]
        public long final_n_tx { get; set; }
    }
}