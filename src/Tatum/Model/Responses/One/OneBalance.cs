using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class OneBalance
    {
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
    }
}
