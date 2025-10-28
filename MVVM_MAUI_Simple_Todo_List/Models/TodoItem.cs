using SQLite;

namespace MVVM_MAUI_Simple_Todo_List.Models;

[Table("TodoItems")] // Название таблицы в базе данных
public class TodoItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } // Уникальный ID, будет присваиваться автоматически

    [MaxLength(250)]
    public string Text { get; set; }

    public bool IsDone { get; set; }
}