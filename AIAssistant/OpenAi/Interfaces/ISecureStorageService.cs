using System;
namespace AIAssistant.OpenAi.Interfaces
{
    public interface ISecureStorageService
    {
        Task SetApiKey(string key);

        Task<string> GetApiKey();
    }
}

