namespace MVVM_MAUI_Simple_Todo_List.Services;

public class ThemeService
{
    private readonly SettingsService _settingsService;
    
    public ThemeService(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    // theme apply
    public void SetTheme(AppTheme theme)
    {
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = theme;
        }
        _settingsService.AppTheme = theme;
    }

    // default theme
    public void InitializeTheme()
    {
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = _settingsService.AppTheme;
        }
    }
}