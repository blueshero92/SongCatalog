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

        public string ListCatalog()
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
            return sb.ToString().Trim();
        }

        public string AddSong(string[] tokens)
        {
            //StringBuilder to build the output string for messages related to adding a song.
            StringBuilder sb = new StringBuilder();

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
                sb.AppendLine(string.Format(SongAlreadyExistsMessage, songTitle, artistName));
            }
            else
            {
                //If it doesn't exist, add the song to the catalog and save the changes.
                catalogRepository.Catalog.Add(song);
                catalogRepository.SaveCatalog();

                sb.AppendLine(string.Format(SongAddedSuccessfullyMessage, songTitle, artistName));
            }

            return sb.ToString().Trim();

        }

        public string RemoveSong(string title, string artistName)
        {
            //StringBuilder to build the output string for messages related to removing a song.
            StringBuilder sb = new StringBuilder();

            //Find the song in the catalog using case-insensitive comparison for both title and artist name.
            Song? song = catalogRepository.Catalog.FirstOrDefault(s => s.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase)
                                                                    && s.ArtistName.Equals(artistName, StringComparison.InvariantCultureIgnoreCase));

            //If the song is found, remove it from the catalog and save the changes.
            if (song == null)
            {
                sb.AppendLine(string.Format(SongNotFoundMessage, title, artistName));
            }
            //If the song is not found, display a message indicating that the song was not found.
            else
            {
                catalogRepository.Catalog.Remove(song);
                catalogRepository.SaveCatalog();

                sb.AppendLine(string.Format(SongRemovedSuccessfullyMessage, title, artistName));
            }

            return sb.ToString().Trim();
        }

        public string SearchSongs(string searchQuery)
        {
            //StringBuilder to build the output string for the search results.
            StringBuilder results = new StringBuilder();

            //Search for songs in the catalog that match the search query in either the title or artist name (case-insensitive).
            IEnumerable<Song> songs = catalogRepository
                                     .Catalog.Where(s => s.Title.Contains(searchQuery, StringComparison.InvariantCultureIgnoreCase)
                                                      || s.ArtistName.Contains(searchQuery, StringComparison.InvariantCultureIgnoreCase))
                                             .ToList();

            //If no songs are found, display a message indicating that no results were found for the search query.
            if (!songs.Any())
            {
                results.AppendLine(string.Format(ResultsNotFoundMessage, searchQuery));

                return results.ToString().Trim();
            }
            else
            {
                results.AppendLine(string.Format(ResultsFound));

                //If songs are found, display the search results with the song title and artist name.
                foreach (Song song in songs)
                {
                    results.AppendLine(string.Format(SongFoundMessage, song.Title, song.ArtistName));
                }

                return results.ToString().Trim();
            }
        }
    }
}
