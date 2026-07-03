namespace SongCatalog.Models
{
    public class Song
    {
        public Song(string title, string artistName, int rating)
        {
            Title = title;
            ArtistName = artistName;
            Rating = rating;
        }

        public string Title { get; set; } = null!;

        public string ArtistName { get; set; } = null!;

        public int Rating { get; set; }
    }
}
