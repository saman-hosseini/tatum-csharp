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
        public List<object> Data { get; private set; }
    }
}
