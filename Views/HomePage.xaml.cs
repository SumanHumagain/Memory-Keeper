using Memory_App.Global;
using Microsoft.Extensions.Logging.Abstractions;

namespace Memory_App.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        UserEmailLabel.Text = GlobalVariable.Email;
    }

    private async void OnAddMemoryClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddMemory());
    }
    private async void OnViewMemoryClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ViewAll());
    }
    private async void OnSearchMemoryClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SearchPage());
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        GlobalVariable.Email = null;
        await Navigation.PushAsync(new LoginPage());
    }
    protected override bool OnBackButtonPressed()
    {
        // Prevent the back navigation
        return true;
    }
}