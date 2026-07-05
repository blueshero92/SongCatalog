
using SongCatalog.Models;

namespace SongCatalog.Services.Contracts
{
    public interface ICatalogService
    {
        string ListCatalog(List<Song> catalog);

        string AddSong(string[] tokens, List<Song> catalog);

        string RemoveSong(string title, string artistName, List<Song> catalog);

        string SearchSongs(string searchQuery, List<Song> catalog);

        string SortCatalogByArtist(List<Song> catalog);

        string SortCatalogByTitle(List<Song> catalog);

        string SortCatalogByRating(List<Song> catalog);

        string MergeFriendCatalog(List<Song> myCatalog, List<Song> friendCatalog);

        void MergeExternalCatalog(List<Song> myCatalog, string filePath);

        string ChangeRating(string title, string artistName, float newRating, List<Song> catalog);
    }
}
