using Firebase.Auth.Providers;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Memory_App.Models;

namespace Memory_App.Services
{
    public class FirebaseDb
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseDb()
        {
            _firebaseClient = new FirebaseClient("https://memorykeeper-8bcf4-default-rtdb.firebaseio.com/");
        }

        public async Task<MemoryDetail> GetMemories()
        {
           var memories = await _firebaseClient.Child("MemoryDetail").OnceAsync<MemoryDetail>();
           return (MemoryDetail)memories;
        }
    }
}
