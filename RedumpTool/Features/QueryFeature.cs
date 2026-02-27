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
            Add(TimeoutInput);

            // Specific
            Add(QueryInput);
            Add(QuickSearchInput);
            Add(ListInput);
            Add(NoSlashInput);
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
            // Get common values
            string? outDir = OutputInput.Value;
            string username = UsernameInput.Value ?? string.Empty;
            string password = PasswordInput.Value ?? string.Empty;
            int? attemptCount = AttemptCountInput.Value;
            int? timeout = TimeoutInput.Value;

            // Get specific values
            bool onlyList = ListInput.Value;
            string? queryString = QueryInput.Value;
            bool quick = QuickSearchInput.Value;
            bool convertForwardSlashes = !NoSlashInput.Value;

            // Output directory validation
            if (!onlyList && !ValidateAndCreateOutputDirectory(outDir))
                return false;

            // Query verification (and cleanup)
            if (string.IsNullOrEmpty(queryString))
            {
                Console.Error.WriteLine("Please enter a query for searching");
                return false;
            }

            // Update client properties
            _client.Debug = DebugInput.Value;
            if (attemptCount != null && attemptCount > 0)
                _client.AttemptCount = attemptCount.Value;
            if (timeout != null && timeout > 0)
                _client.Timeout = TimeSpan.FromSeconds(timeout.Value);

            // Login to Redump, if necessary
            if (!_client.LoggedIn)
                _client.Login(username, password).Wait();

            // Start the processing
            Task<List<int>> processingTask;
            if (quick)
            {
                if (onlyList)
                    processingTask = _client.ListSearchResults(queryString, convertForwardSlashes);
                else
                    processingTask = _client.DownloadSearchResults(queryString, outDir, convertForwardSlashes);
            }
            else
            {
                if (onlyList)
                    processingTask = _client.ListDiscsResults(queryString, convertForwardSlashes);
                else
                    processingTask = _client.DownloadDiscsResults(queryString, outDir, convertForwardSlashes);
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
