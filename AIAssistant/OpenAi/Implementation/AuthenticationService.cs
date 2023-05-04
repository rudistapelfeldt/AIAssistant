using System;
using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AIAssistant.OpenAi.Interfaces;
using AIAssistant.Model;

namespace AIAssistant.OpenAi.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService() { }

        public async Task<string> GetAccessTokenAsync(string apiKey)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/oauth/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:")));

            request.Content = new StringContent("grant_type=client_credentials", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(responseContent);

                if (json.RootElement.TryGetProperty("access_token", out var accessToken))
                {
                    return accessToken.GetString();
                }
            }

            throw new Exception("Failed to obtain access token.");
        }
    }
}

