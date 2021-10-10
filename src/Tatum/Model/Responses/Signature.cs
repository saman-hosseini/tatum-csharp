using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.Model.Responses
{
    public class Signature
    {
        [JsonPropertyName("SignatureId")]
        public string SignatureId { get; set; }
    }
}
