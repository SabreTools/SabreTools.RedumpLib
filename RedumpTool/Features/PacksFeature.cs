using System;
using SabreTools.CommandLine.Inputs;
using SabreTools.RedumpLib.Web;

namespace RedumpTool.Features
{
    internal sealed class PacksFeature : BaseFeature
    {
        #region Feature Definition

        public const string DisplayName = "packs";

        private static readonly string[] _flags = ["packs"];

        private const string _description = "Download available packs";

        #endregion

        #region Inputs

        private const string _subfoldersName = "subfolders";
        internal readonly FlagInput SubfoldersInput = new(_subfoldersName, ["-s", "--subfolders"], "Download packs to named subfolders");

        #endregion

        public PacksFeature()
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
            Add(SubfoldersInput);
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
            bool useSubfolders = SubfoldersInput.Value;

            // Output directory validation
            if (!ValidateAndCreateOutputDirectory(outDir))
                return false;

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
            var processingTask = _client.DownloadPacks(outDir, useSubfolders);

            // Retrieve the result
            processingTask.Wait();
            return processingTask.Result;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;
    }
}
