using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class TronTx
    {
        [JsonPropertyName("blockNumber")]
        public long BlockNumber { get; set; }

        [JsonPropertyName("ret")]
        public object Ret { get; set; }

        [JsonPropertyName("signature")]
        public List<string> Signature { get; set; }

        [JsonPropertyName("txID")]
        public string TxID { get; set; }

        [JsonPropertyName("netFee")]
        public long NetFee { get; set; }

        [JsonPropertyName("netUsage")]
        public long NetUsage { get; set; }

        [JsonPropertyName("energyFee")]
        public long EnergyFee { get; set; }

        [JsonPropertyName("energyUsage")]
        public long EnergyUsage { get; set; }

        [JsonPropertyName("energyUsageTotal")]
        public long EnergyUsageTotal { get; set; }

        [JsonPropertyName("internalTransactions")]
        public List<TronInternalTransaction> InternalTransactions { get; set; }

        [JsonPropertyName("rawData")]
        public object RawData { get; set; }

    }
}
