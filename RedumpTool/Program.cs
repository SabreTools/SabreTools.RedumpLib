using System;
using RedumpTool.Features;
using SabreTools.CommandLine;
using SabreTools.CommandLine.Features;

namespace RedumpTool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create the command set
            var commandSet = CreateCommands();

            // Show help if nothing is input
            if (args.Length == 0)
            {
                Console.WriteLine("At least one argument is required");
                ShowHelp();
                return;
            }

            // Cache the first argument and starting index
            string featureName = args[0];

            // Try processing the standalone arguments
            var topLevel = commandSet.GetTopLevel(featureName);
            switch (topLevel)
            {
                case Help help: help.ProcessArgs(args, 0, commandSet); return;
                case SiteFeature sf: sf.ProcessArgs(args, 1); sf.Execute(); return;
                case WIPFeature wf: wf.ProcessArgs(args, 1); wf.Execute(); return;
                case PacksFeature pf: pf.ProcessArgs(args, 1); pf.Execute(); return;
                case UserFeature uf: uf.ProcessArgs(args, 1); uf.Execute(); return;
                case QueryFeature qf: qf.ProcessArgs(args, 1); qf.Execute(); return;
                case BuildUrlFeature buf: buf.ProcessArgs(args, 1); buf.Execute(); return;

                // This is entirely broken on the site side
                //case StatsFeature sf: sf.ProcessArgs(args, 1); sf.Execute(); return;

                default:
                    Console.WriteLine($"{featureName} is not a known feature");
                    ShowHelp();
                    return;
            }
        }

        /// <summary>
        /// Create the command set for the program
        /// </summary>
        private static CommandSet CreateCommands()
        {
            var commandSet = new CommandSet();

            commandSet.Add(new Help(["-?", "-h", "--help"]));
            commandSet.Add(new SiteFeature());
            commandSet.Add(new WIPFeature());
            commandSet.Add(new PacksFeature());
            commandSet.Add(new UserFeature());
            commandSet.Add(new QueryFeature());
            commandSet.Add(new BuildUrlFeature());
            commandSet.Add(new StatsFeature());

            return commandSet;
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
            Console.WriteLine("    -d, --debug - Enable debug mode");
            Console.WriteLine("    -o <folder>, --output <folder> - Set the base output directory");
            Console.WriteLine("    -u <username>, --username <username> - Redump username");
            Console.WriteLine("    -p <pass>, --password <pass> - Redump password");
            Console.WriteLine("    -a <attempts>, --attempts <attempts> - Number of attempts for web requests (default 3)");
            Console.WriteLine("    -t <seconds>, --timeout <seconds> - Request timeout in whole seconds (default 30)");
            Console.WriteLine("    -f, --force - Force downloading contents even if they already exist");
            Console.WriteLine("    -c, --continue - Force continuing downloads through errors");
            Console.WriteLine();

            Console.WriteLine("site - Download pages and related files from the main site");
            Console.WriteLine("    -min <MinId>, --minimum <MinId> - Lower bound for page numbers (incompatible with --onlynew)");
            Console.WriteLine("    -max <MaxId>, --maximum <MaxId> - Upper bound for page numbers (incompatible with --onlynew)");
            Console.WriteLine("    -n, --onlynew - Use the last modified view (incompatible with min and max)");
            Console.WriteLine("        Internally this sets '--sort modified -sort-dir desc'");
            Console.WriteLine("    --limit <Limit> - Limit number of retrieved result pages");
            Console.WriteLine("    --only-pages - Only download disc subpages (incompatible with --only-files)");
            Console.WriteLine("    --only-files - Only download disc file attachments (incompatible with --only-pages)");
            Console.WriteLine();
            Console.WriteLine("    In addition to the above options, there are advanced options that allow users to finely tune their");
            Console.WriteLine("    site queries. All flags below are incompatible with --minimum, --maximum, and --onlynew.");
            Console.WriteLine("        --anti-modchip <Value> - Filter by anti-modchip status [true, false, null]");
            Console.WriteLine("        --barcode - Filter by missing barcodes");
            Console.WriteLine("        --category <Category> - Filter by disc category"); // TODO: Create list
            Console.WriteLine("        --comments - Filter by comments only, incompatible with --contents and --protection");
            Console.WriteLine("        --contents - Filter by contents only, incompatible with --comments and --protection");
            Console.WriteLine("        --disc-type <DiscType> - Filter by disc type, requires --system [cd, dvd]");
            Console.WriteLine("        --dumper <Dumper> - Filter by dumper");
            Console.WriteLine("        --edc <Value> - Filter by EDC status [true, false, null]");
            Console.WriteLine("        --edition <Edition> - Filter by edition");
            Console.WriteLine("        --errors <Errors> - Filter by errors");
            Console.WriteLine("        --language <Language> - Filter by language"); // TODO: Create list
            Console.WriteLine("        --letter <First> - Filter by first letter");
            Console.WriteLine("        --libcrypt <Value> - Filter by LibCrypt status [true, false, null]");
            Console.WriteLine("        --media <MediaType> - Filter by media type"); // TODO: Create list
            Console.WriteLine("        --offset <Offset> - Filter by disc offset");
            Console.WriteLine("        --page <Page> - Retrieve specific result page");
            Console.WriteLine("        --protection - Filter by protection only, incompatible with --comments and --contents");
            Console.WriteLine("        --quicksearch <Query> - Filter by quicksearch");
            Console.WriteLine("        --region <Region> - Filter by region"); // TODO: Create list
            Console.WriteLine("        --ringcode <Ringcode> - Filter by ringcode");
            Console.WriteLine("        --sort <Criteria> - Sort results by criteria [added, region, system, version, edition, languages, serial, status, modified]");
            Console.WriteLine("        --sort-dir <Direction> - Set sorting direction [asc, desc]");
            Console.WriteLine("        --status <Status> - Filter by status [grey, red, yellow, blue, green]");
            Console.WriteLine("        --system <System> - Filter by system"); // TODO: Create list
            Console.WriteLine("        --tracks <Count> - Filter by track count [1-99]");
            Console.WriteLine();

            Console.WriteLine("wip - Download pages and related files from the WIP list");
            Console.WriteLine("    -min <MinId>, --minimum <MinId> - Lower bound for page numbers (incompatible with --onlynew)");
            Console.WriteLine("    -max <MaxId>, --maximum <MaxId> - Upper bound for page numbers (incompatible with --onlynew)");
            Console.WriteLine("    -n, --onlynew - Use the last modified view (incompatible with min and max)");
            Console.WriteLine();

            Console.WriteLine("packs - Download available packs");
            Console.WriteLine("    -s, --subfolders - Download packs to named subfolders");
            Console.WriteLine();

            Console.WriteLine("user - Download pages and related files for a particular user");
            Console.WriteLine("    -n, --onlynew - Use the last modified view instead of sequential parsing");
            Console.WriteLine("    -l, --list - Only list the page IDs for that user");
            Console.WriteLine("    --limit <Limit> - Limit number of retrieved result pages");
            Console.WriteLine("    --only-pages - Only download disc subpages (incompatible with --only-files)");
            Console.WriteLine("    --only-files - Only download disc file attachments (incompatible with --only-pages)");
            Console.WriteLine();

            Console.WriteLine("query - Download pages and related files from a Redump-compatible quicksearch query");
            Console.WriteLine("    -q, --query - Redump-compatible query to run");
            Console.WriteLine("    -l, --list - Only list the page IDs for that query");
            Console.WriteLine("    --limit <Limit> - Limit number of retrieved result pages");
            Console.WriteLine("    --only-pages - Only download disc subpages (incompatible with --only-files)");
            Console.WriteLine("    --only-files - Only download disc file attachments (incompatible with --only-pages)");
            Console.WriteLine();

            Console.WriteLine("build-url - Build a Redump URL");
            Console.WriteLine("    -b, --base-path <BasePath> - Indicate base path for building URL, see below for options");
            Console.WriteLine();
            Console.WriteLine("    disc - Individual disc pages");
            Console.WriteLine("        -i, --disc-id <ID> - Disc ID (required)");
            Console.WriteLine("        -s, --subpath <Subpath> - Disc page subpath [changes, cue, edit, gdi, key, lsd, md5, sbi, sfv, sha1]");
            Console.WriteLine();
            Console.WriteLine("    discs - Individual disc pages");
            Console.WriteLine("        --anti-modchip <Value> - Filter by anti-modchip status [true, false, null]");
            Console.WriteLine("        --barcode - Filter by missing barcodes");
            Console.WriteLine("        --category <Category> - Filter by disc category"); // TODO: Create list
            Console.WriteLine("        --comments - Filter by comments only, incompatible with --contents and --protection");
            Console.WriteLine("        --contents - Filter by contents only, incompatible with --comments and --protection");
            Console.WriteLine("        --disc-type <DiscType> - Filter by disc type, requires --system [cd, dvd]");
            Console.WriteLine("        --dumper <Dumper> - Filter by dumper");
            Console.WriteLine("        --edc <Value> - Filter by EDC status [true, false, null]");
            Console.WriteLine("        --edition <Edition> - Filter by edition");
            Console.WriteLine("        --errors <Errors> - Filter by errors");
            Console.WriteLine("        --language <Language> - Filter by language"); // TODO: Create list
            Console.WriteLine("        --letter <First> - Filter by first letter");
            Console.WriteLine("        --libcrypt <Value> - Filter by LibCrypt status [true, false, null]");
            Console.WriteLine("        --media <MediaType> - Filter by media type"); // TODO: Create list
            Console.WriteLine("        --offset <Offset> - Filter by disc offset");
            Console.WriteLine("        --page <Page> - Retrieve specific result page");
            Console.WriteLine("        --protection - Filter by protection only, incompatible with --comments and --contents");
            Console.WriteLine("        --quicksearch <Query> - Filter by quicksearch");
            Console.WriteLine("        --region <Region> - Filter by region"); // TODO: Create list
            Console.WriteLine("        --ringcode <Ringcode> - Filter by ringcode");
            Console.WriteLine("        --sort <Criteria> - Sort results by criteria [added, region, system, version, edition, languages, serial, status, modified]");
            Console.WriteLine("        --sort-dir <Direction> - Set sorting direction [asc, desc]");
            Console.WriteLine("        --status <Status> - Filter by status [grey, red, yellow, blue, green]");
            Console.WriteLine("        --system <System> - Filter by system"); // TODO: Create list
            Console.WriteLine("        --tracks <Count> - Filter by track count [1-99]");
            Console.WriteLine();
            Console.WriteLine("    discs-wip - WIP discs queue");
            Console.WriteLine();
            Console.WriteLine("    downloads - Downloads landing page");
            Console.WriteLine();
            Console.WriteLine("    list - Plaintext have/miss lists");
            Console.WriteLine("        -g, --have <Value> - Have [true] or miss [false] filter");
            Console.WriteLine("        --dumper <Dumper> - Dumper name, requires logged-in user (required)");
            Console.WriteLine("        --system <System> - System to filter list by, otherwise returns for all systems combined"); // TODO: Create list
            Console.WriteLine();
            Console.WriteLine("    newdisc - Individual WIP disc pages");
            Console.WriteLine("        -w, --newdisc-id <ID> - WIP Disc ID (required)");
            Console.WriteLine();
            Console.WriteLine("    pack - Download packs");
            Console.WriteLine("        -k, --pack <Pack> - Download pack ID (required) [cues, datfile, dkeys, gdi, keys, lsd, sbi]");
            Console.WriteLine("        --system <System> - System to download pack for (required)"); // TODO: Create list
            Console.WriteLine();
            Console.WriteLine("    statistics - Redump statistics page");
            Console.WriteLine();

            Console.WriteLine("If using an ID range, both minimum and maximum are required");
            Console.WriteLine();
        }
    }
}
