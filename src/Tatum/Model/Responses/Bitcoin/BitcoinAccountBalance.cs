using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class BitcoinAccountBalance
    {
        [JsonPropertyName("incoming")]
        public string Incoming { get; set; }

        [JsonPropertyName("outgoing")]
        public string Outgoing { get; set; }

        [JsonIgnore]
        public decimal CurrentBalance
        {
            get
            {
                decimal i, o;
                decimal.TryParse(Incoming, out i);
                decimal.TryParse(Outgoing, out o);
                return (i - o);
            }
        }
    }
}
