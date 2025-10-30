using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace MVVM_MAUI_Simple_Todo_List.Models
{
    [Table("TodoItems")]
    public partial class TodoItem : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // --- ИСПРАВЛЕНИЕ: Определяем свойство Text вручную ---
        private string _text; // Приватное поле для хранения значения

        [MaxLength(250)] // Атрибут SQLite теперь применяется к свойству
        public string Text
        {
            get => _text;
            // Используем SetProperty из ObservableObject для уведомления
            set => SetProperty(ref _text, value);
        }
        // ---------------------------------------------------

        // IsDone оставляем с [ObservableProperty], т.к. ему не нужны атрибуты SQLite
        [ObservableProperty]
        bool isDone;
    }
}