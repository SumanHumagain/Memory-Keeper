using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using Memory_App.Services;

namespace Memory_App.ViewModels
{
    public class LoginViewModel : ObservableRecipient
    {
        public string Email { get; set; }
        public string Password { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        protected static AuthenticationServiceFirebase Authentication => AuthenticationServiceFirebase.Instance;

        public IAsyncRelayCommand GetCommand { get; }
        public IAsyncRelayCommand SignInCommand { get; }
        public IAsyncRelayCommand SignUpCommand { get; }

        public LoginViewModel()
        {
                GetCommand = new AsyncRelayCommand(Get);
                SignInCommand = new AsyncRelayCommand(SignIn);
                SignUpCommand = new AsyncRelayCommand<Activity>(SignUp);
        }

        private async Task Get()
        {
            //Activities = await Database.GetAsync<Activity>();
            //OnPropertyChanged(nameof(Activities));
        }

        private async Task SignIn()
        {
            var (success, message) = await Authentication.SignInAsync(Email, Password);
            Message = message;

            if (success)
            {
                //await Shell.Current.GoToAsync($"///{nameof(Views.ActivityListView)}");
            }
        }

        private async Task SignUp(Activity activity)
        {
            var (success, message) = await Authentication.SignUpAsync(Email, Password);
            Message = message;

            if (success)
            {
                //await Shell.Current.GoToAsync($"///{nameof(Views.ActivityListView)}");
            }

            //await Shell.Current.GoToAsync($"///{nameof(Views.ActivityDetailsView)}?id={activity.Id}");
        }
    }
}
