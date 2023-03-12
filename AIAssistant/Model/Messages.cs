using System;
using Newtonsoft.Json;

namespace AIAssistant.Model
{
    public class Messages
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}

