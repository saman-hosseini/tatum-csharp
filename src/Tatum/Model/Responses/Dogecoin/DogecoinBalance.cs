using System.Text.Json.Serialization;

namespace Tatum.Model.Responses
{
    public class DogecoinBalance
    {
        [JsonPropertyName("balance")]
        public string Balance { get; set; }

        [JsonPropertyName("success")]
        public int Success { get; set; }
    }
}
