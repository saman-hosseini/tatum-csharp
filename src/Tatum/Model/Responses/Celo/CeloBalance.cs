using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tatum.Model.Responses
{
    public class CeloBalance
    {
        [JsonPropertyName("celo")]
        public string Celo { get; set; }

        [JsonPropertyName("cUsd")]
        public string CUsd { get; set; }
    }
}
