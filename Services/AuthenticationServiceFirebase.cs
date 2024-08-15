using Firebase.Auth.Providers;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Memory_App.Services.SessionService;
using System.Runtime.InteropServices;
using Memory_App.Global;

namespace Memory_App.Services
{
    public class AuthenticationServiceFirebase
    {
        private static AuthenticationServiceFirebase _instance;
        public static AuthenticationServiceFirebase Instance => _instance ??= new AuthenticationServiceFirebase();

        private FirebaseAuthClient _client;

        private readonly IUserSessionService _userSessionService;
        private readonly AnalyticsService _analyticsService;

        //GlobalVariable s = new GlobalVariable();

        //public AuthenticationServiceFirebase()
        //{
        //    InitializeFirebase();
        //}
        public AuthenticationServiceFirebase([Optional] IUserSessionService userSessionService)
        {
            InitializeFirebase();
            _analyticsService = new AnalyticsService("be5b08d35d1dcfa6dd657f9e7e896ba6");
            _userSessionService = userSessionService;
        }
        public void InitializeFirebase()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyAGEq2UQwNHIR4Aflo0jBma9axkyfJlMX4",
                AuthDomain = "memorykeeper-8bcf4.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
             {
                    new EmailProvider()
             }
            };

            _client = new FirebaseAuthClient(config);
        }

        public async Task<(bool success, string message)> SignInAsync(string email, string password)
        {
            InitializeFirebase();
      
            string message;
            var success = false;

            try
            {
                var userCredential = await _client.SignInWithEmailAndPasswordAsync(email, password);
                message = $"Signed in as {userCredential.User.Info.Email}";
                // Store user name
                GlobalVariable.Email = userCredential.User.Info.Email;
                success = true;
            }
            catch (FirebaseAuthHttpException ex)
            {
                message = ex.Reason.ToString() == "Unknown" ? "Incorrect email or password." : ex.Reason.ToString();
            }
            catch (Exception ex)
            {
                message = $"An error occurred: {ex.Message}";
            }

            return (success, message);
        }

        public async Task<(bool success, string message)> SignUpAsync(string email, string password)
        {
            InitializeFirebase();
            string message;
            var success = false;

            try
            {
                var userCredential = await _client.CreateUserWithEmailAndPasswordAsync(email, password);
                message = $"Account created for {userCredential.User.Info.Email}";
                GlobalVariable.Email = userCredential.User.Info.Email;
                _analyticsService.TrackEvent("Register", new { Email = GlobalVariable.Email });

                success = true;
            }
            catch (FirebaseAuthHttpException ex)
            {
                message = ex.Reason.ToString();
            }
            catch (Exception ex)
            {
                message = $"An error occurred: {ex.Message}";
            }

            return (success, message);
        }

    }
}
