using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;
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

        private async void LoadMemories()
        {
            var memories = await _firebaseClient.Child("MemoryDetail").OnceAsync<MemoryDetail>();
            foreach (var memory in memories)
            {
                Memories.Add(memory.Object);
            }
            //var memories = d.GetMemories();
            //foreach (var memory in memories)
            //{
            //    Memories.Add(memory.Object);
            //}
        }
    }
}
