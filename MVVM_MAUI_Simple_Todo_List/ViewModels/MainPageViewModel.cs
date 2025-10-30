using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM_MAUI_Simple_Todo_List.Models;
using MVVM_MAUI_Simple_Todo_List.Services;

namespace MVVM_MAUI_Simple_Todo_List.ViewModels;

public partial class MainPageViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private readonly AudioService _audioService;
        private TodoItem? _draggedItem;

        // Теперь у нас список объектов TodoItem, а не строк
        public ObservableCollection<TodoItem> Items { get; } = new ObservableCollection<TodoItem>();

        [ObservableProperty]
        string newItemText;

        // Внедряем сервис БД через конструктор
        public MainPageViewModel(DatabaseService databaseService, AudioService audioService)
        {
            _databaseService = databaseService;
            _audioService = audioService;
            Title = "Список Задач (SQLite)";
        }
            
        
        [RelayCommand]
        void ItemDragStarting(TodoItem item)
        {
            // Сохраняем, какой элемент мы тащим
            _draggedItem = item;
        }

// Команда #2: Вызывается, когда пользователь БРОСАЕТ элемент на новый
        [RelayCommand]
        void ItemDropped(TodoItem targetItem)
        {
            if (_draggedItem == null || targetItem == null || _draggedItem.Equals(targetItem))
                return;

            // --- Логика смены порядка ---
    
            // 1. Находим старую и новую позиции
            int oldIndex = Items.IndexOf(_draggedItem);
            int newIndex = Items.IndexOf(targetItem);

            if (oldIndex == -1 || newIndex == -1)
                return;

            // 2. Удаляем элемент со старой позиции...
            Items.RemoveAt(oldIndex);
    
            // 3. ...и вставляем его на новую
            // (Используем Insert, а не Add, чтобы вставить в конкретное место)
            Items.Insert(newIndex, _draggedItem);

            // Очищаем перетаскиваемый элемент
            _draggedItem = null;
    
            // (Примечание: для сохранения этого порядка в БД 
            // потребовалась бы доп. логика, например, поле 'SortOrder' в TodoItem.
            // Для простоты мы меняем порядок только в ObservableCollection на экране.)
        }
        // Новый метод для загрузки данных из БД
        // Мы будем вызывать его при запуске страницы
        [RelayCommand]
        async Task LoadItemsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var items = await _databaseService.GetItemsAsync();
                
                Items.Clear(); // Очищаем список в памяти
                foreach (var item in items) // Заполняем данными из БД
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                // (здесь можно обработать ошибку, если нужно)
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Команда добавления
        [RelayCommand]
        async Task AddItemAsync()
        {
            if (string.IsNullOrWhiteSpace(NewItemText))
                return;

            var newItem = new TodoItem { Text = NewItemText, IsDone = false };
            
            // 1. Сохраняем в БД
            await _databaseService.SaveItemAsync(newItem);
            
            // 2. Добавляем в список на экране
            Items.Add(newItem);
            
            NewItemText = string.Empty;
            await _audioService.PlayAddSoundAsync();
        }

        // Команда удаления (теперь принимает TodoItem)
        [RelayCommand]
        async Task DeleteItemAsync(TodoItem item)
        {
            if (item == null) return;

            // 1. Удаляем из БД
            await _databaseService.DeleteItemAsync(item);
            
            // 2. Удаляем из списка на экране
            Items.Remove(item);
        }

        // Команда "изменения" (выполнено/не выполнено)
        [RelayCommand]
        async Task ToggleDoneAsync(TodoItem item)
        {
            if (item == null) return;

            item.IsDone = !item.IsDone;
            
            // Обновляем запись в БД
            await _databaseService.SaveItemAsync(item);
        }
        [RelayCommand]
        async Task GoToSettingsAsync()
        {
            // Используем Shell-навигацию по имени роута, 
            // которое мы задали в AppShell.xaml
            await Shell.Current.GoToAsync("///SettingsPage");
        }
    }