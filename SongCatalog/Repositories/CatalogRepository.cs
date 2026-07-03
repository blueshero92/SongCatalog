using Newtonsoft.Json;
using SongCatalog.Models;
using SongCatalog.Repositories.Contracts;

namespace SongCatalog.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private static string path = "../../../Files/songCatalog.json";

        public List<Song> Catalog { get; private set; } 
            = new List<Song>();

        public void LoadCatalog()
        {
            //If the file does not exist, create an empty catalog.
            if (!File.Exists(path))
            {
                Catalog = new List<Song>();
                return;
            }

            //If the file exists, read the contents.
            string json = File.ReadAllText(path);

            //Deserialize the JSON into a list of songs. If the file is empty or invalid, create an empty catalog.
            Catalog = JsonConvert.DeserializeObject<List<Song>>(json) ?? new List<Song>();
        }

        public void SaveCatalog()
        {
            //Serialize the catalog to JSON with indentation for readability.
            string json = JsonConvert.SerializeObject(Catalog, Formatting.Indented);

            //Write the JSON to the file.
            File.WriteAllText(path, json);
        }
    }
}
