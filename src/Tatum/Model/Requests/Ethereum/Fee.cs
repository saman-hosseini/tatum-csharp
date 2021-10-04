using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Requests
{
    /// <summary>
    /// Unit is Gwei.
    /// </summary>
    public class Fee
    {
        [Required]
        [RegularExpression(@"^[+]?\d+$")]
        [JsonPropertyName("gasLimit")]
        public string GasLimit { get; set; }

        [Required]
        [RegularExpression(@"^[+]?\d+$")]
        [JsonPropertyName("gasPrice")]
        public string GasPrice { get; set; }
    }
}
