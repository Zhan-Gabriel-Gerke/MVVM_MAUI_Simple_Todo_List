// Файл: MauiProgram.cs
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
                // <-- ИСПРАВЛЕНИЕ: Внутри ConfigureFonts - ТОЛЬКО шрифты
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // --- ИСПРАВЛЕНИЕ: Регистрация сервисов должна быть здесь ---
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

        // --- ИСПРАВЛЕНИЕ: Инициализация настроек (Шаг 5) ---
        
        // 1. Сначала собираем приложение
        var app = builder.Build();

        // 2. Получаем сервисы из контейнера
        var themeService = app.Services.GetRequiredService<ThemeService>();
        var localizationService = app.Services.GetRequiredService<LocalizationService>();

        // 3. Применяем сохраненные настройки
        themeService.InitializeTheme();
        localizationService.InitializeLanguage();

        // 4. Возвращаем готовое приложение
        return app;
    }
}