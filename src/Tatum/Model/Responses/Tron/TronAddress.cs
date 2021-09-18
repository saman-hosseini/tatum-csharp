using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class TronAddress
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
