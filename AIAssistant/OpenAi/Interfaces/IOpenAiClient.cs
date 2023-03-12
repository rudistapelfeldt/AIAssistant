using System;
using System.Threading.Tasks;
using OpenAI_API.Completions;

namespace AIAssistant.OpenAi.Interfaces
{
    public interface IOpenAiClient
    {
        Task<string> GetCorrectSpelling(string prompt);
        
        Task<CompletionResult> GetCompletionText(string request, string output);
    }
}

