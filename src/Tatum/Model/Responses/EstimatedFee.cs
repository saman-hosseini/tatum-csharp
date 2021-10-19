using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class EstimatedFee
    {
        [JsonPropertyName("fast")]
        public string Fast { get; set; }

        [JsonPropertyName("medium")]
        public string Medium { get; set; }

        [JsonPropertyName("slow")]
        public string Slow { get; set; }
    }
}
