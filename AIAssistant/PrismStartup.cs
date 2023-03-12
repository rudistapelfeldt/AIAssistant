﻿using AIAssistant.Views;
using AIAssistant.OpenAi.Interfaces;
using AIAssistant.OpenAi.Implementation;
using AIAssistant.ViewModels;
namespace AIAssistant;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                .OnAppStart("FlyOutMainPage/NavigationPage/ChatPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<FlyOutMainPage>()
                     .RegisterForNavigation<MainPage>()
                     .RegisterForNavigation<ChatPage>()
                     .RegisterForNavigation<CorrectSpellingPage>()
                     .RegisterForNavigation<ImageGeneratorPage>()
                     .RegisterInstance(SemanticScreenReader.Default)
                     .RegisterScoped<IOpenAiClient, OpenAiClient>()
                     .RegisterScoped<IAuthenticationService, AuthenticationService>()
                     .RegisterScoped<ISecureStorageService, SecureStorageService>();
                     
    }
}
