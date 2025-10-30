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
        
        public ObservableCollection<TodoItem> Items { get; } = new ObservableCollection<TodoItem>();

        [ObservableProperty]
        string newItemText;
        
        public MainPageViewModel(DatabaseService databaseService, AudioService audioService)
        {
            _databaseService = databaseService;
            _audioService = audioService;
            Title = "Список Задач (SQLite)";
        }
            
        
        [RelayCommand]
        void ItemDragStarting(TodoItem item)
        {
            _draggedItem = item;
        }
        
        [RelayCommand]
        void ItemDropped(TodoItem targetItem)
        {
            if (_draggedItem == null || targetItem == null || _draggedItem.Equals(targetItem))
                return;
            
            int oldIndex = Items.IndexOf(_draggedItem);
            int newIndex = Items.IndexOf(targetItem);

            if (oldIndex == -1 || newIndex == -1)
                return;
            
            Items.RemoveAt(oldIndex);
            
            Items.Insert(newIndex, _draggedItem);
            
            _draggedItem = null;
        }
        [RelayCommand]
        async Task LoadItemsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var items = await _databaseService.GetItemsAsync();
                
                Items.Clear();
                foreach (var item in items) //select from db
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        [RelayCommand]
        async Task AddItemAsync()
        {
            if (string.IsNullOrWhiteSpace(NewItemText))
                return;

            var newItem = new TodoItem { Text = NewItemText, IsDone = false };
            
            // 1. add to db
            await _databaseService.SaveItemAsync(newItem);
            
            // 2. add to screen
            Items.Add(newItem);
            
            NewItemText = string.Empty;
            await _audioService.PlayAddSoundAsync();
        }

        [RelayCommand]
        async Task DeleteItemAsync(TodoItem item)
        {
            if (item == null) return;

            // 1. remove from db
            await _databaseService.DeleteItemAsync(item);
            
            // 2. update screen
            Items.Remove(item);
        }
        
        [RelayCommand]
        async Task ToggleDoneAsync(TodoItem item)
        {
            if (item == null) return;

            item.IsDone = !item.IsDone;
            
            // update the record in db
            await _databaseService.SaveItemAsync(item);
        }
        [RelayCommand]
        async Task GoToSettingsAsync()
        {
            await Shell.Current.GoToAsync("///SettingsPage");
        }
    }