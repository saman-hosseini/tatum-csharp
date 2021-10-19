using Newtonsoft.Json;
using System.Collections.Generic;

namespace TatumPlatform.Model
{
    public class TatumError
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; private set; }

        [JsonProperty("data")]
        public List<ErrorDetail> Data { get; private set; }
    }

    public class ErrorDetail
    {
        [JsonProperty("target")]
        public Dictionary<string, object> Target { get; private set; }

        [JsonProperty("value")]
        public int Value { get; private set; }

        [JsonProperty("property")]
        public string Property { get; private set; }

        [JsonProperty("constraints")]
        public Dictionary<string, string> Constraints { get; private set; }
    }
}
