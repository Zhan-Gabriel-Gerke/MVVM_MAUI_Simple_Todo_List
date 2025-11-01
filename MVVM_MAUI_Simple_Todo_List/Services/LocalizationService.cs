using System.ComponentModel;
using System.Globalization;
using CommunityToolkit.Mvvm.Input;
using MVVM_MAUI_Simple_Todo_List.Resources.Localization;
namespace MVVM_MAUI_Simple_Todo_List.Services;

public class LocalizationService : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private readonly SettingsService _settingsService;
    public static LocalizationService Current { get; private set; } = null!;
    
    public string this[string key] => AppStrings.ResourceManager.GetString(key, AppStrings.Culture) ?? string.Empty;
    
    public LocalizationService(SettingsService settingsService)
    {
        _settingsService = settingsService;
        Current = this;
    }

    public void SetCulture(CultureInfo culture)
    {
        AppStrings.Culture = culture;
        _settingsService.LanguageCode = culture.Name;
            
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }
    
    public void InitializeLanguage()
    {
        try
        {
            // code of a language
            var cultureInfo = new CultureInfo(_settingsService.LanguageCode);
            SetCulture(cultureInfo);
        }
        catch (Exception)
        {
            SetCulture(CultureInfo.CurrentUICulture);
        }
    }
}