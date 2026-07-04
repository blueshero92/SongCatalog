using Newtonsoft.Json;
using SongCatalog.Models;
using SongCatalog.Repositories.Contracts;

using static SongCatalog.Common.AppConstants;

namespace SongCatalog.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        public List<Song> LoadCatalog(string filePath)
        {
            //If the file does not exist, create an empty catalog.
            if (!File.Exists(filePath))
            {
                Console.WriteLine(FileDoesNotExistMessage);
                return new List<Song>();
            }

            //If the file exists, read the contents.
            string json = File.ReadAllText(filePath);

            //Deserialize the JSON into a list of songs. If the file is empty or invalid, create an empty catalog.
            return JsonConvert.DeserializeObject<List<Song>>(json) ?? new List<Song>();
        }

        public void SaveCatalog(string filePath, List<Song> catalog)
        {
            //Serialize the catalog to JSON with indentation for readability.
            string json = JsonConvert.SerializeObject(catalog, Formatting.Indented);

            //Write the JSON to the file.
            File.WriteAllText(filePath, json);
        }
    }
}
