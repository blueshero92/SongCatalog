using SongCatalog.Models;

namespace SongCatalog.Repositories.Contracts
{
    public interface IHistoryRepository
    {
        List<HistoryEntry> LoadHistory(string filePath);

        void SaveHistory(string filePathList, List<HistoryEntry> history);

    }
}
