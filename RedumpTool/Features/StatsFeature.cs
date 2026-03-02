using System;
using System.Threading.Tasks;

namespace RedumpTool.Features
{
    internal sealed class StatsFeature : BaseFeature
    {
        #region Feature Definition

        public const string DisplayName = "stats";

        private static readonly string[] _flags = ["stats"];

        private const string _description = "Download the statistics page";

        #endregion

        public StatsFeature()
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

            // Output directory validation
            if (!ValidateAndCreateOutputDirectory(outDir))
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
            Task<bool> processingTask = _client.DownloadStatisticsPage(outDir);

            // Retrieve the result
            processingTask.Wait();
            return processingTask.Result;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;
    }
}
