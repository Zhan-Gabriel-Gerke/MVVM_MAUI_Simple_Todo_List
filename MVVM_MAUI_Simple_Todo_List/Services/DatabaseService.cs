using MVVM_MAUI_Simple_Todo_List.Models;
using SQLite;

namespace MVVM_MAUI_Simple_Todo_List.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;
    
    public DatabaseService()
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "todo.db3");
        _database = new SQLiteAsyncConnection(dbPath);
    }

    // create table 
    public async Task InitAsync()
    {
        await _database.CreateTableAsync<TodoItem>();
    }

    // select *
    public async Task<List<TodoItem>> GetItemsAsync()
    {
        await InitAsync();
        return await _database.Table<TodoItem>().ToListAsync();
    }

    // create, update
    public async Task<int> SaveItemAsync(TodoItem item)
    {
        await InitAsync();
        if (item.Id != 0)
        {
            return await _database.UpdateAsync(item);
        }
        else
        {
            return await _database.InsertAsync(item);
        }
    }

    // delete
    public async Task<int> DeleteItemAsync(TodoItem item)
    {
        await InitAsync();
        return await _database.DeleteAsync(item);
    }
}