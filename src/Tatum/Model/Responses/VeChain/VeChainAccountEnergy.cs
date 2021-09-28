using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class VeChainAccountEnergy
    {
        [JsonPropertyName("energy")]
        public string Energy { get; set; }
    }
}
