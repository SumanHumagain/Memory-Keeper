using Memory_App.Global;
using Memory_App.Services;
using Memory_App.ViewModels;
using Mixpanel;

namespace Memory_App.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly AnalyticsService _analyticsService;
        //private readonly MixpanelClient _mixpanelClient;
        public LoginPage()
        {
            //_mixpanelClient = new MixpanelClient("be5b08d35d1dcfa6dd657f9e7e896ba6");
            _analyticsService = new AnalyticsService("be5b08d35d1dcfa6dd657f9e7e896ba6");
            InitializeComponent();
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                var viewModel = BindingContext as LoginViewModel;

                bool isSuccess = await viewModel.SignIn();
                if (isSuccess)
                {
                    //await DisplayAlert("Login Success", "Welcome to memory keeper!", "OK");

                    _analyticsService.TrackEvent("User_Info", new { Email = GlobalVariable.Email });
                    //_analyticsService.TrackEvent("Login", new Dictionary<string, object>
                    //{
                    //    { "User_Info", new { Email = GlobalVariable.Email } }
                    //});

                    await Navigation.PushAsync(new HomePage());

                }

                // Navigate to the main page or another page after successful login
                //await Navigation.PushAsync(new HomePage());
                else
                {
                    await DisplayAlert("Login Failed", "Invalid username or password", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Login Failed.", " Try Again!", "OK");

            }
        }
    }

}
