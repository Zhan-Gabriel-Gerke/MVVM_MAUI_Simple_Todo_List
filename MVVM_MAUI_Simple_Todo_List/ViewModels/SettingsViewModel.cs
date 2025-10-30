// Файл: ViewModels/SettingsViewModel.cs
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using MVVM_MAUI_Simple_Todo_List.Models;
using MVVM_MAUI_Simple_Todo_List.Services;
using MVVM_MAUI_Simple_Todo_List.ViewModels;
using MVVM_MAUI_Simple_Todo_List.Services; // Для доступа к сервисам

namespace MVVM_MAUI_Simple_Todo_List.ViewModels
{
    // Класс должен быть 'partial' для CommunityToolkit.Mvvm
    public partial class SettingsViewModel : BaseViewModel
    {
        // --- Поля для сервисов ---
        private readonly LocalizationService _localizationService;
        private readonly ThemeService _themeService;

        // --- Конструктор (Внедрение зависимостей) ---
        // Теперь мы просим MAUI предоставить нам ОБА сервиса
        public SettingsViewModel(LocalizationService localizationService, ThemeService themeService)
        {
            _localizationService = localizationService;
            _themeService = themeService;

            // Нам больше не нужно устанавливать 'Title' здесь,
            // потому что 'SettingsPage.xaml' делает это 
            // динамически через {local:Translate SettingsPageTitle}
        }

        // --- Команды Языка (Шаг 4) ---
        
        
        
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

        // (Бонус: команда для русского, т.к. он был у вас в .resx)
        [RelayCommand]
        void SetLanguageRussian()
        {
            _localizationService.SetCulture(new CultureInfo("ru-RU"));
        }

        // --- Команды Темы (Шаг 5) ---

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
            // AppTheme.Unspecified означает "Следовать настройкам системы"
            _themeService.SetTheme(AppTheme.Unspecified);
        }
    }
}