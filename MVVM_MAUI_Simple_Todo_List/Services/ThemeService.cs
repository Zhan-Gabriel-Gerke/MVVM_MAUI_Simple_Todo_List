namespace MVVM_MAUI_Simple_Todo_List.Services;

public class ThemeService
{
    private readonly SettingsService _settingsService;

    // Внедряем SettingsService
    public ThemeService(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    // Метод для применения темы
    public void SetTheme(AppTheme theme)
    {
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = theme;
        }
        // Сохраняем выбор
        _settingsService.AppTheme = theme;
    }

    // Метод для загрузки темы при старте
    public void InitializeTheme()
    {
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = _settingsService.AppTheme;
        }
    }
}