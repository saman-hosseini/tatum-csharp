using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tatum.Model.Requests.Xdc
{
    public class XDCTransaction
    {
        [Required]
        [StringLength(66, MinimumLength = 66)]
        [JsonPropertyName("from")]
        public string From { get; set; }

        [Required]
        [StringLength(42, MinimumLength = 43)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [StringLength(50000)]
        [RegularExpression(@"^(0x|0h)?[0-9A-F]+$")]
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
