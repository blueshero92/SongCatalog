namespace SongCatalog.Models
{
    public class HistoryEntry
    {
        public string OperationType { get; set; } = null!;

        public Song Song { get; set; } = null!;

        public List<Song>? Songs { get; set; }
    }
}
