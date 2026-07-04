
namespace SongCatalog.Services.Contracts
{
    public interface ICatalogService
    {
        string ListCatalog();

        string AddSong(string[] tokens);

        string RemoveSong(string title, string artistName);

        string SearchSongs(string searchQuery);

        string SortCatalogByArtist();

        string SortCatalogByTitle();

        string SortCatalogByRating();
    }
}
