using SongCatalog.Models;

namespace SongCatalog.Repositories.Contracts
{
    public interface ICatalogRepository
    {
        List<Song> Catalog { get; }

        void LoadCatalog();

        void SaveCatalog();
    }
}
