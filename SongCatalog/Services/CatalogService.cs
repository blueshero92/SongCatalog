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
        private readonly IHistoryRepository historyRepository;

        public CatalogService(ICatalogRepository catalogRepository, IHistoryRepository historyRepository)
        {
            this.catalogRepository = catalogRepository;
            this.historyRepository = historyRepository;
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
                foreach (Song song in catalog)
                {
                    sb.AppendLine($"{number}.{song.ArtistName} - {song.Title}, Rating: {song.Rating:F2}");
                    number++;
                }
            }

            //Output the catalog listing to the console, trimming any trailing whitespace.
            return sb.ToString().Trim();


        }

        public string AddSong(string[] tokens, List<Song> catalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory)
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

                return sb.ToString().Trim();
            }

            //If it doesn't exist, add the song to the catalog and save the changes.
            catalog.Add(song);
            catalogRepository.SaveCatalog(JsonFilePath, catalog);

            //Add the operation to the undo history to allow for undoing the addition of the song, and save the undo history to the file.
            undoHistory.Add(new HistoryEntry { OperationType = "Add", Song = song });

            historyRepository.SaveHistory(UndoHistoryFilePath, undoHistory);

            //Clear the redo history to prevent redoing actions after a new operation has been performed, and save the cleared redo history to the file.
            redoHistory.Clear();

            historyRepository.SaveHistory(RedoHistoryFilePath, redoHistory);


            sb.AppendLine(string.Format(SongAddedSuccessfullyMessage, songTitle, artistName));

            return sb.ToString().Trim();

        }

        public string RemoveSong(string title, string artistName, List<Song> catalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory)
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
                return sb.ToString().Trim();
            }

            //If the song is not found, display a message indicating that the song was not found.

            catalog.Remove(song);
            catalogRepository.SaveCatalog(JsonFilePath, catalog);

            //Add the operation to the undo history to allow for undoing the removal of the song, and save the undo history to the file.
            undoHistory.Add(new HistoryEntry { OperationType = "Remove", Song = song });

            historyRepository.SaveHistory(UndoHistoryFilePath, undoHistory);

            //Clear the redo history to prevent redoing actions after a new operation has been performed, and save the cleared redo history to the file.
            redoHistory.Clear();

            historyRepository.SaveHistory(RedoHistoryFilePath, redoHistory);


            sb.AppendLine(string.Format(SongRemovedSuccessfullyMessage, title, artistName));

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

        public string MergeFriendCatalog(List<Song> myCatalog, List<Song> friendCatalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory)
        {
            //Determine which songs from the friend's catalog are new (not already in myCatalog).
            List<Song> songsToAdd = friendCatalog
                                    .Where(s => !myCatalog
                                                .Any(ms => ms.ArtistName.Equals(s.ArtistName, StringComparison.InvariantCultureIgnoreCase) &&
                                                           ms.Title.Equals(s.Title, StringComparison.InvariantCultureIgnoreCase)))
                                    .ToList();

            //If there are no new songs, return without modifying the history.
            if (!songsToAdd.Any())
            {
                return MergeSuccessfulMessage;
            }

            //Merge only the new songs into the user's catalog.
            myCatalog.AddRange(songsToAdd);

            //Save the merged catalog to the JSON file.
            catalogRepository.SaveCatalog(JsonFilePath, myCatalog);

            //Add the merge operation to the undo history as a single atomic entry.
            undoHistory.Add(new HistoryEntry { OperationType = "Merge", Songs = songsToAdd });

            historyRepository.SaveHistory(UndoHistoryFilePath, undoHistory);

            //Clear the redo history to prevent redoing actions after a new operation has been performed.
            redoHistory.Clear();

            historyRepository.SaveHistory(RedoHistoryFilePath, redoHistory);

            return MergeSuccessfulMessage;
        }

        public void MergeExternalCatalog(List<Song> myCatalog, string filePath, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory)
        {
            //Load the friend's catalog from the specified file path.
            List<Song> friendCatalog = catalogRepository.LoadCatalog(filePath);

            MergeFriendCatalog(myCatalog, friendCatalog, undoHistory, redoHistory);

            Console.WriteLine(MergeSuccessfulMessage);
        }

        public string ChangeRating(string title, string artistName, float newRating, List<Song> catalog)
        {
            //Find the song in the catalog based on the title and artist name (case-insensitive).
            Song? song = catalog.SingleOrDefault(s => s.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase) &&
                                                      s.ArtistName.Equals(artistName, StringComparison.InvariantCultureIgnoreCase));

            //If the song is not found, return -1 to indicate that the song does not exist in the catalog.
            if (song == null)
            {
                return string.Format(SongNotFoundMessage, title, artistName);
            }

            //Update the rating of the found song.
            song.Rating = newRating;

            //Save the updated catalog to the JSON file.
            catalogRepository.SaveCatalog(JsonFilePath, catalog);

            //Return a message indicating that the rating has been changed successfully.
            return string.Format(RatingChangedSuccessfullyMessage, title, artistName, newRating);
        }

        public void Undo(List<Song> catalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory)
        {
            //Check if there are any operations in the undo history. If not, display a message and return.
            if (!undoHistory.Any())
            {
                Console.WriteLine("Nothing to undo.");
                return;
            }

            //Get the last operation from the undo history to determine what action to undo.
            HistoryEntry lastOperation = undoHistory.Last();

            //If the last operation was an "Add" operation, remove the song from the catalog. If it was a "Remove" operation, add the song back to the catalog.
            if (lastOperation.OperationType == "Add")
            {
                Song? songToRemove = catalog
                                    .SingleOrDefault(s => s.Title.Equals(lastOperation.Song.Title, StringComparison.InvariantCultureIgnoreCase)
                                                       && s.ArtistName.Equals(lastOperation.Song.ArtistName, StringComparison.InvariantCultureIgnoreCase));


                //If the song to remove is found, remove it from the catalog.
                if (songToRemove != null)
                {
                    catalog.Remove(songToRemove);
                }

            }
            else if (lastOperation.OperationType == "Remove")
            {
                bool alreadyExists = catalog.Any(s => s.Title.Equals(lastOperation.Song.Title, StringComparison.InvariantCultureIgnoreCase)
                                                   && s.ArtistName.Equals(lastOperation.Song.ArtistName, StringComparison.InvariantCultureIgnoreCase));

                if (!alreadyExists)
                {
                    catalog.Add(lastOperation.Song);
                }
                else
                {
                    Console.WriteLine($"Cannot undo: \"{lastOperation.Song.Title}\" by {lastOperation.Song.ArtistName} already exists in the catalog.");
                    return;
                }
            }
            else if (lastOperation.OperationType == "Merge")
            {
                //Remove each song that was added by the merge.
                foreach (Song mergedSong in lastOperation.Songs!)
                {
                    Song? songToRemove = catalog
                                        .SingleOrDefault(s => s.Title.Equals(mergedSong.Title, StringComparison.InvariantCultureIgnoreCase)
                                                           && s.ArtistName.Equals(mergedSong.ArtistName, StringComparison.InvariantCultureIgnoreCase));

                    if (songToRemove != null)
                    {
                        catalog.Remove(songToRemove);
                    }
                }
            }

            //Remove the last operation from the undo history and add it to the redo history for potential redo actions.
            undoHistory.RemoveAt(undoHistory.Count - 1);
            redoHistory.Add(lastOperation);

            //Save the updated repositories.
            catalogRepository.SaveCatalog(JsonFilePath, catalog);
            historyRepository.SaveHistory(UndoHistoryFilePath, undoHistory);
            historyRepository.SaveHistory(RedoHistoryFilePath, redoHistory);

            Console.WriteLine("Undo operation completed successfully.");

        }

        public void Redo(List<Song> catalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory)
        {
            //Check if there are any operations in the redo history. If not, display a message and return.
            if (!redoHistory.Any())
            {
                Console.WriteLine("Nothing to redo.");
                return;
            }

            //Get the last operation from the redo history to determine what action to redo.
            HistoryEntry lastOperation = redoHistory.Last();

            //If the last operation was an "Add" operation, remove the song from the catalog. If it was a "Remove" operation, add the song back to the catalog.
            if (lastOperation.OperationType == "Add")
            {
                bool alreadyExists = catalog.Any(s => s.Title.Equals(lastOperation.Song.Title, StringComparison.InvariantCultureIgnoreCase)
                                                   && s.ArtistName.Equals(lastOperation.Song.ArtistName, StringComparison.InvariantCultureIgnoreCase));

                if (!alreadyExists)
                {
                    catalog.Add(lastOperation.Song);
                }
                else
                {
                    Console.WriteLine($"Cannot redo: \"{lastOperation.Song.Title}\" by {lastOperation.Song.ArtistName} already exists in the catalog.");
                    return;
                }
            }
            else if (lastOperation.OperationType == "Remove")
            {
                Song? songToRemove = catalog
                                    .SingleOrDefault(s => s.Title.Equals(lastOperation.Song.Title, StringComparison.InvariantCultureIgnoreCase)
                                                       && s.ArtistName.Equals(lastOperation.Song.ArtistName, StringComparison.InvariantCultureIgnoreCase));

                //If the song to remove is found, remove it from the catalog.
                if (songToRemove != null)
                {
                    catalog.Remove(songToRemove);
                }

            }
            else if (lastOperation.OperationType == "Merge")
            {
                //Check all songs before re-adding any, to avoid a partial re-merge.
                List<Song> songsToReAdd = lastOperation.Songs!
                                         .Where(s => !catalog.Any(ms => ms.Title.Equals(s.Title, StringComparison.InvariantCultureIgnoreCase)
                                                                     && ms.ArtistName.Equals(s.ArtistName, StringComparison.InvariantCultureIgnoreCase)))
                                         .ToList();

                if (songsToReAdd.Count != lastOperation.Songs!.Count)
                {
                    Console.WriteLine("Cannot redo: one or more merged songs already exist in the catalog.");
                    return;
                }

                catalog.AddRange(songsToReAdd);
            }

            //Remove the last operation from the redo history and add it to the undo history for potential undo actions.
            redoHistory.RemoveAt(redoHistory.Count - 1);
            undoHistory.Add(lastOperation);

            //Save the updated repositories.
            catalogRepository.SaveCatalog(JsonFilePath, catalog);
            historyRepository.SaveHistory(UndoHistoryFilePath, undoHistory);
            historyRepository.SaveHistory(RedoHistoryFilePath, redoHistory);

            Console.WriteLine("Redo operation completed successfully.");
        }
    }
}
