using SongCatalog.Models;

namespace SongCatalog.Repositories.Contracts
{
    public interface ICatalogRepository
    {
        List<Song> LoadCatalog(string filePath);

        void SaveCatalog(string filePath, List<Song> catalog);
    }
}
