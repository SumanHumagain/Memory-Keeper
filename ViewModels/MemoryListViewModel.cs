using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;
using Memory_App.Global;
using Memory_App.Models;

namespace Memory_App.ViewModels
{
    public class MemoryListViewModel : BindableObject
    {
        private readonly FirebaseClient _firebaseClient;

        public ObservableCollection<MemoryDetail> Memories { get; set; }

        public MemoryListViewModel()
        {
            _firebaseClient = new FirebaseClient("https://memorykeeper-8bcf4-default-rtdb.firebaseio.com/");
            Memories = new ObservableCollection<MemoryDetail>();
            LoadMemories();
        }

        public async void LoadMemories()
        {
            // Clear existing memories
            Memories.Clear();

            string userEmail = GlobalVariable.Email;

            // Fetch all memories
            var allMemories = await _firebaseClient
                .Child("MemoryDetail")
                .OnceAsync<MemoryDetail>();

            // Filter memories by user email
            var userMemories = allMemories
                .Where(memory => memory.Object.Email == userEmail)
                .Select(memory => memory.Object);

            // Add filtered memories to the collection
            foreach (var memory in userMemories)
            {
                Memories.Add(memory);
            }
        }
    }
}
