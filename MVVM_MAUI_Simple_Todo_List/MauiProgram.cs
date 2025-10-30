using Microsoft.Extensions.Logging;
using MVVM_MAUI_Simple_Todo_List.Services;
using MVVM_MAUI_Simple_Todo_List.ViewModels;
using MVVM_MAUI_Simple_Todo_List.Views;
using Plugin.Maui.Audio;

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
            });

        // --- Регистрация сервисов ---
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<SettingsService>();
        builder.Services.AddSingleton<ThemeService>();
        builder.Services.AddSingleton<LocalizationService>();
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddSingleton<AudioService>();

        // --- Регистрация Страниц и ViewModel ---
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        var app = builder.Build();

        // --- Инициализация Настроек ---
        var themeService = app.Services.GetRequiredService<ThemeService>();
        var localizationService = app.Services.GetRequiredService<LocalizationService>();
        
        themeService.InitializeTheme();
        localizationService.InitializeLanguage();

        return app;
    }
}