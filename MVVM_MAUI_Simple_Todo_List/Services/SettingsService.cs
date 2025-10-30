using System.Globalization;

namespace MVVM_MAUI_Simple_Todo_List.Services;

public class SettingsService
{
    private const string ThemeKey = "app_theme";
    private const string LanguageKey = "app_language";
    
    public AppTheme AppTheme
    {
        get => (AppTheme)Preferences.Get(ThemeKey, (int)AppTheme.Unspecified);
        set => Preferences.Set(ThemeKey, (int)value);
    }
    
    public string LanguageCode
    {
        get => Preferences.Get(LanguageKey, CultureInfo.CurrentUICulture.Name);
        set => Preferences.Set(LanguageKey, value);
    }
}