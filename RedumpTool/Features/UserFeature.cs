using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.CommandLine.Inputs;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace RedumpTool.Features
{
    internal sealed class UserFeature : BaseFeature
    {
        #region Feature Definition

        public const string DisplayName = "user";

        private static readonly string[] _flags = ["user"];

        private const string _description = "Download pages and related files for a particular user";

        #endregion

        #region Inputs

        private const string _limitName = "limit";
        internal readonly Int32Input LimitInput = new(_limitName, ["--limit"], "Limit number of retrieved result pages");

        private const string _listName = "list";
        internal readonly FlagInput ListInput = new(_listName, ["-l", "--list"], "Only list the page IDs for that user");

        private const string _onlyFilesName = "onlypages";
        internal readonly FlagInput OnlyFilesInput = new(_onlyFilesName, ["--only-files"], "Only download disc file attachments (incompatible with --only-pages)");

        private const string _onlyNewName = "onlynew";
        internal readonly FlagInput OnlyNewInput = new(_onlyNewName, ["-n", "--onlynew"], "Use the last modified view instead of sequential parsing");

        private const string _onlyPagesName = "onlypages";
        internal readonly FlagInput OnlyPagesInput = new(_onlyPagesName, ["--only-pages"], "Only download disc subpages (incompatible with --only-files)");

        #endregion

        public UserFeature()
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
            Add(OnlyNewInput);
            Add(ListInput);
            Add(LimitInput);
            Add(OnlyPagesInput);
            Add(OnlyFilesInput);
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
            bool onlyNew = OnlyNewInput.Value;
            bool onlyList = ListInput.Value;
            int limit = LimitInput.Value ?? -1;
            bool onlyPages = OnlyPagesInput.Value;
            bool onlyFiles = OnlyFilesInput.Value;

            // Build the disc subpaths
            DiscSubpath[]? discSubpaths = Constants.AllDiscSubpaths;
            if (onlyPages)
                discSubpaths = Constants.DiscSubPagesOnly;
            else if (onlyFiles)
                discSubpaths = Constants.DiscFilesOnly;

            // Output directory validation
            if (!onlyList && !ValidateAndCreateOutputDirectory(outDir))
                return false;

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
                processingTask = _client.ListDiscsResults(dumper: username, limit: limit);
            else if (onlyNew)
                processingTask = _client.DownloadDiscsResults(outDir, dumper: username, sort: SortCategory.Modified, sortDir: SortDirection.Descending, limit: limit, discSubpaths: discSubpaths);
            else
                processingTask = _client.DownloadDiscsResults(outDir, dumper: username, limit: limit, discSubpaths: discSubpaths);

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
