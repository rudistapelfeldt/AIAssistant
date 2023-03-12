using System;
using Xamarin.Essentials;
using AIAssistant.Model;
using AIAssistant.OpenAi.Interfaces;
using SecureStorage = Xamarin.Essentials.SecureStorage;

namespace AIAssistant.OpenAi.Implementation
{
    public class SecureStorageService : ISecureStorageService
    {
        public async Task SetApiKey(string key)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync(AppConstants.API_STORAGE_KEY, key);
        }

        public async Task<string> GetApiKey()
        {
            return await SecureStorage.GetAsync(AppConstants.API_STORAGE_KEY);
        }
    }
}

