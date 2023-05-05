using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AIAssistant.Model;
using AIAssistant.OpenAi.Interfaces;
using OpenAI;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Completions;
using OpenAI_API.Images;
using Microsoft.Extensions.Configuration;
using CompletionRequest = OpenAI_API.Completions.CompletionRequest;
using System.Reflection;

namespace AIAssistant.OpenAi.Implementation
{
    public class OpenAiClient : IOpenAiClient
    {
        readonly IHttpClientFactory _httpClientFactory;

        readonly IConfiguration _configuration;

        OpenAIAPI api;

        string openaiApiKey;

        public OpenAiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var settings = configuration.GetRequiredSection("OpenAISettings").Get<OpenAISettings>();
            api = new OpenAIAPI(settings.ApiKey);
        }

        public OpenAIAPI GetApiClient()
        {
            return api;
        }

        public async Task<string> GetCorrectSpelling(string prompt)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var gptEditRequest = new CorrectionRequest
                    {
                        Model = "text-davinci-edit-001",
                        Input = prompt,
                        Instruction = "Fix spelling mistakes"
                    };
                    CorrectionResponse? completionResponse = null;

                    using (var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/edits"))
                    {
                        httpReq.Headers.Add("Authorization", $"Bearer {openaiApiKey}");
                        string requestString = JsonSerializer.Serialize(gptEditRequest);
                        httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");
                        using (HttpResponseMessage? httpResponse = await httpClient.SendAsync(httpReq))
                        {
                            if (httpResponse is not null)
                            {
                                string responseString = await httpResponse.Content.ReadAsStringAsync();
                                if (httpResponse.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseString))
                                {
                                    completionResponse = JsonSerializer.Deserialize<CorrectionResponse>(responseString);
                                }
                            }
                        }
                    }
                    if (completionResponse is not null)
                    {
                        string? completionText = completionResponse.Choices?[0]?.Text;
                        if (!string.IsNullOrEmpty(completionText))
                            return completionText;
                    }
                }
                return "No response returned. Try again later.";
            }
            catch(Exception)
            {
                return "No response returned. Try again later.";
            }
        }



        public async Task<CompletionResult> GetCompletionText(string request, string output)
        {
            return await api.Completions.CreateCompletionAsync(new CompletionRequest(request, model: OpenAI_API.Models.Model.CurieText, temperature: 0.1, max_tokens : 100));
        }

        public async Task StreamChatAsync(OpenAI_API.Chat.ChatRequest request, string output)
        {
            await foreach (var res in api.Chat.StreamChatEnumerableAsync(request))
            {
                output += res.Choices[0].Message;
            }
        }

        public async Task<ImageResult> GenerateImage(string prompt)
        {
            var response = await api.ImageGenerations.CreateImageAsync(new OpenAI_API.Images.ImageGenerationRequest { NumOfImages = 1, Prompt = prompt, ResponseFormat = ImageResponseFormat.Url, Size = ImageSize._1024, User = Guid.NewGuid().ToString() });
            return response;
        }

        public async Task<ChatResult> GetConversation(OpenAI_API.Chat.ChatRequest request)
        {
            var response = await api.Chat.CreateChatCompletionAsync(request);

            return response;
        }
    }   
}

