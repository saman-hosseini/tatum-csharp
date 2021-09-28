using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class EgldBalance
    {
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
    }
}
