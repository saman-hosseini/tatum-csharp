using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class XdcBalance
    {
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
    }
}
