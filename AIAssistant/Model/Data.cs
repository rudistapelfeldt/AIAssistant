using System;
namespace AIAssistant.Model
{
    public class Data
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public List<Choice> choices { get; set; }
        public string model { get; set; }
    }
}

