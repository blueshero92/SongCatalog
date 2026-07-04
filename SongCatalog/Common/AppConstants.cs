namespace SongCatalog.Common
{
    public static class AppConstants
    {
        //Json file path for storing the song catalog.
        public static string JsonFilePath = "../../../Files/songCatalog.json";

        //Variable to keep track of the song number in the catalog listing.
        public static int SongListNumber = 1;

        //Constant for empty catalog message.
        public const string EmptyCatalogMessage = "The catalog is currently empty.";

        // Constant for song already exists message, with placeholders for song title and artist name.
        public const string SongAlreadyExistsMessage = "Song \"{0}\" by \"{1}\" already exists in the catalog.";

        // Constant for song added successfully message, with placeholders for song title and artist name.
        public const string SongAddedSuccessfullyMessage = "Song \"{0}\" by \"{1}\" successfully added to the catalog.";

        // Constant for search results not found message, with placeholders for search query.
        public const string ResultsNotFoundMessage = "No results matching \"{0}\" in the catalog.";

        // Constant for song removed successfully message, with placeholders for song title and artist name.
        public const string SongRemovedSuccessfullyMessage = "Song \"{0}\" by \"{1}\" successfully removed from the catalog.";

        // Constant for song found message, with placeholders for song title and artist name.
        public const string SongFoundMessage = "Song \"{0}\" by \"{1}\" found in the catalog.";

        // Constant for song not found message, with placeholders for song title and artist name.
        public const string SongNotFoundMessage = "Song \"{0}\" by \"{1}\" not found in the catalog.";

        // Constant for search results found message.
        public const string ResultsFound = "Search results:";

    }
}
