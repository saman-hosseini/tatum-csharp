using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class BitcoinAccountBalance
    {
        [JsonPropertyName("incoming")]
        public string Incoming { get; set; }

        [JsonPropertyName("outgoing")]
        public string Outgoing { get; set; }
    }
}
