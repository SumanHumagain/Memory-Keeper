namespace Memory_App.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnAddMemoryClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddMemory());
    }
}