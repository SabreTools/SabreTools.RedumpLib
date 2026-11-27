using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SabreTools.CommandLine.Inputs;
using SabreTools.RedumpLib.Data;
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

        private const string _additionalMouldName = "additional-mould";
        internal readonly StringInput AdditionalMouldInput = new(_additionalMouldName, ["--additional-mould"], "Add additional mould to filter");

        private const string _barcodeName = "barcode";
        internal readonly StringInput BarcodeInput = new(_barcodeName, ["--barcode"], "Add barcode to filter");

        private const string _barcodeModeName = "barcode-mode";
        internal readonly StringInput BarcodeModeInput = new(_barcodeModeName, ["--barcode-mode"], "Add barcode matching mode to filter [any, all]");

        private const string _categoryName = "category";
        internal readonly StringInput CategoryInput = new(_categoryName, ["--category"], "Add category to filter");

        private const string _commentsName = "comments";
        internal readonly StringInput CommentsInput = new(_commentsName, ["--comments"], "Add comments to filter");

        private const string _contentsName = "contents";
        internal readonly StringInput ContentsInput = new(_contentsName, ["--contents"], "Add contents to filter");

        private const string _discNumberName = "disc-number";
        internal readonly StringInput DiscNumberInput = new(_discNumberName, ["--disc-number"], "Add disc number to filter");

        private const string _discTitleName = "disc-title";
        internal readonly StringInput DiscTitleInput = new(_discTitleName, ["--disc-title"], "Add disc title to filter");

        private const string _discTitleModeName = "disc-title-mode";
        internal readonly StringInput DiscTitleModeInput = new(_discTitleModeName, ["--disc-title-mode"], "Add disc title matching mode to filter [any, all]");

        private const string _discTitleForeignName = "disc-title-foreign";
        internal readonly StringInput DiscTitleForeignInput = new(_discTitleForeignName, ["--disc-title-foreign"], "Add foreign disc title to filter");

        private const string _discTitleForeignModeName = "disc-title-foreign-mode";
        internal readonly StringInput DiscTitleForeignModeInput = new(_discTitleForeignModeName, ["--disc-title-foreign-mode"], "Add foreign disc title matching mode to filter [any, all]");

        private const string _dumperName = "dumper";
        internal readonly StringInput DumperInput = new(_dumperName, ["--dumper"], "Add dumper to filter");

        private const string _edcName = "edc";
        internal readonly BooleanInput EdcInput = new(_edcName, ["--edc"], "Add EDC status to filter [true, false]");

        private const string _editionName = "edition";
        internal readonly StringInput EditionInput = new(_editionName, ["--edition"], "Add edition to filter");

        private const string _editionModeName = "edition-mode";
        internal readonly StringInput EditionModeInput = new(_editionModeName, ["--edition-mode"], "Add edition matching mode to filter [any, all]");

        private const string _errorsMaxName = "errors-max";
        internal readonly Int32Input ErrorsMaxInput = new(_errorsMaxName, ["--errors-max"], "Add maximum error count to filter");

        private const string _errorsMinName = "errors-min";
        internal readonly Int32Input ErrorsMinInput = new(_errorsMinName, ["--errors-min"], "Add minimum error count to filter");

        private const string _exeDateMaxName = "exe-date-max";
        internal readonly StringInput ExeDateMaxInput = new(_exeDateMaxName, ["--exe-date-max"], "Add maximum EXE date to filter");

        private const string _exeDateMinName = "exe-date-min";
        internal readonly StringInput ExeDateMinInput = new(_exeDateMinName, ["--exe-date-min"], "Add minimum EXE date to filter");

        private const string _filenameSuffixName = "filename-suffix";
        internal readonly StringInput FilenameSuffixInput = new(_filenameSuffixName, ["--filename-suffix"], "Add filename suffix to filter");

        private const string _filenameSuffixModeName = "filename-suffix-mode";
        internal readonly StringInput FilenameSuffixModeInput = new(_filenameSuffixModeName, ["--filename-suffix-mode"], "Add filename suffix matching mode to filter [any, all]");

        private const string _internalSerialName = "internal-serial";
        internal readonly StringInput InternalSerialInput = new(_internalSerialName, ["--internal-serial"], "Add internal serial to filter");

        private const string _internalSerialModeName = "finternal-serial-mode";
        internal readonly StringInput InternalSerialModeInput = new(_internalSerialModeName, ["--internal-serial-mode"], "Add internal serial matching mode to filter [any, all]");

        private const string _languageName = "language";
        internal readonly StringInput LanguageInput = new(_languageName, ["--language"], "Add language to filter");

        private const string _letterName = "letter";
        internal readonly StringInput LetterInput = new(_letterName, ["--letter"], "Add title first letter to filter");

        private const string _limitName = "limit";
        internal readonly Int32Input LimitInput = new(_limitName, ["--limit"], "Limit number of retrieved result pages");

        private const string _listName = "list";
        internal readonly FlagInput ListInput = new(_listName, ["-l", "--list"], "Only list the page IDs for the filters");

        private const string _masteringCodeName = "mastering-code";
        internal readonly StringInput MasteringCodeInput = new(_masteringCodeName, ["--mastering-code"], "Add mastering code to filter");

        private const string _masteringSidName = "mastering-sid";
        internal readonly StringInput MasteringSidInput = new(_masteringSidName, ["--mastering-sid"], "Add mastering SID to filter");

        private const string _maximumName = "maximum";
        internal readonly Int32Input MaximumInput = new(_maximumName, ["-max", "--maximum"], "Upper bound for page numbers (incompatible with --onlynew)");

        private const string _mediaName = "media";
        internal readonly StringInput MediaInput = new(_mediaName, ["--media"], "Add media type to filter");

        private const string _minimumName = "minimum";
        internal readonly Int32Input MinimumInput = new(_minimumName, ["-min", "--minimum"], "Lower bound for page numbers (incompatible with --onlynew)");

        private const string _mouldSidName = "mould-sid";
        internal readonly StringInput MouldSidInput = new(_mouldSidName, ["--mould-sid"], "Add mould SID to filter");

        private const string _notTagsName = "not-tags";
        internal readonly StringListInput NotTagsInput = new(_notTagsName, ["--not-tag"], "Add not tag to filter (multiple allowed)");

        private const string _offsetMaxName = "offset-max";
        internal readonly Int32Input OffsetMaxInput = new(_offsetMaxName, ["--offset-max"], "Add maximum offset to filter");

        private const string _offsetMinName = "offset-min";
        internal readonly Int32Input OffsetMinInput = new(_offsetMinName, ["--offset-min"], "Add minimum offset to filter");

        private const string _orderName = "order";
        internal readonly StringInput OrderInput = new(_orderName, ["--order"], "Add sort order to filter [asc, desc]");

        private const string _protectionName = "protection";
        internal readonly StringInput ProtectionInput = new(_protectionName, ["--protection"], "Add protection to filter");

        private const string _pvdDateMaxName = "pvd-date-max";
        internal readonly StringInput PvdDateMaxInput = new(_pvdDateMaxName, ["--pvd-date-max"], "Add maximum PVD date to filter");

        private const string _pvdDateMinName = "pvd-date-min";
        internal readonly StringInput PvdDateMinInput = new(_pvdDateMinName, ["--pvd-date-min"], "Add minimum PVD date to filter");

        private const string _queryName = "query";
        internal readonly StringInput QueryInput = new(_queryName, ["--query"], "Add query to filter");

        private const string _regionName = "region";
        internal readonly StringInput RegionInput = new(_regionName, ["--region"], "Add region to filter");

        private const string _serialName = "serial";
        internal readonly StringInput SerialInput = new(_serialName, ["--serial"], "Add serial to filter");

        private const string _serialModeName = "serial-mode";
        internal readonly StringInput SerialModeInput = new(_serialModeName, ["--serial-mode"], "Add serial matching mode to filter [any, all]");

        private const string _sortName = "sort";
        internal readonly StringInput SortInput = new(_sortName, ["--sort"], "Add sort category to filter [title, added, region, system, version, edition, language, serial, status, modified]");

        private const string _statusName = "status";
        internal readonly StringInput StatusInput = new(_statusName, ["--status"], "Filter by status [grey, red, yellow, blue, green]");

        private const string _systemName = "system";
        internal readonly StringInput SystemInput = new(_systemName, ["--system"], "Filter by system");

        private const string _tagsName = "tags";
        internal readonly StringListInput TagsInput = new(_tagsName, ["--tag"], "Add tag to filter (multiple allowed)");

        private const string _tagModeInput = "tag-mode";
        internal readonly StringInput TagModeInput = new(_tagModeInput, ["--tag-mode"], "Add tag matching mode to filter [any, all]");

        private const string _titleName = "title";
        internal readonly StringInput TitleInput = new(_titleName, ["--title"], "Add title to filter");

        private const string _titleModeName = "title-mode";
        internal readonly StringInput TitleModeInput = new(_titleModeName, ["--title-mode"], "Add title matching mode to filter [any, all]");

        private const string _titleForeignName = "title-foreign";
        internal readonly StringInput TitleForeignInput = new(_titleForeignName, ["--title-foreign"], "Add foreign title to filter");

        private const string _titleForeignModeName = "title-foreign-mode";
        internal readonly StringInput TitleForeignModeInput = new(_titleForeignModeName, ["--title-foreign-mode"], "Add foreign title matching mode to filter [any, all]");

        private const string _toolstampName = "toolstamp";
        internal readonly StringInput ToolstampInput = new(_toolstampName, ["--toolstamp"], "Add toolstamp to filter");

        private const string _tracksMaxName = "tracks-max";
        internal readonly Int32Input TracksMaxInput = new(_tracksMaxName, ["--tracks-max"], "Add maximum track count to filter");

        private const string _tracksMinName = "tracks-min";
        internal readonly Int32Input TracksMinInput = new(_tracksMinName, ["--tracks-min"], "Add minimum track count to filter");

        private const string _universalHashName = "universal-hash";
        internal readonly StringInput UniversalHashInput = new(_universalHashName, ["--universal-hash"], "Add universal hash to filter");

        private const string _versionName = "version";
        internal readonly StringInput VersionInput = new(_versionName, ["--version"], "Add version to filter");

        private const string _versionModeName = "version-mode";
        internal readonly StringInput VersionModeInput = new(_versionModeName, ["--version-mode"], "Add version matching mode to filter [any, all]");

        private const string _volumeLabelName = "volume-label";
        internal readonly StringInput VolumeLabelInput = new(_volumeLabelName, ["--volume-label"], "Add volume label to filter");

        private const string _volumeLabelModeName = "volume-label-mode";
        internal readonly StringInput VolumeLabelModeInput = new(_volumeLabelModeName, ["--volume-label-mode"], "Add volume label matching mode to filter [any, all]");

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
            Add(CookiesInput);
            Add(AttemptCountInput);
            Add(TimeoutInput);
            Add(ForceDownloadInput);
            Add(ForceContinueInput);

            // Specific
            Add(LimitInput);
            Add(ListInput);
            Add(MinimumInput);
            Add(MaximumInput);

            // Filter
            Add(AdditionalMouldInput);
            Add(BarcodeInput);
            Add(BarcodeModeInput);
            Add(CategoryInput);
            Add(CommentsInput);
            Add(ContentsInput);
            Add(DiscNumberInput);
            Add(DiscTitleInput);
            Add(DiscTitleModeInput);
            Add(DumperInput);
            Add(EdcInput);
            Add(EditionInput);
            Add(EditionModeInput);
            Add(ErrorsMaxInput);
            Add(ErrorsMinInput);
            Add(ExeDateMaxInput);
            Add(ExeDateMinInput);
            Add(FilenameSuffixInput);
            Add(FilenameSuffixModeInput);
            Add(InternalSerialInput);
            Add(InternalSerialModeInput);
            Add(LanguageInput);
            Add(LetterInput);
            Add(MasteringCodeInput);
            Add(MasteringSidInput);
            Add(MediaInput);
            Add(MouldSidInput);
            Add(NotTagsInput);
            Add(OffsetMaxInput);
            Add(OffsetMinInput);
            Add(OrderInput);
            Add(ProtectionInput);
            Add(PvdDateMaxInput);
            Add(PvdDateMinInput);
            Add(QueryInput);
            Add(RegionInput);
            Add(SerialInput);
            Add(SerialModeInput);
            Add(SortInput);
            Add(StatusInput);
            Add(SystemInput);
            Add(TagsInput);
            Add(TagModeInput);
            Add(TitleInput);
            Add(TitleModeInput);
            Add(TitleForeignInput);
            Add(TitleForeignModeInput);
            Add(ToolstampInput);
            Add(TracksMaxInput);
            Add(TracksMinInput);
            Add(UniversalHashInput);
            Add(VersionInput);
            Add(VersionModeInput);
            Add(VolumeLabelInput);
            Add(VolumeLabelModeInput);
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
            // Get common values
            string? outDir = OutputInput.Value;
            string? username = UsernameInput.Value;
            string? password = PasswordInput.Value;
            string? cookieFile = CookiesInput.Value;
            int? attemptCount = AttemptCountInput.Value;
            int? timeout = TimeoutInput.Value;
            bool forceDownload = ForceDownloadInput.Value;
            bool forceContinue = ForceContinueInput.Value;

            // Get specific values
            int limit = LimitInput.Value ?? -1;
            int? minId = MinimumInput.Value;
            int? maxId = MaximumInput.Value;
            bool onlyList = ListInput.Value;

            // Get filter values
            string? additionalMould = AdditionalMouldInput.Value;
            string? barcode = BarcodeInput.Value;
            string? barcodeMode = BarcodeModeInput.Value;
            DiscCategory? category = CategoryInput.Value.ToDiscCategory();
            string? comments = CommentsInput.Value;
            string? contents = ContentsInput.Value;
            string? discNumber = DiscNumberInput.Value;
            string? discTitle = DiscTitleInput.Value;
            string? discTitleMode = DiscTitleModeInput.Value;
            string? discTitleForeign = DiscTitleForeignInput.Value;
            string? discTitleForeignMode = DiscTitleForeignModeInput.Value;
            string? dumper = DumperInput.Value;
            YesNo? edc = EdcInput.Value?.ToYesNo();
            string? edition = EditionInput.Value;
            string? editionMode = EditionModeInput.Value;
            long? errorsMax = ErrorsMaxInput.Value;
            long? errorsMin = ErrorsMinInput.Value;
            string? exeDateMax = ExeDateMaxInput.Value;
            string? exeDateMin = ExeDateMinInput.Value;
            string? filenameSuffix = FilenameSuffixInput.Value;
            string? filenameSuffixMode = FilenameSuffixModeInput.Value;
            string? internalSerial = InternalSerialInput.Value;
            string? internalSerialMode = InternalSerialModeInput.Value;
            LanguageCode? language = LanguageInput.Value.ToLanguageCode();
            char? letter = string.IsNullOrEmpty(LetterInput.Value)
                ? null
                : LetterInput.Value![0];
            string? masteringCode = MasteringCodeInput.Value;
            string? masteringSid = MasteringSidInput.Value;
            MediaType? media = MediaInput.Value.ToMediaType();
            string? mouldSid = MouldSidInput.Value;
            DiscTag[]? notTags = NotTagsInput.Value is null
                ? null
                : [.. NotTagsInput.Value.ConvertAll(t => t.ToDiscTag() ?? 0)];
            int? offsetMax = OffsetMaxInput.Value;
            int? offsetMin = OffsetMinInput.Value;
            SortDirection? order = OrderInput.Value.ToSortDirection();
            string? protection = ProtectionInput.Value;
            string? pvdDateMax = PvdDateMaxInput.Value;
            string? pvdDateMin = PvdDateMinInput.Value;
            string? query = QueryInput.Value;
            RegionCode? region = RegionInput.Value.ToRegionCode();
            string? serial = SerialInput.Value;
            string? serialMode = SerialModeInput.Value;
            SortCategory? sort = SortInput.Value.ToSortCategory();
            DumpStatus? status = StatusInput.Value.ToDumpStatus();
            PhysicalSystem? system = SystemInput.Value.ToPhysicalSystem();
            DiscTag[]? tags = TagsInput.Value is null
                ? null
                : [.. TagsInput.Value.ConvertAll(t => t.ToDiscTag() ?? 0)];;
            string? tagMode = TagModeInput.Value;
            string? title = TitleInput.Value;
            string? titleMode = TitleModeInput.Value;
            string? titleForeign = TitleForeignInput.Value;
            string? titleForeignMode = TitleForeignModeInput.Value;
            string? toolstamp = ToolstampInput.Value;
            long? tracksMax = TracksMaxInput.Value;
            long? tracksMin = TracksMinInput.Value;
            string? universalHash = UniversalHashInput.Value;
            string? version = VersionInput.Value;
            string? versionMode = VersionModeInput.Value;
            string? volumeLabel = VolumeLabelInput.Value;
            string? volumeLabelMode = VolumeLabelModeInput.Value;

            // Build the disc subpaths
            DiscSubpath[] discSubpaths = Constants.AllDiscSubpaths;

            // Output directory validation
            if (!onlyList && !ValidateAndCreateOutputDirectory(outDir))
                return false;

            // Range verification
            if ((minId is null) ^ (maxId is null))
            {
                Console.WriteLine("Both a maximum and minimum ID are required");
                return false;
            }
            else if (minId is not null && minId < 0)
            {
                Console.WriteLine($"{minId} is an invalid minimum ID");
                return false;
            }
            else if (maxId is not null && maxId < 0)
            {
                Console.WriteLine($"{maxId} is an invalid maximum ID");
                return false;
            }

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
            if ((minId is null || maxId is null) && onlyList)
            {
                processingTask = _client.ListDiscsResults(
                    additionalMould,
                    advanced: true, // Hardcoded, only toggles the advanced options on page by default
                    barcode,
                    barcodeMode,
                    category,
                    comments,
                    contents,
                    discNumber,
                    discTitle,
                    discTitleMode,
                    discTitleForeign,
                    discTitleForeignMode,
                    dumper,
                    edc,
                    edition,
                    editionMode,
                    errorsMax,
                    errorsMin,
                    exeDateMax,
                    exeDateMin,
                    filenameSuffix,
                    filenameSuffixMode,
                    internalSerial,
                    internalSerialMode,
                    language,
                    letter,
                    masteringCode,
                    masteringSid,
                    media,
                    mouldSid,
                    notTags,
                    offsetMax,
                    offsetMin,
                    order,
                    protection,
                    pvdDateMax,
                    pvdDateMin,
                    query,
                    region,
                    serial,
                    serialMode,
                    sort,
                    status,
                    system,
                    tagMode,
                    tags,
                    title,
                    titleMode,
                    titleForeign,
                    titleForeignMode,
                    toolstamp,
                    tracksMax,
                    tracksMin,
                    universalHash,
                    version,
                    versionMode,
                    volumeLabel,
                    volumeLabelMode,
                    limit);
            }
            else if ((minId is null || maxId is null) && !onlyList)
            {
                processingTask = _client.DownloadDiscsResults(outDir,
                    additionalMould,
                    advanced: true, // Hardcoded, only toggles the advanced options on page by default
                    barcode,
                    barcodeMode,
                    category,
                    comments,
                    contents,
                    discNumber,
                    discTitle,
                    discTitleMode,
                    discTitleForeign,
                    discTitleForeignMode,
                    dumper,
                    edc,
                    edition,
                    editionMode,
                    errorsMax,
                    errorsMin,
                    exeDateMax,
                    exeDateMin,
                    filenameSuffix,
                    filenameSuffixMode,
                    internalSerial,
                    internalSerialMode,
                    language,
                    letter,
                    masteringCode,
                    masteringSid,
                    media,
                    mouldSid,
                    notTags,
                    offsetMax,
                    offsetMin,
                    order,
                    protection,
                    pvdDateMax,
                    pvdDateMin,
                    query,
                    region,
                    serial,
                    serialMode,
                    sort,
                    status,
                    system,
                    tagMode,
                    tags,
                    title,
                    titleMode,
                    titleForeign,
                    titleForeignMode,
                    toolstamp,
                    tracksMax,
                    tracksMin,
                    universalHash,
                    version,
                    versionMode,
                    volumeLabel,
                    volumeLabelMode,
                    limit,
                    discSubpaths);
            }
            else
            {
                processingTask = _client.DownloadSiteRange(outDir, minId!.Value, maxId!.Value, discSubpaths: discSubpaths);
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
