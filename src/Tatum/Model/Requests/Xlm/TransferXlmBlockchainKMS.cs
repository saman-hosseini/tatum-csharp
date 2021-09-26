using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Tatum.Model.Requests
{
    public class TransferXlmBlockchainKMS
    {
        [Required]
        [StringLength(56, MinimumLength = 56)]
        [JsonPropertyName("fromAccount")]
        public string FromAccount { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 36)]
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [Required]
        [StringLength(56, MinimumLength = 56)]
        [JsonPropertyName("to")]
        public string To { get; set; }

        [Required]
        [RegularExpression(@"^[+]?((\d+(\.\d*)?)|(\.\d+))$")]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Set to true, if the destination address is not yet initialized and should be funded for the first time.
        /// </summary>
        [DefaultValue(false)]
        [JsonPropertyName("initialize")]
        public bool Initialize { get; set; }

        [StringLength(0, MinimumLength = 64)]
        [RegularExpression(@"^[ -~]{0,64}$")]
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
