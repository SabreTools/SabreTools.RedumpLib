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
            Add(ForceDownloadInput);
            Add(ForceContinueInput);

            // Discs Path
            Add(AntiModchipInput);
            Add(BarcodeInput);
            Add(CategoryInput);
            Add(CommentsInput);
            Add(ContentsInput);
            Add(DiscTypeInput);
            Add(DumperInput);
            Add(EdcInput);
            Add(EditionInput);
            Add(ErrorsInput);
            Add(LanguageInput);
            Add(LetterInput);
            Add(LibCryptInput);
            Add(MediaInput);
            Add(OffsetInput);
            Add(ProtectionInput);
            Add(QuickSearchInput);
            Add(RegionInput);
            Add(RingcodeInput);
            Add(SortInput);
            Add(SortDirInput);
            Add(StatusInput);
            Add(SystemInput);
            Add(TracksInput);

            // Specific
            Add(OnlyNewInput);
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

            // Get discs path values
            bool? antiModchip = AntiModchipInput.Value;
            bool barcode = BarcodeInput.Value;
            string? categoryString = CategoryInput.Value;
            DiscCategory? category = categoryString.ToDiscCategory();
            bool comments = CommentsInput.Value;
            bool contents = ContentsInput.Value;
            string? discTypeString = DiscTypeInput.Value;
            DiscType? discType = discTypeString.ToDiscType();
            string? dumper = DumperInput.Value;
            bool? edc = EdcInput.Value;
            string? edition = EditionInput.Value;
            string? errors = ErrorsInput.Value;
            string? languageString = LanguageInput.Value;
            Language? language = languageString.ToLanguage();
            char? letter = string.IsNullOrEmpty(LetterInput.Value)
                ? null
                : LetterInput.Value![0];
            bool? libcrypt = LibCryptInput.Value;
            MediaType? media = MediaInput.Value?.ToLowerInvariant() switch
            {
                "cd" => MediaType.CDROM,
                "dvd" => MediaType.DVD,
                _ => null,
            };
            int? offset = OffsetInput.Value;
            bool protection = ProtectionInput.Value;
            string? quicksearch = QuickSearchInput.Value;
            string? regionString = RegionInput.Value;
            Region? region = regionString.ToRegion();
            string? ringcode = RingcodeInput.Value;
            string? sortString = SortInput.Value;
            SortCategory? sort = sortString.ToSortCategory();
            string? sortDirString = SortDirInput.Value;
            SortDirection? sortDir = sortDirString.ToSortDirection();
            string? statusString = StatusInput.Value;
            DumpStatus? status = statusString.ToDumpStatus();
            string? systemString = SystemInput.Value;
            RedumpSystem? system = systemString.ToRedumpSystem();
            int? tracks = TracksInput.Value;

            // Get specific values
            bool onlyNew = OnlyNewInput.Value;
            bool onlyList = ListInput.Value;
            int limit = LimitInput.Value ?? -1;

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
                processingTask = _client.ListUser(username, limit);
            else if (onlyNew)
                processingTask = _client.DownloadUserLastModified(username, outDir, limit);
            else
                processingTask = _client.DownloadUser(username, outDir, limit);

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
