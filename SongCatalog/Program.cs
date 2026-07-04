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

            //Load the catalog from the repository to ensure that the catalog is populated with any existing songs before processing commands.
            catalogRepository.LoadCatalog();

            //Read the command from the console and split it into an array of strings.
            args = Console.ReadLine()!.Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray();

            //If no command is provided, exit the program.
            if (args.Length == 0)
            {
                Environment.Exit(0);
            }

            //Set the command to the first argument in the array.
            string command = args[0].ToLower();

            //Process commands until the user enters "exit".
            while (command != "exit")
            {
                switch (command)
                {
                    case "list":
                        Console.WriteLine(catalogService.ListCatalog());
                        break;

                    case "add":
                        Console.WriteLine(catalogService.AddSong(args));
                        break;

                    case "remove":
                        Console.WriteLine(catalogService.RemoveSong(args[1], args[2]));
                        break;

                    case "search":
                        Console.WriteLine(catalogService.SearchSongs(args[1]));
                        break;

                }

                //Read the next command from the console.
                args = Console.ReadLine()!.Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray();

                //If no command is provided, exit the program.
                if (args.Length == 0)
                {
                    Environment.Exit(0);
                }

                //Set the command to the first argument in the array.
                command = args[0].ToLower();
            }
            
        }
    }
}
