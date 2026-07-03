using SongCatalog.Models;
using SongCatalog.Repositories.Contracts;
using SongCatalog.Services.Contracts;
using System.Text;

using static SongCatalog.Common.AppConstants;

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
            int number = SongListNumber;

            //StringBuilder to build the output string for the catalog listing.
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("My Catalog:");
            sb.AppendLine("-----------");

            if (!catalogRepository.Catalog.Any())
            {
                sb.AppendLine(EmptyCatalogMessage);
            }
            else
            {
                foreach (Song song in catalogRepository.Catalog.OrderBy(s => s.ArtistName.ToLower())
                                                               .ThenBy(s => s.Title.ToLower())
                                                               .ThenByDescending(s => s.Rating))
                {
                    sb.AppendLine($"{number}.{song.ArtistName} - {song.Title}, Rating: {song.Rating}");
                    number++;
                }
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
                Console.WriteLine(string.Format(SongAlreadyExistsMessage, songTitle, artistName));
            }
            else
            {
                //If it doesn't exist, add the song to the catalog and save the changes.
                catalogRepository.Catalog.Add(song);
                catalogRepository.SaveCatalog();

                Console.WriteLine(string.Format(SongAddedSuccessfullyMessage, songTitle, artistName));
            }

        }

        public void RemoveSong(string title, string artistName)
        {
            //Find the song in the catalog using case-insensitive comparison for both title and artist name.
            Song? song = catalogRepository.Catalog.FirstOrDefault(s => s.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase)
                                                                    && s.ArtistName.Equals(artistName, StringComparison.InvariantCultureIgnoreCase));

            //If the song is found, remove it from the catalog and save the changes.
            if (song == null)
            {
                Console.WriteLine(string.Format(SongNotFoundMessage, title, artistName));
            }
            //If the song is not found, display a message indicating that the song was not found.
            else
            {
                catalogRepository.Catalog.Remove(song);
                catalogRepository.SaveCatalog();

                Console.WriteLine(string.Format(SongRemovedSuccessfullyMessage, title, artistName));
            }
        }
    }
}
