
using SongCatalog.Models;

namespace SongCatalog.Services.Contracts
{
    public interface ICatalogService
    {
        string ListCatalog(List<Song> catalog);

        string AddSong(string[] tokens, List<Song> catalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory);

        string RemoveSong(string title, string artistName, List<Song> catalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory);

        string SearchSongs(string searchQuery, List<Song> catalog);

        string SortCatalogByArtist(List<Song> catalog);

        string SortCatalogByTitle(List<Song> catalog);

        string SortCatalogByRating(List<Song> catalog);

        string MergeFriendCatalog(List<Song> myCatalog, List<Song> friendCatalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory);

        void MergeExternalCatalog(List<Song> myCatalog, string filePath, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory);

        string ChangeRating(string title, string artistName, float newRating, List<Song> catalog);

        void Undo(List<Song> catalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory);

        void Redo(List<Song> catalog, List<HistoryEntry> undoHistory, List<HistoryEntry> redoHistory);
    }
}
