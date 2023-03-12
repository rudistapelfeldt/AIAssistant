using System;
using System.Text.Json.Serialization;

namespace AIAssistant.Model
{
    public class CorrectionResponse
    {
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("created")]
        public int Created { get; set; }
        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; }
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }
    }
}

