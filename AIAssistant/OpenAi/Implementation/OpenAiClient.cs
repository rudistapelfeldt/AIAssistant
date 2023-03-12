﻿using System;
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
using OpenAI_API.Completions;
using CompletionRequest = OpenAI_API.Completions.CompletionRequest;

namespace AIAssistant.OpenAi.Implementation
{
    public class OpenAiClient : IOpenAiClient
    {
       OpenAIAPI api = new OpenAIAPI(AppConstants.OPENAI_API_KEY);

        readonly ISecureStorageService _secureStorageService;

        public OpenAiClient(ISecureStorageService secureStorageService)
        {
            _secureStorageService = secureStorageService;
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
                        httpReq.Headers.Add("Authorization", $"Bearer {AppConstants.OPENAI_API_KEY}");
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
            

            //var api = new OpenAI_API.OpenAIAPI(AppConstants.OPENAI_API_KEY);
            //await foreach (var token in api.Completions.StreamCompletionEnumerableAsync(new CompletionRequest
            //{
            //    Model = "text-davinci-003",
            //    Temperature = 0.5f,
            //    MaxTokens = 120,
            //    TopP = 0.3f,
            //    FrequencyPenalty = 0.5f,
            //    PresencePenalty = 0,
            //    Prompt = request
            //}))
            //{
            //    Console.WriteLine(token);
            //}
        }
    }   
}
