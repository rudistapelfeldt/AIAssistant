using System;
using System.Threading.Tasks;
using AIAssistant.Model;
using OpenAI_API.Chat;
using OpenAI_API.Completions;

namespace AIAssistant.OpenAi.Interfaces
{
    public interface IOpenAiClient
    {
        Task<string> GetCorrectSpelling(string prompt);
        
        Task<CompletionResult> GetCompletionText(string request, string output);

        Task<ChatResult> CreateChatCompletionAsync(OpenAI_API.Chat.ChatRequest request);

        Task GenerateImage(string prompt);
    }
}

