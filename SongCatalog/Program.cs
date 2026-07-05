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
            Console.WriteLine(OpeningMessage);

            //Create an instance of services and repositories to handle catalog operations and history tracking.
            IHistoryRepository historyRepository = new HistoryRepository();
            ICatalogRepository catalogRepository = new CatalogRepository();
            ICatalogService catalogService = new CatalogService(catalogRepository, historyRepository);

            //Load the catalogs from the repository to ensure that they are populated with any existing songs before processing commands.
            List<Song> myCatalog = catalogRepository.LoadCatalog(JsonFilePath);
            List<Song> friendCatalog = catalogRepository.LoadCatalog(JsonFilePathFriendCatalog);
            List<HistoryEntry> undoHistory = historyRepository.LoadHistory(UndoHistoryFilePath);
            List<HistoryEntry> redoHistory = historyRepository.LoadHistory(RedoHistoryFilePath);

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
                        if (args.Length < 4)
                        {
                            Console.WriteLine(InvalidAddUsageMessage);
                            break;
                        }
                        if (!float.TryParse(args[3], out float addRating))
                        {
                            Console.WriteLine(InvalidRatingFormatMessage);
                            break;
                        }
                        Console.WriteLine(catalogService.AddSong(args, myCatalog, undoHistory, redoHistory));
                        break;

                    case "remove":
                        if (args.Length < 3)
                        {
                            Console.WriteLine(InvalidRemoveUsageMessage);
                            break;
                        }
                        Console.WriteLine(catalogService.RemoveSong(args[1], args[2], myCatalog, undoHistory, redoHistory));
                        break;

                    case "search":
                        if (args.Length < 2)
                        {
                            Console.WriteLine(InvalidSearchUsageMessage);
                            break;
                        }
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

                    case "merge":
                        Console.WriteLine(catalogService.MergeFriendCatalog(myCatalog, friendCatalog, undoHistory, redoHistory));
                        break;

                    case "merge external":
                        if (args.Length < 2)
                        {
                            Console.WriteLine(InvalidMergeExternalUsageMessage);
                            break;
                        }
                        catalogService.MergeExternalCatalog(myCatalog, args[1], undoHistory, redoHistory);
                        break;

                    case "change rating":
                        if (args.Length < 4)
                        {
                            Console.WriteLine(InvalidChangeRatingUsageMessage);
                            break;
                        }
                        if (!float.TryParse(args[3], out float newRating))
                        {
                            Console.WriteLine(InvalidRatingFormatMessage);
                            break;
                        }
                        Console.WriteLine(catalogService.ChangeRating(args[1], args[2], newRating, myCatalog));
                        break;

                    case "undo":
                        catalogService.Undo(myCatalog, undoHistory, redoHistory);
                        break;

                    case "redo":
                        catalogService.Redo(myCatalog, undoHistory, redoHistory);
                        break;

                    case "help":
                        Console.WriteLine(HelpMessage);
                        break;

                    default:
                        Console.WriteLine(UnknownCommandMessage);
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
