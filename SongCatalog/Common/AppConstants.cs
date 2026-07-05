namespace SongCatalog.Common
{
    public static class AppConstants
    {
        //Json file path for storing the song catalog. Path.Combine is used to create a platform-independent file path.
        public static readonly string JsonFilePath = Path.Combine(AppContext.BaseDirectory, "Files", "song-catalog.json");

        //Json file path for storing the friend catalog. Path.Combine is used to create a platform-independent file path.
        public static readonly string JsonFilePathFriendCatalog = Path.Combine(AppContext.BaseDirectory, "Files", "friend-catalog.json");

        //Json file path for storing the undo history. Path.Combine is used to create a platform-independent file path.
        public static readonly string UndoHistoryFilePath = Path.Combine(AppContext.BaseDirectory, "Files", "undo-history.json");

        //Json file path for storing the redo history. Path.Combine is used to create a platform-independent file path.
        public static readonly string RedoHistoryFilePath = Path.Combine(AppContext.BaseDirectory, "Files", "redo-history.json");

        //The following lines are commented out because they use relative paths that may not work correctly in all environments. 
        //They are used for development purposes and can be uncommented if needed.

        //public static readonly string JsonFilePath = "../../../Files/song-catalog.json";
        //public static readonly string JsonFilePathFriendCatalog = "../../../Files/friend-catalog.json";
        //public static readonly string UndoHistoryFilePath = "../../../Files/undo-history.json";
        //public static readonly string RedoHistoryFilePath = "../../../Files/redo-history.json";


        public const string HelpMessage = "Song Catalog CLI Help\r\n---------------------\r\n\r\n" +
            "Commands use '|' as a separator.\r\n\r\nGENERAL COMMANDS\r\n-----------------\r\nlist\r\n" +
            "Displays all songs in your catalog.\r\n\r\nadd|title|artist|rating\r\nAdds a new song to the catalog.\r\n" +
            "Example: add|Numb|Linkin Park|5\r\n\r\nremove|title|artist\r\n" +
            "Removes a song from the catalog.\r\n" +
            "Example: remove|Numb|Linkin Park\r\n\r\nsearch|keyword\r\n" +
            "Searches songs by title or artist (partial, case-insensitive).\r\n" +
            "Example: search|linkin\r\n\r\nchange rating|title|artist|newRating\r\n" +
            "Changes the rating of an existing song.\r\nExample: change rating|Numb|Linkin Park|4\r\n\r\nsort artist\r\n" +
            "Sorts songs by artist name.\r\n\r\nsort title\r\nSorts songs by song title.\r\n\r\nsort rating\r\n" +
            "Sorts songs by rating.\r\n\r\nMERGE COMMANDS\r\n---------------\r\nmerge\r\n" +
            "Merges your catalog with the built-in friend catalog.\r\n\r\nmerge external|filePath\r\n" +
            "Merges a catalog from an external file.\r\nExample: merge external|C:\\Users\\User\\Desktop\\catalog.json\r\n\r\nUNDO / REDO\r\n-----------\r\nundo\r\n" +
            "Reverts the last change (add/remove/merge).\r\n\r\nredo\r\n" +
            "Re-applies the last undone change.\r\n\r\nEXIT\r\n-----\r\nexit\r\n" +
            "Closes the application.";

        public const string OpeningMessage = "Welcome to the Song Catalog CLI! Type 'help' and press 'Enter' to get a small manual for using the tool.";

        //Constant for the message displayed when a file does not exist.
        public const string FileDoesNotExistMessage = "File does not exist.";

        //Variable to keep track of the song number in the catalog listing.
        public static int SongListNumber = 1;

        //Constant for the minimum rating value for a song.
        public const float RatingMinValue = 1;

        //Constant for the maximum rating value for a song.
        public const float RatingMaxValue = 5;

        // Constant for invalid rating message, with placeholders for minimum and maximum rating values.
        public const string InvalidRatingMessage = "Rating must be between {0} and {1}.";

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

        // Constant for catalog sorted by artist message.
        public const string CatalogSortedByArtist = "Catalog sorted by artist name:";

        // Constant for catalog sorted by title message.
        public const string CatalogSortedByTitle = "Catalog sorted by song title:";

        // Constant for catalog sorted by rating message.
        public const string CatalogSortedByRating = "Catalog sorted by song rating:";

        // Constant for merge successful message.
        public const string MergeSuccessfulMessage = "Friend catalog merged successfully.";

        public const string RatingChangedSuccessfullyMessage = "Rating for song \"{0}\" by \"{1}\" changed to {2} successfully.";

        // Constant for unknown command message.
        public const string UnknownCommandMessage = "Unknown command. Type 'help' for a list of available commands.";

        // Constant for invalid rating format message.
        public const string InvalidRatingFormatMessage = "Rating must be a valid number.";

        // Usage hint messages for commands that require arguments.
        public const string InvalidAddUsageMessage = "Usage: add|title|artist|rating";
        public const string InvalidRemoveUsageMessage = "Usage: remove|title|artist";
        public const string InvalidSearchUsageMessage = "Usage: search|keyword";
        public const string InvalidMergeExternalUsageMessage = "Usage: merge external|filePath";
        public const string InvalidChangeRatingUsageMessage = "Usage: change rating|title|artist|newRating";

    }
}
