using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace MVVM_MAUI_Simple_Todo_List.Models
{
    [Table("TodoItems")]
    public partial class TodoItem : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _text = string.Empty; //Value

        [MaxLength(250)] // SQLite
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
        
        [ObservableProperty]
        bool isDone;
    }
}