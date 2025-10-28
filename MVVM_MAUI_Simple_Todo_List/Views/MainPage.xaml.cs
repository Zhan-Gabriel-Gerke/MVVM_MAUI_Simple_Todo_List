using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_MAUI_Simple_Todo_List.ViewModels;

namespace MVVM_MAUI_Simple_Todo_List.Views;

public partial class MainPage : ContentPage
{
    // Мы "внедряем" ViewModel через конструктор
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm; // Устанавливаем DataContext (контекст привязки)
    }
}