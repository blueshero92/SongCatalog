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

        public string ListCatalog(List<Song> catalog)
        {

            //Variable to keep track of the song number in the catalog.
            int number = SongListNumber;

            //StringBuilder to build the output string for the catalog listing.
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("My Catalog:");
            sb.AppendLine("-----------");

            if (!catalog.Any())
            {
                sb.AppendLine(EmptyCatalogMessage);
            }
            else
            {
                foreach (Song song in catalog.OrderBy(s => s.ArtistName.ToLower())
                                             .ThenBy(s => s.Title.ToLower())
                                             .ThenByDescending(s => s.Rating))
                {
                    sb.AppendLine($"{number}.{song.ArtistName} - {song.Title}, Rating: {song.Rating:F2}");
                    number++;
                }
            }

            //Output the catalog listing to the console, trimming any trailing whitespace.
            return sb.ToString().Trim();


        }

        public string AddSong(string[] tokens, List<Song> catalog)
        {
            //StringBuilder to build the output string for messages related to adding a song.
            StringBuilder sb = new StringBuilder();

            //Validate the input tokens.
            string songTitle = tokens[1];
            string artistName = tokens[2];
            float rating = float.Parse(tokens[3]);

            //Check if the rating is within the valid range (1 to 5). If not, display an error message.
            if (rating < RatingMinValue || rating > RatingMaxValue)
            {
                sb.AppendLine(string.Format(InvalidRatingMessage, RatingMinValue, RatingMaxValue));

                return sb.ToString().Trim();
            }

            //Create a new Song object with the provided details.
            Song song = new Song(songTitle, artistName, rating);

            //Check if the song already exists in the catalog (case-insensitive comparison).           
            if (catalog.Any(s => s.Title.Equals(songTitle, StringComparison.InvariantCultureIgnoreCase)
                                                && s.ArtistName.Equals(artistName, StringComparison.InvariantCultureIgnoreCase)))
            {
                //If it exists, display a message indicating that the song already exists
                sb.AppendLine(string.Format(SongAlreadyExistsMessage, songTitle, artistName));
            }
            else
            {
                //If it doesn't exist, add the song to the catalog and save the changes.
                catalog.Add(song);
                catalogRepository.SaveCatalog(JsonFilePath, catalog);

                sb.AppendLine(string.Format(SongAddedSuccessfullyMessage, songTitle, artistName));
            }

            return sb.ToString().Trim();

        }

        public string RemoveSong(string title, string artistName, List<Song> catalog)
        {
            //StringBuilder to build the output string for messages related to removing a song.
            StringBuilder sb = new StringBuilder();

            //Find the song in the catalog using case-insensitive comparison for both title and artist name.
            Song? song = catalog.FirstOrDefault(s => s.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase)
                                                                    && s.ArtistName.Equals(artistName, StringComparison.InvariantCultureIgnoreCase));

            //If the song is found, remove it from the catalog and save the changes.
            if (song == null)
            {
                sb.AppendLine(string.Format(SongNotFoundMessage, title, artistName));
            }
            //If the song is not found, display a message indicating that the song was not found.
            else
            {
                catalog.Remove(song);
                catalogRepository.SaveCatalog(JsonFilePath, catalog);

                sb.AppendLine(string.Format(SongRemovedSuccessfullyMessage, title, artistName));
            }

            return sb.ToString().Trim();
        }

        public string SearchSongs(string searchQuery, List<Song> catalog)
        {
            //StringBuilder to build the output string for the search results.
            StringBuilder results = new StringBuilder();

            //Search for songs in the catalog that match the search query in either the title or artist name (case-insensitive).
            IEnumerable<Song> songs = catalog.Where(s => s.Title.Contains(searchQuery, StringComparison.InvariantCultureIgnoreCase)
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

        public string SortCatalogByArtist(List<Song> catalog)
        {
            //Variable to keep track of the song number in the sorted catalog.
            int number = SongListNumber;

            //StringBuilder to build the output string for the sorted catalog by artist name.
            StringBuilder sortedByTitle = new StringBuilder();

            //Sort the catalog by artist name in ascending order (case-insensitive).
            IEnumerable<Song> sortedCatalog = catalog
                                             .OrderBy(s => s.ArtistName.ToLower())
                                             .ToList();

            //If the sorted catalog is empty, display a message indicating that the catalog is empty.
            if (!sortedCatalog.Any())
            {
                sortedByTitle.AppendLine(EmptyCatalogMessage);
            }
            else
            {
                //If the sorted catalog is not empty, display the sorted catalog with the artist name and song title.
                sortedByTitle.AppendLine(CatalogSortedByArtist);

                foreach (Song song in sortedCatalog)
                {
                    sortedByTitle.AppendLine($"{number}. {song.ArtistName} - {song.Title} - {song.Rating:F2}");

                    number++;
                }
            }

            return sortedByTitle.ToString().Trim();
        }

        public string SortCatalogByTitle(List<Song> catalog)
        {
            //Variable to keep track of the song number in the sorted catalog.
            int number = SongListNumber;

            //StringBuilder to build the output string for the sorted catalog by title.
            StringBuilder sortedByTitle = new StringBuilder();

            //Sort the catalog by title in ascending order (case-insensitive).
            IEnumerable<Song> sortedCatalog = catalog
                                             .OrderBy(s => s.Title.ToLower())
                                             .ToList();

            //If the sorted catalog is empty, display a message indicating that the catalog is empty.
            if (!sortedCatalog.Any())
            {
                sortedByTitle.AppendLine(EmptyCatalogMessage);
            }
            else
            {
                //If the sorted catalog is not empty, display the sorted catalog with the title and artist name.
                sortedByTitle.AppendLine(CatalogSortedByTitle);

                foreach (Song song in sortedCatalog)
                {
                    sortedByTitle.AppendLine($"{number}. {song.Title} - {song.ArtistName} - {song.Rating:F2}");
                    number++;
                }
            }

            return sortedByTitle.ToString().Trim();
        }

        public string SortCatalogByRating(List<Song> catalog)
        {
            //Variable to keep track of the song number in the sorted catalog.
            int number = SongListNumber;

            //StringBuilder to build the output string for the sorted catalog by rating.
            StringBuilder sortedByRating = new StringBuilder();

            //Sort the catalog by rating in descending order.
            IEnumerable<Song> sortedCatalog = catalog
                                             .OrderByDescending(s => s.Rating)
                                             .ToList();

            //If the sorted catalog is empty, display a message indicating that the catalog is empty.
            if (!sortedCatalog.Any())
            {
                sortedByRating.AppendLine(EmptyCatalogMessage);
            }
            else
            {
                //If the sorted catalog is not empty, display the sorted catalog with the rating, title, and artist name.
                sortedByRating.AppendLine(CatalogSortedByRating);

                foreach (Song song in sortedCatalog)
                {
                    sortedByRating.AppendLine($"{number}. {song.Title} - {song.ArtistName} - {song.Rating:F2}");
                    number++;
                }
            }

            return sortedByRating.ToString().Trim();
        }

        public string MergeFriendCatalog(List<Song> myCatalog, List<Song> friendCatalog)
        {
            //Merge the friend's catalog into the user's catalog, avoiding duplicates based on artist name and title (case-insensitive).
            myCatalog.AddRange(friendCatalog
                              .Where(s => !myCatalog
                                          .Any(ms => ms.ArtistName.Equals(s.ArtistName, StringComparison.InvariantCultureIgnoreCase) &&
                                                     ms.Title.Equals(s.Title, StringComparison.InvariantCultureIgnoreCase))));

            //Save the merged catalog to the JSON file.
            catalogRepository.SaveCatalog(JsonFilePath, myCatalog);

            return MergeSuccessfulMessage;
        }

        public void MergeExternalCatalog(List<Song> myCatalog, string filePath)
        {
            //Load the friend's catalog from the specified file path.
            List<Song> friendCatalog = catalogRepository.LoadCatalog(filePath);

            MergeFriendCatalog(myCatalog, friendCatalog);

            Console.WriteLine(MergeSuccessfulMessage);
        }
    }
}
