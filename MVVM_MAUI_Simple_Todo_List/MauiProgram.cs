using Microsoft.Extensions.Logging;
using MVVM_MAUI_Simple_Todo_List.ViewModels;

namespace MVVM_MAUI_Simple_Todo_List;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                builder.Services.AddSingleton<MainPage>();
                builder.Services.AddSingleton<MainPageViewModel>();
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}