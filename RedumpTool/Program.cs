using System;
using System.IO;
using SabreTools.RedumpLib;
using SabreTools.RedumpLib.Data;

namespace RedumpTool
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Show help if nothing is input
            if (args == null || args.Length == 0)
            {
                ShowHelp();
                return;
            }

            // Derive the feature, if possible
            Feature feature = DeriveFeature(args[0]);
            if (feature == Feature.NONE)
            {
                ShowHelp();
                return;
            }

            // Create a new Downloader
            var downloader = CreateDownloader(feature, args);
            if (downloader == null)
            {
                ShowHelp();
                return;
            }

            // Run the download task
            var downloaderTask = downloader.Download();
            downloaderTask.Wait();

            // Get the downloader task results and print, if necessary
            var downloaderResult = downloaderTask.Result;
            if (downloaderResult.Count > 0)
            {
                string processedIds = string.Join(", ", [.. downloaderResult.ConvertAll(i => i.ToString())]);
                Console.WriteLine($"Processed IDs: {processedIds}");
            }
            else if (downloaderResult.Count == 0 && downloader.Feature != Feature.Packs)
            {
                ShowHelp();
            }
        }

        /// <summary>
        /// Derive the feature from the supplied argument
        /// </summary>
        /// <param name="feature">Possible feature name to derive from</param>
        /// <returns>True if the feature was set, false otherwise</returns>
        private static Feature DeriveFeature(string feature)
        {
            return feature.ToLowerInvariant() switch
            {
                "site" => Feature.Site,
                "wip" => Feature.WIP,
                "packs" => Feature.Packs,
                "user" => Feature.User,
                "search" => Feature.Quicksearch,
                "query" => Feature.Quicksearch,
                _ => Feature.NONE,
            };
        }

        /// <summary>
        /// Create a Downloader from a feature and a set of arguments
        /// </summary>
        /// <param name="feature">Primary feature to use</param>
        /// <param name="args">Arguments list to parse</param>
        /// <returns>Initialized Downloader on success, null otherwise</returns>
        private static Downloader? CreateDownloader(Feature feature, string[] args)
        {
            // Set temporary internal variables
            string? outDir = null;
            string? username = null;
            string? password = null;
            int minimumId = -1;
            int maximumId = -1;
            string? queryString = null;
            bool useSubfolders = false;
            bool onlyNew = false;
            bool onlyList = false;
            bool noSlash = false;
            bool force = false;

            // Now loop through all of the arguments
            try
            {
                for (int i = 1; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        // Output directory
                        case "-o":
                        case "--output":
                            outDir = args[++i].Trim('"');
                            break;

                        // Username
                        case "-u":
                        case "--username":
                            username = args[++i];
                            break;

                        // Password
                        case "-p":
                        case "--password":
                            password = args[++i];
                            break;

                        // Minimum Redump ID
                        case "-min":
                        case "--minimum":
                            if (!int.TryParse(args[++i], out minimumId))
                                minimumId = -1;
                            break;

                        // Maximum Redump ID
                        case "-max":
                        case "--maximum":
                            if (!int.TryParse(args[++i], out maximumId))
                                maximumId = -1;
                            break;

                        // Quicksearch text
                        case "-q":
                        case "--query":
                            queryString = args[++i];
                            break;

                        // Packs subfolders
                        case "-s":
                        case "--subfolders":
                            useSubfolders = true;
                            break;

                        // Use last modified
                        case "-n":
                        case "--onlynew":
                            onlyNew = true;
                            break;

                        // List instead of download
                        case "-l":
                        case "--list":
                            onlyList = true;
                            break;

                        // Don't filter forward slashes from queries
                        case "-ns":
                        case "--noslash":
                            noSlash = true;
                            break;

                        // Force continuation
                        case "-f":
                        case "--force":
                            force = true;
                            break;

                        // Everything else
                        default:
                            Console.WriteLine($"Unrecognized flag: {args[i]}");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception has occurred: {ex}");
                return null;
            }

            // Output directory validation
            if (!onlyList && string.IsNullOrEmpty(outDir))
            {
                Console.WriteLine("No output directory set!");
                return null;
            }
            else if (!onlyList && !string.IsNullOrEmpty(outDir))
            {
                // Create the output directory, if it doesn't exist
                try
                {
                    if (!Directory.Exists(outDir))
                        Directory.CreateDirectory(outDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception has occurred: {ex}");
                    return null;
                }
            }

            // Range verification
            if (feature == Feature.Site && !onlyNew && (minimumId < 0 || maximumId < 0))
            {
                Console.WriteLine("Please enter a valid range of Redump IDs");
                return null;
            }
            else if (feature == Feature.WIP && !onlyNew && (minimumId < 0 || maximumId < 0))
            {
                Console.WriteLine("Please enter a valid range of WIP IDs");
                return null;
            }

            // Query verification (and cleanup)
            if (feature == Feature.Quicksearch && string.IsNullOrEmpty(queryString))
            {
                Console.WriteLine("Please enter a query for searching");
                return null;
            }

            // Create and return the downloader
            var downloader = new Downloader()
            {
                Feature = feature,
                MinimumId = minimumId,
                MaximumId = maximumId,
                QueryString = queryString,
                OutDir = outDir,
                UseSubfolders = useSubfolders,
                OnlyNew = onlyNew,
                OnlyList = onlyList,
                Force = force,
                NoSlash = noSlash,
                Username = username,
                Password = password,
            };
            return downloader;
        }

        /// <summary>
        /// Show the commandline help for the program
        /// </summary>
        private static void ShowHelp()
        {
            Console.WriteLine("RedumpTool - A Redump.org recovery tool");
            Console.WriteLine();
            Console.WriteLine("Usage: RedumpTool <feature> [options]");
            Console.WriteLine();
            Console.WriteLine("Common Options");
            Console.WriteLine("    -o <folder>, --output <folder> - Set the base output directory");
            Console.WriteLine("    -u <username>, --username <username> - Redump username");
            Console.WriteLine("    -p <pass>, --password <pass> - Redump password");
            Console.WriteLine();
            Console.WriteLine("site - Download pages and related files from the main site");
            Console.WriteLine("    -min <MinId>, --minimum <MinId> - Lower bound for page numbers (cannot be used with only new)");
            Console.WriteLine("    -max <MaxId>, --maximum <MaxId> - Upper bound for page numbers (cannot be used with only new)");
            Console.WriteLine("    -n, --onlynew - Use the last modified view (cannot be used with min and max)");
            Console.WriteLine("    -f, --force - Force continuing downloads until user cancels (used with only new)");
            Console.WriteLine();
            Console.WriteLine("wip - Download pages and related files from the WIP list");
            Console.WriteLine("    -min <MinId>, --minimum <MinId> - Lower bound for page numbers (cannot be used with only new)");
            Console.WriteLine("    -max <MaxId>, --maximum <MaxId> - Upper bound for page numbers (cannot be used with only new)");
            Console.WriteLine("    -n, --onlynew - Use the last modified view (cannot be used with min and max)");
            Console.WriteLine();
            Console.WriteLine("packs - Download available packs");
            Console.WriteLine("    -s, --subfolders - Download packs to named subfolders");
            Console.WriteLine();
            Console.WriteLine("user - Download pages and related files for a particular user");
            Console.WriteLine("    -n, --onlynew - Use the last modified view instead of sequential parsing");
            Console.WriteLine("    -l, --list - Only list the page IDs for that user");
            Console.WriteLine();
            Console.WriteLine("query - Download pages and related files from a Redump-compatible query");
            Console.WriteLine("    -q, --query - Redump-compatible query to run");
            Console.WriteLine("    -l, --list - Only list the page IDs for that query");
            Console.WriteLine("    -ns, --noslash - Don't replace forward slashes with '-'");
            Console.WriteLine();
        }
    }
}
