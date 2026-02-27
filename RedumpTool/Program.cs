using System;
using System.IO;

namespace RedumpTool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Show help if nothing is input
            if (args.Length == 0)
            {
                Console.WriteLine("At least one argument is required");
                ShowHelp();
                return;
            }

            // Derive the feature, if possible
            Feature feature = DeriveFeature(args[0]);
            if (feature == Feature.NONE)
            {
                Console.WriteLine("The feature could not be derived");
                ShowHelp();
                return;
            }

            // Create a new Downloader
            var downloader = CreateDownloader(feature, args);
            if (downloader is null)
            {
                Console.WriteLine("A downloader could not be created from the inputs");
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
                Console.WriteLine("No results were found");
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
            var downloader = new Downloader()
            {
                Feature = feature,
                MinimumId = -1,
                MaximumId = -1,
            };

            // Loop through all of the arguments
            try
            {
                for (int i = 1; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        // Output directory
                        case "-o":
                        case "--output":
                            downloader.OutDir = args[++i].Trim('"');
                            break;

                        // Username
                        case "-u":
                        case "--username":
                            downloader.Username = args[++i];
                            break;

                        // Password
                        case "-p":
                        case "--password":
                            downloader.Password = args[++i];
                            break;

                        // Minimum Redump ID
                        case "-min":
                        case "--minimum":
                            if (!int.TryParse(args[++i], out int minimumId))
                                minimumId = -1;

                            downloader.MinimumId = minimumId;
                            break;

                        // Maximum Redump ID
                        case "-max":
                        case "--maximum":
                            if (!int.TryParse(args[++i], out int maximumId))
                                maximumId = -1;

                            downloader.MaximumId = maximumId;
                            break;

                        // Quicksearch text
                        case "-q":
                        case "--query":
                            downloader.QueryString = args[++i];
                            break;

                        // Packs subfolders
                        case "-s":
                        case "--subfolders":
                            downloader.UseSubfolders = true;
                            break;

                        // Use last modified
                        case "-n":
                        case "--onlynew":
                            downloader.OnlyNew = true;
                            break;

                        // List instead of download
                        case "-l":
                        case "--list":
                            downloader.OnlyList = true;
                            break;

                        // Don't filter forward slashes from queries
                        case "-ns":
                        case "--noslash":
                            downloader.NoSlash = true;
                            break;

                        // Force continuation
                        case "-f":
                        case "--force":
                            downloader.Force = true;
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
            if (!downloader.OnlyList && string.IsNullOrEmpty(downloader.OutDir))
            {
                Console.WriteLine("No output directory set!");
                return null;
            }
            else if (!downloader.OnlyList && !string.IsNullOrEmpty(downloader.OutDir))
            {
                // Create the output directory, if it doesn't exist
                try
                {
                    if (!Directory.Exists(downloader.OutDir))
                        Directory.CreateDirectory(downloader.OutDir!);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception has occurred: {ex}");
                    return null;
                }
            }

            // Range verification
            if (feature == Feature.Site && !downloader.OnlyNew && (downloader.MinimumId < 0 || downloader.MaximumId < 0))
            {
                Console.WriteLine("Please enter a valid range of Redump IDs");
                return null;
            }
            else if (feature == Feature.WIP && !downloader.OnlyNew && (downloader.MinimumId < 0 || downloader.MaximumId < 0))
            {
                Console.WriteLine("Please enter a valid range of WIP IDs");
                return null;
            }

            // Query verification (and cleanup)
            if (feature == Feature.Quicksearch && string.IsNullOrEmpty(downloader.QueryString))
            {
                Console.WriteLine("Please enter a query for searching");
                return null;
            }

            // Return the downloader
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
            Console.WriteLine("If using an ID range, both minimum and maximum are required");
            Console.WriteLine();
        }
    }
}
