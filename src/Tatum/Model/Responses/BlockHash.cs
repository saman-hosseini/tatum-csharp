using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class BlockHash
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
    }
}
