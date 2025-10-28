using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MVVM_MAUI_Simple_Todo_List.ViewModels;

public partial class MainPageViewModel : BaseViewModel 
{
    // Это список наших задач. 
    // ObservableCollection автоматически уведомляет UI об изменениях (добавлении/удалении)
    public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

    // [ObservableProperty] автоматически создает свойство "NewItemText"
    [ObservableProperty]
    string newItemText;

    public MainPageViewModel()
    {
        Title = "Мой Список Задач"; // Устанавливаем заголовок
            
        // Добавим пару задач для теста
        Items.Add("Выполнить Шаг 2");
        Items.Add("Выпить кофе");
    }

    // [RelayCommand] превращает этот метод в ICommand, 
    // который мы можем привязать к кнопке.
    [RelayCommand]
    void AddItem()
    {
        if (string.IsNullOrWhiteSpace(NewItemText))
            return;

        Items.Add(NewItemText);
        NewItemText = string.Empty; // Очищаем поле ввода
    }

    [RelayCommand]
    void DeleteItem(string item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
        }
    }
}