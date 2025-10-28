using System.Globalization;

namespace MVVM_MAUI_Simple_Todo_List.Services;

public class SettingsService
{
    // Ключи для хранения в Preferences
    private const string ThemeKey = "app_theme";
    private const string LanguageKey = "app_language";

    // Свойство для Темы
    public AppTheme AppTheme
    {
        // (int)AppTheme.Unspecified - это 0, что означает "Следовать системе"
        get => (AppTheme)Preferences.Get(ThemeKey, (int)AppTheme.Unspecified);
        set => Preferences.Set(ThemeKey, (int)value);
    }

    // Свойство для Языка
    public string LanguageCode
    {
        // По умолчанию - язык системы
        get => Preferences.Get(LanguageKey, CultureInfo.CurrentUICulture.Name);
        set => Preferences.Set(LanguageKey, value);
    }
}