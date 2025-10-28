using System.ComponentModel;
using System.Globalization;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using MVVM_MAUI_Simple_Todo_List.Resources.Localization;
namespace MVVM_MAUI_Simple_Todo_List.Services;

public class LocalizationService : INotifyPropertyChanged
{
    // Событие, которое оповещает UI об изменениях
    public event PropertyChangedEventHandler PropertyChanged;
        
    // Cтатическое свойство для доступа к единственному экземпляру сервиса
    private readonly SettingsService _settingsService; // <-- ИЗМЕНЕНИЕ 1: Добавить
    public static LocalizationService Current { get; private set; }

    // Свойство-индексатор. XAML будет обращаться к нему так: [ИмяСтроки]
    public string this[string key] => AppStrings.ResourceManager.GetString(key, AppStrings.Culture);

    // Метод смены культуры
    public LocalizationService(SettingsService settingsService)
    {
        _settingsService = settingsService;
        Current = this; // Устанавливаем статический экземпляр для TranslateExtension
    }

    public void SetCulture(CultureInfo culture)
    {
        AppStrings.Culture = culture;
        _settingsService.LanguageCode = culture.Name; // <-- СОХРАНЯЕМ ВЫБОР
            
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }

    // Новый метод для загрузки языка при старте
    public void InitializeLanguage()
    {
        try
        {
            // Загружаем сохраненный код языка
            var cultureInfo = new CultureInfo(_settingsService.LanguageCode);
            SetCulture(cultureInfo);
        }
        catch (Exception)
        {
            // Если сохранен "плохой" код, используем язык системы
            SetCulture(CultureInfo.CurrentUICulture);
        }
    }
}