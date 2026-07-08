using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.CommandLine.Inputs;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace RedumpTool.Features
{
    // TODO: Add back every single URL parameter to here
    internal sealed class SiteFeature : BaseFeature
    {
        #region Feature Definition

        public const string DisplayName = "site";

        private static readonly string[] _flags = ["site"];

        private const string _description = "Download pages and related files from the main site";

        #endregion

        #region Inputs

        private const string _barcodeName = "barcode";
        internal readonly StringInput BarcodeInput = new(_barcodeName, ["--barcode"], "Add barcode to filter");

        private const string _barcodeExactName = "barcode-exact";
        internal readonly BooleanInput BarcodeExactInput = new(_barcodeExactName, ["--barcode-exact"], "Add barcode exact matching to filter [true, false]");

        private const string _categoryName = "category";
        internal readonly StringInput CategoryInput = new(_categoryName, ["--category"], "Add category to filter");

        private const string _commentsName = "comments";
        internal readonly StringInput CommentsInput = new(_commentsName, ["--comments"], "Add comments to filter");

        private const string _contentsName = "contents";
        internal readonly StringInput ContentsInput = new(_contentsName, ["--contents"], "Add contents to filter");

        private const string _dumperName = "dumper";
        internal readonly StringInput DumperInput = new(_dumperName, ["--dumper"], "Add dumper to filter");

        private const string _edcName = "edc";
        internal readonly BooleanInput EdcInput = new(_edcName, ["--edc"], "Add EDC status to filter [true, false]");

        private const string _editionName = "edition";
        internal readonly StringInput EditionInput = new(_editionName, ["--edition"], "Add edition to filter");

        private const string _editionExactName = "edition-exact";
        internal readonly BooleanInput EditionExactInput = new(_editionExactName, ["--edition-exact"], "Add edition exact matching to filter [true, false]");

        private const string _errorsMaxName = "errors-max";
        internal readonly Int32Input ErrorsMaximumInput = new(_errorsMaxName, ["--errors-max"], "Add maximum error count to filter");

        private const string _errorsMinName = "errors-min";
        internal readonly Int32Input ErrorsMinimumInput = new(_errorsMinName, ["--errors-min"], "Add minimum error count to filter");

        private const string _languageName = "language";
        internal readonly StringInput LanguageInput = new(_languageName, ["--language"], "Add language to filter");

        private const string _letterName = "letter";
        internal readonly StringInput LetterInput = new(_letterName, ["--letter"], "Add title first letter to filter");

        private const string _limitName = "limit";
        internal readonly Int32Input LimitInput = new(_limitName, ["--limit"], "Limit number of retrieved result pages");

        private const string _maximumName = "maximum";
        internal readonly Int32Input MaximumInput = new(_maximumName, ["-max", "--maximum"], "Upper bound for page numbers (incompatible with --onlynew)");

        private const string _mediaName = "media";
        internal readonly StringInput MediaInput = new(_mediaName, ["--media"], "Add media type to filter");

        private const string _minimumName = "minimum";
        internal readonly Int32Input MinimumInput = new(_minimumName, ["-min", "--minimum"], "Lower bound for page numbers (incompatible with --onlynew)");

        private const string _offsetName = "offset";
        internal readonly Int32Input OffsetInput = new(_offsetName, ["--offset"], "Add offset to filter");

        private const string _onlyNewName = "onlynew";
        internal readonly FlagInput OnlyNewInput = new(_onlyNewName, ["-n", "--onlynew"], "Use the last modified view (incompatible with min and max)");

        private const string _orderName = "order";
        internal readonly StringInput OrderInput = new(_orderName, ["--order"], "Add sort order to filter [asc, desc]");

        private const string _protectionName = "protection";
        internal readonly StringInput ProtectionInput = new(_protectionName, ["--protection"], "Add protection to filter");

        private const string _queryName = "query";
        internal readonly StringInput QueryInput = new(_queryName, ["--query"], "Add query to filter");

        private const string _regionName = "region";
        internal readonly StringInput RegionInput = new(_regionName, ["--region"], "Add region to filter");

        private const string _ringcodeName = "ringcode";
        internal readonly StringInput RingcodeInput = new(_ringcodeName, ["--ringcode"], "Add ringcode to filter");

        private const string _serialName = "serial";
        internal readonly StringInput SerialInput = new(_serialName, ["--serial"], "Add serial to filter");

        private const string _serialExactName = "serial-exact";
        internal readonly BooleanInput SerialExactInput = new(_serialExactName, ["--serial-exact"], "Add serial exact matching to filter [true, false]");

        private const string _sortName = "sort";
        internal readonly StringInput SortInput = new(_sortName, ["--sort"], "Add sort category to filter [title, added, region, system, version, edition, language, serial, status, modified]");

        private const string _statusName = "status";
        internal readonly StringInput StatusInput = new(_statusName, ["--status"], "Filter by status [grey, red, yellow, blue, green]");

        private const string _systemName = "system";
        internal readonly StringInput SystemInput = new(_systemName, ["--system"], "Filter by system");

        private const string _titleName = "title";
        internal readonly StringInput TitleInput = new(_titleName, ["--title"], "Add title to filter");

        private const string _titleExactName = "title-exact";
        internal readonly BooleanInput TitleExactInput = new(_titleExactName, ["--title-exact"], "Add title exact matching to filter [true, false]");

        private const string _titleForeignName = "title-foreign";
        internal readonly StringInput TitleForeignInput = new(_titleForeignName, ["--title-foreign"], "Add foreign title to filter");

        private const string _titleForeignExactName = "title-foreign-exact";
        internal readonly BooleanInput TitleForeignExactInput = new(_titleForeignExactName, ["--title-foreign-exact"], "Add foreign title exact matching to filter [true, false]");

        private const string _tracksMaxName = "tracks-max";
        internal readonly Int32Input TracksMaximumInput = new(_tracksMaxName, ["--tracks-max"], "Add maximum track count to filter");

        private const string _tracksMinName = "tracks-min";
        internal readonly Int32Input TracksMinimumInput = new(_tracksMinName, ["--tracks-min"], "Add minimum track count to filter");

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
            Add(ForceDownloadInput);
            Add(ForceContinueInput);

            // Specific
            Add(MinimumInput);
            Add(MaximumInput);
            Add(OnlyNewInput);
            Add(LimitInput);

            // Filter
            Add(BarcodeInput);
            Add(BarcodeExactInput);
            Add(CategoryInput);
            Add(CommentsInput);
            Add(ContentsInput);
            Add(DumperInput);
            Add(EdcInput);
            Add(EditionInput);
            Add(EditionExactInput);
            Add(ErrorsMaximumInput);
            Add(ErrorsMinimumInput);
            Add(LanguageInput);
            Add(LetterInput);
            Add(MediaInput);
            Add(OffsetInput);
            Add(OrderInput);
            Add(ProtectionInput);
            Add(QueryInput);
            Add(RegionInput);
            Add(RingcodeInput);
            Add(SerialInput);
            Add(SerialExactInput);
            Add(SortInput);
            Add(StatusInput);
            Add(SystemInput);
            Add(TitleInput);
            Add(TitleExactInput);
            Add(TitleForeignInput);
            Add(TitleForeignExactInput);
            Add(TracksMaximumInput);
            Add(TracksMinimumInput);
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
            int minId = MinimumInput.Value ?? -1;
            int maxId = MaximumInput.Value ?? -1;
            bool onlyNew = OnlyNewInput.Value;
            int limit = LimitInput.Value ?? -1;

            // Get filter values
            string? barcode = BarcodeInput.Value;
            bool? barcodeExact = BarcodeExactInput.Value;
            DiscCategory? category = CategoryInput.Value.ToDiscCategory();
            string? comments = CommentsInput.Value;
            string? contents = ContentsInput.Value;
            string? dumper = DumperInput.Value;
            YesNo? edc = EdcInput.Value?.ToYesNo();
            string? edition = EditionInput.Value;
            bool? editionExact = EditionExactInput.Value;
            long? errorsMax = ErrorsMaximumInput.Value;
            long? errorsMin = ErrorsMinimumInput.Value;
            Language? language = LanguageInput.Value.ToLanguage();
            char? letter = string.IsNullOrEmpty(LetterInput.Value)
                ? null
                : LetterInput.Value![0];
            MediaType? media = MediaInput.Value.ToMediaType();
            int? offset = OffsetInput.Value;
            SortDirection? order = OrderInput.Value.ToSortDirection();
            string? protection = ProtectionInput.Value;
            string? query = QueryInput.Value;
            Region? region = RegionInput.Value.ToRegion();
            string? ringcode = RingcodeInput.Value;
            string? serial = SerialInput.Value;
            bool? serialExact = SerialExactInput.Value;
            SortCategory? sort = SortInput.Value.ToSortCategory();
            DumpStatus? status = StatusInput.Value.ToDumpStatus();
            PhysicalSystem? system = SystemInput.Value.ToPhysicalSystem();
            string? title = TitleInput.Value;
            bool? titleExact = TitleExactInput.Value;
            string? titleForeign = TitleForeignInput.Value;
            bool? titleForeignExact = TitleForeignExactInput.Value;
            long? tracksMax = TracksMaximumInput.Value;
            long? tracksMin = TracksMinimumInput.Value;

            // Build the disc subpaths
            DiscSubpath[] discSubpaths = Constants.AllDiscSubpaths;

            // Override individual flags if shorthand flags used
            if (onlyNew)
            {
                sort = SortCategory.Modified;
                order = SortDirection.Descending;
            }

            // Output directory validation
            if (!ValidateAndCreateOutputDirectory(outDir))
                return false;

            // Update redump.info client properties
            _client.Debug = DebugInput.Value;
            if (attemptCount != null && attemptCount > 0)
                _client.AttemptCount = attemptCount.Value;
            if (timeout != null && timeout > 0)
                _client.Timeout = TimeSpan.FromSeconds(timeout.Value);
            _client.Overwrite = forceDownload;
            _client.IgnoreErrors = forceContinue;

            // Login to redump.info, if necessary
            _client.Login(username, password).Wait();

            // Start the processing
            Task<List<int>> processingTask;
            if (minId >= 0 && maxId >= 0)
            {
                processingTask = _client.DownloadSiteRange(outDir, minId, maxId, discSubpaths: discSubpaths);
            }
            else
            {
                processingTask = _client.DownloadDiscsResults(outDir,
                    advanced: true, // Hardcoded, only toggles the advanced options on page by default
                    barcode,
                    barcodeExact,
                    category,
                    comments,
                    contents,
                    dumper,
                    edc,
                    edition,
                    editionExact,
                    errorsMax,
                    errorsMin,
                    language,
                    letter,
                    media,
                    offset,
                    order,
                    protection,
                    query,
                    region,
                    ringcode,
                    serial,
                    serialExact,
                    sort,
                    status,
                    system,
                    title,
                    titleExact,
                    titleForeign,
                    titleForeignExact,
                    tracksMax,
                    tracksMin,
                    limit: limit,
                    discSubpaths: discSubpaths);
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
