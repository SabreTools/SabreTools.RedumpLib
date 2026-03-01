using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.CommandLine.Inputs;
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

        private const string _forceContinueName = "forcecontinue";
        internal readonly FlagInput ForceContinueInput = new(_forceContinueName, ["-c", "--continue"], "Force continuing downloads through errors");

        private const string _forceDownloadName = "forcedownload";
        internal readonly FlagInput ForceDownloadInput = new(_forceDownloadName, ["-f", "--force"], "Force downloading contents even if they already exist");

        private const string _listName = "list";
        internal readonly FlagInput ListInput = new(_listName, ["-l", "--list"], "Only list the page IDs for that user");

        private const string _onlyNewName = "onlynew";
        internal readonly FlagInput OnlyNewInput = new(_onlyNewName, ["-n", "--onlynew"], "Use the last modified view instead of sequential parsing");

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

            // Specific
            Add(OnlyNewInput);
            Add(ListInput);
            Add(ForceDownloadInput);
            Add(ForceContinueInput);
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

            // Get specific values
            bool onlyNew = OnlyNewInput.Value;
            bool onlyList = ListInput.Value;
            bool forceDownload = ForceDownloadInput.Value;
            bool forceContinue = ForceContinueInput.Value;

            // Output directory validation
            if (!onlyList && !ValidateAndCreateOutputDirectory(outDir))
                return false;

            // Update client properties
            _client.Debug = DebugInput.Value;
            if (attemptCount != null && attemptCount > 0)
                _client.AttemptCount = attemptCount.Value;
            if (timeout != null && timeout > 0)
                _client.Timeout = TimeSpan.FromSeconds(timeout.Value);

            // Login to Redump, if necessary
            _client.Login(username, password).Wait();

            // Start the processing
            Task<List<int>> processingTask;
            if (onlyList)
                processingTask = _client.ListUser(username);
            else if (onlyNew)
                processingTask = _client.DownloadUserLastModified(username, outDir, forceDownload, forceContinue);
            else
                processingTask = _client.DownloadUser(username, outDir, forceDownload, forceContinue);

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
