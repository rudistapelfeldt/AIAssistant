using System.Reflection;
using Microsoft.Extensions.Configuration;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace AIAssistant;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UsePrismApp<App>(PrismStartup.Configure)
            .UseSkiaSharp()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("AIAssistant.appsettings.json");

        var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();


        builder.Configuration.AddConfiguration(config);
        return builder.Build();
    }
}
