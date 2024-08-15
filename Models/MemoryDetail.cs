//using SQLite;

namespace Memory_App.Models
{

        public class MemoryDetail : IEntity
        {
            //[PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Email { get; set; }
            public string Location { get; set; }
            public string Caption { get; set; }
            public string Image { get; set; }
            public Stream ImageUploaded { get; set; }
        }
}