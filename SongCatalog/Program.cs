using SongCatalog.Models;
using SongCatalog.Repositories;
using SongCatalog.Repositories.Contracts;
using SongCatalog.Services;
using SongCatalog.Services.Contracts;

using static SongCatalog.Common.AppConstants;

namespace SongCatalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Create an instance of the CatalogRepository and CatalogService classes using Dependency Injection.
            ICatalogRepository catalogRepository = new CatalogRepository();
            ICatalogService catalogService = new CatalogService(catalogRepository);

            //Load the catalogs from the repository to ensure that they are populated with any existing songs before processing commands.
            List<Song> myCatalog = catalogRepository.LoadCatalog(JsonFilePath);
            List<Song> friendCatalog = catalogRepository.LoadCatalog(JsonFilePathFriendCatalog);

            //Read the command from the console and split it into an array of strings.
            args = Console.ReadLine()!.Split('|', StringSplitOptions.RemoveEmptyEntries).ToArray();

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
                        Console.WriteLine(catalogService.ListCatalog(myCatalog));
                        break;

                    case "add":
                        Console.WriteLine(catalogService.AddSong(args, myCatalog));
                        break;

                    case "remove":
                        Console.WriteLine(catalogService.RemoveSong(args[1], args[2], myCatalog));
                        break;

                    case "search":
                        Console.WriteLine(catalogService.SearchSongs(args[1], myCatalog));
                        break;

                    case "sort artist":
                        Console.WriteLine(catalogService.SortCatalogByArtist(myCatalog));
                        break;

                    case "sort title":
                        Console.WriteLine(catalogService.SortCatalogByTitle(myCatalog));
                        break;

                    case "sort rating":
                        Console.WriteLine(catalogService.SortCatalogByRating(myCatalog));
                        break;
                    //Merge the user's catalog with a hardcoded friend's catalog.
                    case "merge":
                        Console.WriteLine(catalogService.MergeFriendCatalog(myCatalog, friendCatalog));
                        break;
                    //Merge external catalog from a file path provided as the second argument.
                    case "merge external":
                        catalogService.MergeExternalCatalog(myCatalog, args[1]);
                        break;

                    case "change rating":
                        Console.WriteLine(catalogService.ChangeRating(args[1], args[2], float.Parse(args[3]), myCatalog));
                        break;
                }

                //Read the next command from the console.
                args = Console.ReadLine()!.Split('|', StringSplitOptions.RemoveEmptyEntries).ToArray();

                //If no command is provided, exit the program.
                if (args.Length == 0)
                {
                    Environment.Exit(0);
                }
            }
            
        }
    }
}
