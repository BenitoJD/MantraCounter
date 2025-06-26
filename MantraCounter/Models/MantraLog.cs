using SQLite;

namespace MantraCounter.Models
{
    [Table("mantralogs")]
    public class MantraLog
    {
      
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        
        [Indexed]
        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}