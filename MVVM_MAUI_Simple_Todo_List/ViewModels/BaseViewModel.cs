using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM_MAUI_Simple_Todo_List.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    // Мы используем [ObservableProperty], чтобы 
    // автоматически создать свойство IsBusy
    // и связанный с ним метод OnIsBusyChanged().
    [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        [ObservableProperty]
        string title = string.Empty; // Инициализировано
        public bool IsNotBusy => !IsBusy;
}