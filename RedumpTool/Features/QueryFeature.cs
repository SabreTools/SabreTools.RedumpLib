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

        private const string _description = "Download pages and related files from a Redump-compatible quicksearch query";

        #endregion

        #region Inputs

        private const string _limitName = "limit";
        internal readonly Int32Input LimitInput = new(_limitName, ["--limit"], "Limit number of retrieved result pages");

        private const string _listName = "list";
        internal readonly FlagInput ListInput = new(_listName, ["-l", "--list"], "Only list the page IDs for that query");

        private const string _queryName = "query";
        internal readonly StringInput QueryInput = new(_queryName, ["-q", "--query"], "Redump-compatible query to run");

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
            Add(ForceDownloadInput);
            Add(ForceContinueInput);

            // Specific
            Add(QueryInput);
            Add(ListInput);
            Add(LimitInput);
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
            // Get common values
            string? outDir = OutputInput.Value;
            string? username = UsernameInput.Value;
            string? password = PasswordInput.Value;
            int? attemptCount = AttemptCountInput.Value;
            int? timeout = TimeoutInput.Value;
            bool forceDownload = ForceDownloadInput.Value;
            bool forceContinue = ForceContinueInput.Value;

            // Get specific values
            bool onlyList = ListInput.Value;
            string? query = QueryInput.Value;
            int limit = LimitInput.Value ?? -1;

            // Output directory validation
            if (!onlyList && !ValidateAndCreateOutputDirectory(outDir))
                return false;

            // Query verification (and cleanup)
            if (string.IsNullOrEmpty(query))
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
            _client.Overwrite = forceDownload;
            _client.IgnoreErrors = forceContinue;

            // Login to Redump, if necessary
            _client.Login(username, password).Wait();

            // Start the processing
            Task<List<int>> processingTask;
            if (onlyList)
                processingTask = _client.ListSearchResults(query, limit);
            else
                processingTask = _client.DownloadSearchResults(query, outDir, limit);

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
