using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Tatum.Model.Requests
{
    public class TronPrivateKeyRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [StringLength(500, MinimumLength = 48)]
        [JsonPropertyName("mnemonic")]
        public string Mnemonic { get; set; }
    }
}
