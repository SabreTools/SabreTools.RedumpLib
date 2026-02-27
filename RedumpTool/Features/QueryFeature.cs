using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.CommandLine.Inputs;
using SabreTools.RedumpLib.Web;

namespace RedumpTool.Features
{
    internal sealed class QueryFeature : BaseFeature
    {
        #region Feature Definition

        public const string DisplayName = "query";

        private static readonly string[] _flags = ["query"];

        private const string _description = "Download pages and related files from a Redump-compatible query";

        #endregion

        #region Inputs

        private const string _listName = "list";
        internal readonly FlagInput ListInput = new(_listName, ["-l", "--list"], "Only list the page IDs for that query");

        private const string _noSlashName = "noslash";
        internal readonly FlagInput NoSlashInput = new(_noSlashName, ["-ns", "--noslash"], "Don't replace forward slashes with '-'");

        private const string _queryName = "query";
        internal readonly StringInput QueryInput = new(_queryName, ["-q", "--query"], "Redump-compatible query to run");

        private const string _quickSearchName = "quicksearch";
        internal readonly FlagInput QuickSearchInput = new(_quickSearchName, ["-s", "--quick"], "Indicate a query is for the 'quicksearch' path, not 'discs'");

        #endregion

        public QueryFeature()
           : base(DisplayName, _flags, _description)
        {
            RequiresInputs = false;

            // Common
            Add(DebugInput);
            Add(OutputInput);
            Add(UsernameInput);
            Add(PasswordInput);
            Add(AttemptCountInput);

            // Specific
            Add(QueryInput);
            Add(QuickSearchInput);
            Add(ListInput);
            Add(NoSlashInput);
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
            // Get values needed more than once
            bool onlyList = ListInput.Value;
            string? outputDirectory = OutputInput.Value;
            string? queryString = QueryInput.Value;
            int? attemptCount = AttemptCountInput.Value;
            bool quick = QuickSearchInput.Value;

            // Output directory validation
            if (!onlyList && !ValidateAndCreateOutputDirectory(outputDirectory))
                return false;

            // Query verification (and cleanup)
            if (string.IsNullOrEmpty(queryString))
            {
                Console.Error.WriteLine("Please enter a query for searching");
                return false;
            }

            // Login to Redump, if necessary
            if (!_client.LoggedIn)
                _client.Login(UsernameInput.Value ?? string.Empty, PasswordInput.Value ?? string.Empty).Wait();

            // Update client properties
            _client.Debug = DebugInput.Value;
            if (attemptCount != null && attemptCount > 0)
                _client.AttemptCount = attemptCount.Value;

            // Start the processing
            Task<List<int>> processingTask;
            if (quick)
            {
                if (onlyList)
                    processingTask = _client.ListSearchResults(queryString, NoSlashInput.Value);
                else
                    processingTask = _client.DownloadSearchResults(queryString, outputDirectory, NoSlashInput.Value);
            }
            else
            {
                if (onlyList)
                    processingTask = _client.ListDiscsResults(queryString, NoSlashInput.Value);
                else
                    processingTask = _client.DownloadDiscsResults(queryString, outputDirectory, NoSlashInput.Value);
            }

            // Retrieve the result
            processingTask.Wait();
            var processedIds = processingTask.Result;

            // Display the processed IDs
            return PrintProcessedIds(processedIds);
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;
    }
}
