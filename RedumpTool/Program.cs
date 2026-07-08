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
                case QueueFeature qf: qf.ProcessArgs(args, 1); qf.Execute(); return;
                case PacksFeature pf: pf.ProcessArgs(args, 1); pf.Execute(); return;
                case UserFeature uf: uf.ProcessArgs(args, 1); uf.Execute(); return;

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
            commandSet.Add(new QueueFeature());
            commandSet.Add(new PacksFeature());
            commandSet.Add(new UserFeature());

            return commandSet;
        }

        /// <summary>
        /// Show the commandline help for the program
        /// </summary>
        private static void ShowHelp()
        {
            Console.WriteLine("RedumpTool - A Redump.info reference tool");
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
            Console.WriteLine("    --limit <Limit> - Limit number of retrieved result pages");
            Console.WriteLine("    -l, --list - Only list the page IDs for the filters (incompatible with --minimum and --maximum)");
            Console.WriteLine("    -min <MinId>, --minimum <MinId> - Lower bound for page numbers (requires --maximum, incompatible with --list)");
            Console.WriteLine("    -max <MaxId>, --maximum <MaxId> - Upper bound for page numbers (requires --minimum, incompatible with --list)");
            Console.WriteLine();
            Console.WriteLine("    In addition to the above options, there are advanced options that allow users to finely tune their");
            Console.WriteLine("    site queries. All flags below are incompatible with --minimum and --maximum.");
            Console.WriteLine("        --barcode - Add barcode to filter");
            Console.WriteLine("        --barcode-exact - Add barcode exact matching to filter [true, false]");
            Console.WriteLine("        --category - Add category to filter");
            Console.WriteLine("        --comments - Add comments to filter");
            Console.WriteLine("        --contents - Add contents to filter");
            Console.WriteLine("        --dumper - Add dumper to filter");
            Console.WriteLine("        --edc - Add EDC status to filter [true, false]");
            Console.WriteLine("        --edition - Add edition to filter");
            Console.WriteLine("        --edition-exact - Add edition exact matching to filter [true, false]");
            Console.WriteLine("        --errors-max - Add maximum error count to filter");
            Console.WriteLine("        --errors-min - Add minimum error count to filter");
            Console.WriteLine("        --language - Add language to filter");
            Console.WriteLine("        --letter - Add title first letter to filter");
            Console.WriteLine("        --media - Add media type to filter");
            Console.WriteLine("        --offset - Add offset to filter");
            Console.WriteLine("        --order - Add sort order to filter [asc, desc]");
            Console.WriteLine("        --protection - Add protection to filter");
            Console.WriteLine("        --query - Add query to filter");
            Console.WriteLine("        --region - Add region to filter");
            Console.WriteLine("        --ringcode - Add ringcode to filter");
            Console.WriteLine("        --serial - Add serial to filter");
            Console.WriteLine("        --serial-exact - Add serial exact matching to filter [true, false]");
            Console.WriteLine("        --sort - Add sort category to filter [title, added, region, system, version, edition, language, serial, status, modified]");
            Console.WriteLine("        --status - Filter by status [grey, red, yellow, blue, green]");
            Console.WriteLine("        --system - Filter by system");
            Console.WriteLine("        --title - Add title to filter");
            Console.WriteLine("        --title-exact - Add title exact matching to filter [true, false]");
            Console.WriteLine("        --title-foreign - Add foreign title to filter");
            Console.WriteLine("        --title-foreign-exact - Add foreign title exact matching to filter [true, false]");
            Console.WriteLine("        --tracks-max - Add maximum track count to filter");
            Console.WriteLine("        --tracks-min - Add minimum track count to filter");
            Console.WriteLine();

            Console.WriteLine("queue - Download pages and related files from the submission queue");
            Console.WriteLine("    --limit <Limit> - Limit number of retrieved result pages");
            Console.WriteLine("    -l, --list - Only list the page IDs for the filters (incompatible with --minimum and --maximum)");
            Console.WriteLine("    -min <MinId>, --minimum <MinId> - Lower bound for page numbers (requires --maximum, incompatible with --list)");
            Console.WriteLine("    -max <MaxId>, --maximum <MaxId> - Upper bound for page numbers (requires --minimum, incompatible with --list)");
            Console.WriteLine();
            Console.WriteLine("    In addition to the above options, there are advanced options that allow users to finely tune their");
            Console.WriteLine("    queries. All flags below are incompatible with --minimum and --maximum.");
            Console.WriteLine("        --disc-id <Value> - Add disc ID to filter");
            Console.WriteLine("        --is-disc-history <Value> - Add disc history status to filter [true, false]");
            Console.WriteLine("        --order <Order> - Add sort order to filter [asc, desc]");
            Console.WriteLine("        --sort <Category> - Add sort category to filter [title, added, region, system, version, edition, language, serial, status, modified]");
            Console.WriteLine("        --status <Status> - Add status to filter [grey, red, yellow, blue, green]");
            Console.WriteLine("        --submitter <Value> - Add submitter to filter");
            Console.WriteLine("        --sub-type <SubType> - Add submission type to filter [edit, new disc, verification]");
            Console.WriteLine("        --system <System> - Add system to filter"); // TODO: Create list
            Console.WriteLine();

            Console.WriteLine("packs - Download available packs");
            Console.WriteLine("    -b, --database - Include database in downloads");
            Console.WriteLine("    -s, --subfolders - Download packs to named subfolders");
            Console.WriteLine();

            Console.WriteLine("user - Download pages and related files for a particular user");
            Console.WriteLine("    -n, --onlynew - Use the last modified view instead of sequential parsing");
            Console.WriteLine("    -l, --list - Only list the page IDs for that user");
            Console.WriteLine("    --limit <Limit> - Limit number of retrieved result pages");
            Console.WriteLine("    --only-pages - Only download disc subpages (incompatible with --only-files)");
            Console.WriteLine("    --only-files - Only download disc file attachments (incompatible with --only-pages)");
            Console.WriteLine();

            Console.WriteLine("If using an ID range, both minimum and maximum are required");
            Console.WriteLine();
        }
    }
}
