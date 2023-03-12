using System;
using Newtonsoft.Json;

namespace AIAssistant.Model
{
    public class ImageGenerationRequest
    {
        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("n")]
        public int N { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }
    }
}

