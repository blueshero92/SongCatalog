using SongCatalog.Models;
using SongCatalog.Repositories.Contracts;
using SongCatalog.Services.Contracts;
using System.Text;

namespace SongCatalog.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            this.catalogRepository = catalogRepository;
        }

        public void ListCatalog()
        {
            //Variable to keep track of the song number in the catalog.
            int number = 1;

            //StringBuilder to build the output string for the catalog listing.
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("My Catalog:");

            foreach (Song song in catalogRepository.Catalog.OrderBy(s => s.ArtistName.ToLower())
                                                           .ThenBy(s => s.Title.ToLower())
                                                           .ThenByDescending(s => s.Rating))
            {
                sb.AppendLine($"{number}.{song.ArtistName} - {song.Title}, Rating: {song.Rating}");
                number++;
            }

            //Output the catalog listing to the console, trimming any trailing whitespace.
            Console.WriteLine(sb.ToString().Trim());
        }

        public void AddSong(string[] tokens)
        {
            //Validate the input tokens.
            string songTitle = tokens[1];
            string artistName = tokens[2];
            int rating = int.Parse(tokens[3]);

            //Create a new Song object with the provided details.
            Song song = new Song(songTitle, artistName, rating);

            //Check if the song already exists in the catalog (case-insensitive comparison).           
            if (catalogRepository.Catalog.Any(s => s.Title.Equals(songTitle, StringComparison.InvariantCultureIgnoreCase) 
                                                && s.ArtistName.Equals(artistName, StringComparison.InvariantCultureIgnoreCase)))
            {
                //If it exists, display a message indicating that the song already exists
                Console.WriteLine($"Song \"{songTitle}\" by \"{artistName}\" already exists in the catalog.");
            }
            else
            {
                //If it doesn't exist, add the song to the catalog and save the changes.
                catalogRepository.Catalog.Add(song);
                catalogRepository.SaveCatalog();

                Console.WriteLine($"Successfully added song: \"{songTitle}\" by \"{artistName}\".");
            }

        }
    }
}
