using MantraCounter.ViewModels;

namespace MantraCounter.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm) 
    {
        InitializeComponent();
        BindingContext = vm; // Set the page's data source to our ViewModel
    }

    // This method is called every time the page appears on screen.
    // It's a great place to load or refresh data.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainViewModel vm)
        {
            // We execute the command we defined in our ViewModel
            await vm.LoadDataCommand.ExecuteAsync(null);
        }
    }
}