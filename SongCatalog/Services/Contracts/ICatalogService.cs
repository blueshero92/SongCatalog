
namespace SongCatalog.Services.Contracts
{
    public interface ICatalogService
    {
        void ListCatalog();
        void AddSong(string[] tokens);
    }
}
