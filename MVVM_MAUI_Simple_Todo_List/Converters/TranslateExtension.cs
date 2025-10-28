using MVVM_MAUI_Simple_Todo_List.Services;

namespace MVVM_MAUI_Simple_Todo_List.Converters;

[ContentProperty(nameof(Key))]
public class TranslateExtension : IMarkupExtension<BindingBase>
{
    public string Key { get; set; } // Имя строки (ключ) из .resx

    public BindingBase ProvideValue(IServiceProvider serviceProvider)
    {
        // Мы создаем хитрую привязку:
        // 1. Источник: Наш статический сервис LocalizationService.Current
        // 2. Путь: Индексатор [Key], который мы передали
        return new Binding
        {
            Mode = BindingMode.OneWay,
            Path = $"[{Key}]",
            Source = LocalizationService.Current
        };
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
    }
}