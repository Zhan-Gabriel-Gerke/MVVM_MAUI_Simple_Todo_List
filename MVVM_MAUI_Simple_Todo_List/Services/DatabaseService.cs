using MVVM_MAUI_Simple_Todo_List.Models;
using SQLite;

namespace MVVM_MAUI_Simple_Todo_List.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;

    // Конструктор
    public DatabaseService()
    {
        // Определяем путь к файлу БД. 
        // Он будет создан в локальном хранилище приложения
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "todo.db3");
        _database = new SQLiteAsyncConnection(dbPath);
    }

    // Метод для инициализации (создаст таблицу, если ее нет)
    public async Task InitAsync()
    {
        await _database.CreateTableAsync<TodoItem>();
    }

    // Получить все задачи
    public async Task<List<TodoItem>> GetItemsAsync()
    {
        await InitAsync(); // Убедимся, что таблица существует
        return await _database.Table<TodoItem>().ToListAsync();
    }

    // Сохранить или обновить задачу (требование "изменить")
    public async Task<int> SaveItemAsync(TodoItem item)
    {
        await InitAsync();
        if (item.Id != 0)
        {
            // Обновить существующую
            return await _database.UpdateAsync(item);
        }
        else
        {
            // Добавить новую
            return await _database.InsertAsync(item);
        }
    }

    // Удалить задачу (требование "удалить")
    public async Task<int> DeleteItemAsync(TodoItem item)
    {
        await InitAsync();
        return await _database.DeleteAsync(item);
    }
}