using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using MVVM_MAUI_Simple_Todo_List.Models;
using MVVM_MAUI_Simple_Todo_List.Services;
using MVVM_MAUI_Simple_Todo_List.ViewModels;
using MVVM_MAUI_Simple_Todo_List.Services;

namespace MVVM_MAUI_Simple_Todo_List.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly LocalizationService _localizationService;
        private readonly ThemeService _themeService;
        
        public SettingsViewModel(LocalizationService localizationService, ThemeService themeService)
        {
            _localizationService = localizationService;
            _themeService = themeService;
        }
        
        [RelayCommand]
        void SetLanguageEstonian()
        {
            _localizationService.SetCulture(new CultureInfo("et-EE"));
        }

        [RelayCommand]
        void SetLanguageEnglish()
        {
            _localizationService.SetCulture(new CultureInfo("en-US"));
        }
        
        [RelayCommand]
        void SetLanguageRussian()
        {
            _localizationService.SetCulture(new CultureInfo("ru-RU"));
        }
        

        [RelayCommand]
        void SetThemeLight()
        {
            _themeService.SetTheme(AppTheme.Light);
        }

        [RelayCommand]
        void SetThemeDark()
        {
            _themeService.SetTheme(AppTheme.Dark);
        }

        [RelayCommand]
        void SetThemeSystem()
        {
            // AppTheme.Unspecified "take system settings"
            _themeService.SetTheme(AppTheme.Unspecified);
        }
    }
}