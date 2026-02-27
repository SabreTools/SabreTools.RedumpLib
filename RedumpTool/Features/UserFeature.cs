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

            // Specific
            Add(OnlyNewInput);
            Add(ListInput);
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
            // Get values needed more than once
            bool onlyList = ListInput.Value;
            string? outputDirectory = OutputInput.Value;

            // Output directory validation
            if (!onlyList && !ValidateAndCreateOutputDirectory(outputDirectory))
                return false;

            // Login to Redump, if necessary
            if (!_client.LoggedIn)
                _client.Login(UsernameInput.Value ?? string.Empty, PasswordInput.Value ?? string.Empty).Wait();

            // Start the processing
            Task<List<int>> processingTask;
            if (onlyList)
                processingTask = User.ListUser(_client, UsernameInput.Value);
            else if (OnlyNewInput.Value)
                processingTask = User.DownloadUserLastModified(_client, UsernameInput.Value, outputDirectory);
            else
                processingTask = User.DownloadUser(_client, UsernameInput.Value, outputDirectory);

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
