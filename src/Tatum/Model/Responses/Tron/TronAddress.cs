﻿using System.Text.Json.Serialization;

namespace TatumPlatform.Model.Responses
{
    public class TronAddress
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}