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
            //Create an instance of the CatalogRepository and CatalogService classes using Dependency Injection.
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

            string command;

            //Set the command to the first argument in the array and process commands until the user enters "exit".
            while ((command = args[0].ToLower()) != "exit")
            {
                //Process the command using a switch case and call the appropriate method from the CatalogService class.
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

                    case "sort artist":
                        Console.WriteLine(catalogService.SortCatalogByArtist());
                        break;

                    case "sort title":
                        Console.WriteLine(catalogService.SortCatalogByTitle());
                        break;

                    case "sort rating":
                        Console.WriteLine(catalogService.SortCatalogByRating());
                        break;
                }

                //Read the next command from the console.
                args = Console.ReadLine()!.Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray();

                //If no command is provided, exit the program.
                if (args.Length == 0)
                {
                    Environment.Exit(0);
                }
            }
            
        }
    }
}
