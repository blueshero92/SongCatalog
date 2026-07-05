using Newtonsoft.Json;
using SongCatalog.Models;
using SongCatalog.Repositories.Contracts;

using static SongCatalog.Common.AppConstants;

namespace SongCatalog.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        public List<HistoryEntry> LoadHistory(string filePath)
        {
            // Check if the file exists before attempting to read it.
            if (!File.Exists(filePath))
            {
                Console.WriteLine(FileDoesNotExistMessage);
                return new List<HistoryEntry>();
            }

            // Read the JSON content from the file.
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON content into a list of HistoryEntry objects.
            return JsonConvert.DeserializeObject<List<HistoryEntry>>(json) ?? new List<HistoryEntry>();

        }

        public void SaveHistory(string filePathList, List<HistoryEntry> history)
        {
            // Serialize the history list to JSON with indentation for better readability.
            string json = JsonConvert.SerializeObject(history, Formatting.Indented);

            // Write the JSON content to the specified file.
            File.WriteAllText(filePathList, json);
        }
    }
}
