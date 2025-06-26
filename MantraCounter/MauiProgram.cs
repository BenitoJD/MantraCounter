using CommunityToolkit.Maui;
using MantraCounter;
using MantraCounter.Services;
using MantraCounter.ViewModels;
using MantraCounter.Views; 

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
              .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        builder.Services.AddSingleton<IMantraService, MantraService>();

        
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();


        return builder.Build();
    }
}