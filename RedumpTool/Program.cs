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
                case QueryFeature qf: qf.ProcessArgs(args, 1); qf.Execute(); return;

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
            Console.WriteLine("    -o <folder>, --output <folder> - Set the base output directory");
            Console.WriteLine("    -u <username>, --username <username> - Redump username");
            Console.WriteLine("    -p <pass>, --password <pass> - Redump password");
            Console.WriteLine();
            Console.WriteLine("site - Download pages and related files from the main site");
            Console.WriteLine("    -min <MinId>, --minimum <MinId> - Lower bound for page numbers (cannot be used with only new)");
            Console.WriteLine("    -max <MaxId>, --maximum <MaxId> - Upper bound for page numbers (cannot be used with only new)");
            Console.WriteLine("    -n, --onlynew - Use the last modified view (cannot be used with min and max)");
            Console.WriteLine("    -f, --force - Force continuing downloads until user cancels (requires only new)");
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
