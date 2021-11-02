using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class PolygonTx
    {
        [JsonPropertyName("blockHash")]
        public string BlockHash { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("blockNumber")]
        public long BlockNumber { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("gas")]
        public long Gas { get; set; }

        [JsonPropertyName("gasPrice")]
        public long GasPrice { get; set; }

        [JsonPropertyName("transactionHash")]
        public string TransactionHash { get; set; }

        [JsonPropertyName("input")]
        public string Input { get; set; }

        [JsonPropertyName("nonce")]
        public long Nonce { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("transactionIndex")]
        public int TransactionIndex { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("gasUsed")]
        public string GasUsed { get; set; }

        [JsonPropertyName("cumulativeGasUsed")]
        public string CumulativeGasUsed { get; set; }

        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; }

        public decimal Fee
        {
            get
            {
                var gas = int.Parse(GasUsed);
                var fee = (gas * GasPrice) / 1000000000000000000M;
                return fee;
            }
        }

        [JsonPropertyName("logs")]
        public List<Log> Logs { get; set; }
    }
}
