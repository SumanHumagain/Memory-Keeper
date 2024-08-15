using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Firebase.Storage;
using Memory_App.Global;
using Memory_App.Models;
using Memory_App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Memory_App.Services.SessionService;

namespace Memory_App.ViewModels
{
    public class SearchViewModel : ObservableObject
    {
        private readonly FirebaseClient _firebaseClient;
        private readonly FirebaseStorage _firebaseStorage;
        public IAsyncRelayCommand SearchMemoryCommand { get; }
        public string Keyword { get; set; }
        public ObservableCollection<MemoryDetail> MemoryDetails { get; } = new ObservableCollection<MemoryDetail>();
        private readonly IUserSessionService _userSessionService;

        public SearchViewModel()
        {
            SearchMemoryCommand = new AsyncRelayCommand(SearchMemory);
            _firebaseStorage = new FirebaseStorage("memorykeeper-8bcf4.appspot.com");
            _firebaseClient = new FirebaseClient("https://memorykeeper-8bcf4-default-rtdb.firebaseio.com/");
        }
        public SearchViewModel([Optional] IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
            SearchMemoryCommand = new AsyncRelayCommand(SearchMemory);
            _firebaseStorage = new FirebaseStorage("memorykeeper-8bcf4.appspot.com");
            _firebaseClient = new FirebaseClient("https://memorykeeper-8bcf4-default-rtdb.firebaseio.com/");
        }

        private async Task SearchMemory()
        {
            if (string.IsNullOrWhiteSpace(Keyword))
                return;

            // Retrieve all memory details from Firebase
            var allMemories = await _firebaseClient
                .Child("MemoryDetail")
                .OnceAsync<MemoryDetail>();

            // Get the email of the currently logged-in user
            string userEmail = GlobalVariable.Email;

            // Filter memories by both keyword and user email
            var filteredMemories = allMemories
                .Where(m => m.Object.Caption != null &&
                            m.Object.Caption.Contains(Keyword, StringComparison.OrdinalIgnoreCase) &&
                            m.Object.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
                .Select(m => m.Object);

            // Update the collection of memories in the view model
            MemoryDetails.Clear();
            foreach (var memory in filteredMemories)
            {
                MemoryDetails.Add(memory);
            }
        }


    }
}