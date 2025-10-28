using MVVM_MAUI_Simple_Todo_List.ViewModels;

namespace MVVM_MAUI_Simple_Todo_List.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}