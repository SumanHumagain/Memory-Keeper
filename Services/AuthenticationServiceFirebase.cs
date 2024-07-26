using Firebase.Auth.Providers;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_App.Services
{
    public class AuthenticationServiceFirebase
    {
        private static AuthenticationServiceFirebase _instance;
        public static AuthenticationServiceFirebase Instance => _instance ??= new AuthenticationServiceFirebase();

        private FirebaseAuthClient _client;

        private AuthenticationServiceFirebase()
        {
        }

        
    }
}
