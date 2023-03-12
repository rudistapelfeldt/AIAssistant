using System;
using System.Text.Json.Serialization;

namespace AIAssistant.Model
{
    public class Choice
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("Index")]
        public int index { get; set; }
    }
}

