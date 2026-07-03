using SongCatalog.Repositories;
using SongCatalog.Repositories.Contracts;
using SongCatalog.Services;
using SongCatalog.Services.Contracts;

namespace SongCatalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICatalogRepository catalogRepository = new CatalogRepository();
            ICatalogService catalogService = new CatalogService(catalogRepository);

            catalogRepository.LoadCatalog();

            args = Console.ReadLine()!.Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray();

            //If no command is provided, exit the program.
            if (args.Length == 0)
            {
                Environment.Exit(0);
            }

            string command = args[0].ToLower();

            while(command != "exit")
            {
                switch (command)
                {
                    case "list":
                        catalogService.ListCatalog();
                        break;

                    case "add":
                        catalogService.AddSong(args);
                        break;

                    case "remove":
                        catalogService.RemoveSong(args[1], args[2]);
                        break;

                }

                args = Console.ReadLine()!.Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (args.Length == 0)
                {
                    Environment.Exit(0);
                }

                command = args[0].ToLower();
            }
            
        }
    }
}
