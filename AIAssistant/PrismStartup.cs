using AIAssistant.Views;
using AIAssistant.OpenAi.Interfaces;
using AIAssistant.OpenAi.Implementation;
using AIAssistant.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AIAssistant;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                .OnAppStart("TabbedMainPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<TabbedMainPage>()
                     .RegisterForNavigation<MainPage>()
                     .RegisterForNavigation<ChatPage>()
                     .RegisterForNavigation<CorrectSpellingPage>()
                     .RegisterForNavigation<ImageGeneratorPage>()
                     .RegisterInstance(SemanticScreenReader.Default)
                     .RegisterScoped<IOpenAiClient, OpenAiClient>()
                     .RegisterScoped<IAuthenticationService, AuthenticationService>();
    }
}
