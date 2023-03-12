using System;
namespace AIAssistant.OpenAi.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetAccessTokenAsync(string apiKey);
    }
}

