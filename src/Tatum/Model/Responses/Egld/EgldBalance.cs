using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class EgldBalance
    {
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
    }
}
