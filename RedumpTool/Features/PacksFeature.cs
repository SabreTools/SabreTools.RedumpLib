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

            // Specific
            Add(SubfoldersInput);
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
            // Get values needed more than once
            string? outputDirectory = OutputInput.Value;

            // Output directory validation
            if (!ValidateAndCreateOutputDirectory(outputDirectory))
                return false;

            // Login to Redump, if necessary
            if (!_client.LoggedIn)
                _client.Login(UsernameInput.Value ?? string.Empty, PasswordInput.Value ?? string.Empty).Wait();

            // Start the processing
            var processingTask = Packs.DownloadPacks(_client, outputDirectory, SubfoldersInput.Value);

            // Retrieve the result
            processingTask.Wait();
            return processingTask.Result;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;
    }
}
