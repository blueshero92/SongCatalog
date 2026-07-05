namespace SongCatalog.Common
{
    public static class AppConstants
    {
        //Json file path for storing the song catalog. Path.Combine is used to create a platform-independent file path.
        //public static readonly string JsonFilePath = Path.Combine(Environment.CurrentDirectory, "Files", "song-catalog.json");

        public static readonly string JsonFilePath = "../../../Files/song-catalog.json";
        public static readonly string JsonFilePathFriendCatalog = "../../../Files/friend-catalog.json";
        public static readonly string UndoHistoryFilePath = "../../../Files/undo-history.json";
        public static readonly string RedoHistoryFilePath = "../../../Files/redo-history.json";

        //Json file path for storing the friend catalog. Path.Combine is used to create a platform-independent file path.
        //public static readonly string JsonFilePathFriendCatalog = Path.Combine(Environment.CurrentDirectory, "Files", "friend-catalog.json");

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

    }
}
