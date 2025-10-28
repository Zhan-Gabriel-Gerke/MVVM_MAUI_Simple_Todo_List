using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using MVVM_MAUI_Simple_Todo_List.ViewModels;

namespace MVVM_MAUI_Simple_Todo_List.Views;

public partial class MainPage : ContentPage
{
    // Нам нужна ссылка на ViewModel, чтобы вызвать его метод
    private readonly MainPageViewModel _viewModel;

    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm; // Сохраняем ссылку
    }

    // Этот метод вызывается каждый раз, когда страница становится видимой
    protected override void OnAppearing()
    {
        base.OnAppearing();
            
        // Запускаем команду загрузки данных
        // (Не ждем ее завершения, чтобы не блокировать UI)
        _viewModel.LoadItemsCommand.Execute(null);
    }
}