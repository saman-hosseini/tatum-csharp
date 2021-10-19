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

    public class Fee<T>
    {
        [Required]
        [RegularExpression(@"^[+]?\d+$")]
        [JsonPropertyName("gasLimit")]
        public T GasLimit { get; set; }

        [Required]
        [RegularExpression(@"^[+]?\d+$")]
        [JsonPropertyName("gasPrice")]
        public string GasPrice { get; set; }

        public static implicit operator Fee(Fee<T> d)
        {
            return new Fee() 
            {
                GasLimit = d.GasLimit.ToString(),
                GasPrice = d.GasPrice
            };
        }
    }
}
