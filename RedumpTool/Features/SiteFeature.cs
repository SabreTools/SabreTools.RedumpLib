using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.CommandLine.Inputs;
using SabreTools.RedumpLib.Web;

namespace RedumpTool.Features
{
    internal sealed class SiteFeature : BaseFeature
    {
        #region Feature Definition

        public const string DisplayName = "site";

        private static readonly string[] _flags = ["site"];

        private const string _description = "Download pages and related files from the main site";

        #endregion

        #region Inputs

        private const string _forceName = "force";
        internal readonly FlagInput ForceInput = new(_forceName, ["-f", "--force"], "Force continuing downloads until user cancels (requires only new)");

        private const string _maximumName = "maximum";
        internal readonly Int32Input MaximumInput = new(_maximumName, ["-max", "--maximum"], "Upper bound for page numbers (cannot be used with only new)");

        private const string _minimumName = "minimum";
        internal readonly Int32Input MinimumInput = new(_minimumName, ["-min", "--minimum"], "Lower bound for page numbers (cannot be used with only new)");

        private const string _onlyNewName = "onlynew";
        internal readonly FlagInput OnlyNewInput = new(_onlyNewName, ["-n", "--onlynew"], "Use the last modified view (cannot be used with min and max)");

        #endregion

        public SiteFeature()
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
            Add(MinimumInput);
            Add(MaximumInput);
            Add(OnlyNewInput);
            Add(ForceInput);
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
            int minId = MinimumInput.Value ?? -1;
            int maxId = MaximumInput.Value ?? -1;
            bool onlyNew = OnlyNewInput.Value;
            bool force = ForceInput.Value;

            // Output directory validation
            if (!ValidateAndCreateOutputDirectory(outDir))
                return false;

            // Range verification
            if (!onlyNew && (minId < 0 || maxId < 0))
            {
                Console.WriteLine("Please enter a valid range of Redump IDs");
                return false;
            }

            // Login to Redump, if necessary
            if (!_client.LoggedIn)
                _client.Login(username, password).Wait();

            // Update client properties
            _client.Debug = DebugInput.Value;
            if (attemptCount != null && attemptCount > 0)
                _client.AttemptCount = attemptCount.Value;
            if (timeout != null && timeout > 0)
                _client.Timeout = TimeSpan.FromSeconds(timeout.Value);

            // Start the processing
            Task<List<int>> processingTask;
            if (onlyNew)
                processingTask = _client.DownloadLastModified(outDir, force);
            else
                processingTask = _client.DownloadSiteRange(outDir, minId, maxId);

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
