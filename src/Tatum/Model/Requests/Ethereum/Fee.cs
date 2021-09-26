using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Tatum.Model.Requests
{
    /// <summary>
    /// Unit is Gwei.
    /// </summary>
    public class Fee
    {
        [Required]
        [RegularExpression(@"^[+]?\d+$")]
        public string GasLimit { get; set; }

        [Required]
        [RegularExpression(@"^[+]?\d+$")]
        public string GasPrice { get; set; }
    }
}
