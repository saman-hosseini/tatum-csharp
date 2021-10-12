using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class AdaTx
    {
        [JsonPropertyName("block")]
        public AdaBlock Block { get; set; }

        [JsonPropertyName("blockIndex")]
        public long BlockIndex { get; set; }

        [JsonPropertyName("deposit")]
        public long Deposit { get; set; }

        [JsonPropertyName("fee")]
        public long Fee { get; set; }

        [JsonPropertyName("inputs_aggregate")]
        public AdaAggregate InputsAggregate { get; set; }

        [JsonPropertyName("inputs")]
        public List<AdaTxInput> Inputs { get; set; }

        [JsonPropertyName("outputs")]
        public List<AdaTxOutput> Outputs { get; set; }

        [JsonPropertyName("outputs_aggregate")]
        public AdaAggregate OutputsAggregate { get; set; }

        [JsonPropertyName("invalidBefore")]
        public object InvalidBefore { get; set; }

        [JsonPropertyName("invalidHereafter")]
        public string InvalidHereafter { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("totalOutput")]
        public string TotalOutput { get; set; }

        [JsonPropertyName("includedAt")]
        public string IncludedAt { get; set; }

        [JsonPropertyName("withdrawals")]
        public List<object> Withdrawals { get; set; }

        [JsonPropertyName("withdrawals_aggregate")]
        public object WithdrawalsAggregate { get; set; }
    }

    public class AdaAggregate
    {
        [JsonPropertyName("aggregate")]
        public Aggregate Aggregate { get; set; }
    }

    public class Aggregate
    {
        [JsonPropertyName("count")]
        public string Count { get; set; }
    }

    public class AdaBlock
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("number")]
        public long Number { get; set; }
    }

    public class AdaTxInput
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("sourceTxHash")]
        public string SourceTxHash { get; set; }

        [JsonPropertyName("sourceTxIndex")]
        public int SourceTxIndex { get; set; }
    }

    public class AdaTxOutput
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("txHash")]
        public string TxHash { get; set; }

        [JsonPropertyName("script")]
        public string Script { get; set; }


    }
}
