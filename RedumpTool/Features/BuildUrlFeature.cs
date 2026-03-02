using System;
using SabreTools.CommandLine.Inputs;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace RedumpTool.Features
{
    internal sealed class BuildUrlFeature : BaseFeature
    {
        #region Feature Definition

        public const string DisplayName = "build-url";

        private static readonly string[] _flags = ["build-url"];

        private const string _description = "Builds and outputs a requested discs URL";

        #endregion

        #region Inputs

        private const string _basePathName = "basepath";
        internal readonly StringInput BasePathInput = new(_basePathName, ["-b", "--base-path"], "Indicate base path for building URL (disc [requires --disc-id], discs, discs-wip, downloads, list [requires --dumper], newdisc, pack [requires --pack and --system], statistics)");

        private const string _discIdName = "discid";
        internal readonly Int32Input DiscIdInput = new(_discIdName, ["-i", "--disc-id"], "Disc ID, requires 'disc'");

        private const string _haveName = "have";
        internal readonly BooleanInput HaveInput = new(_haveName, ["-g", "--have"], "Have or miss filter, requires 'list'");

        private const string _newDiscIdName = "newdiscid";
        internal readonly Int32Input NewDiscIdInput = new(_newDiscIdName, ["-w", "--newdisc-id"], "Disc WIP ID, requires 'newdisc'");

        private const string _packName = "pack";
        internal readonly StringInput PackInput = new(_packName, ["-k", "--pack"], "Download pack ID, requires 'downloads' and --system (cues, datfile, dkeys, gdi, keys, lsd, sbi)");

        private const string _subpathName = "subpath";
        internal readonly StringInput SubpathInput = new(_subpathName, ["-s", "--subpath"], "Disc page subpath, requires 'disc' (changes, cue, edit, gdi, key, lsd, md5, sbi, sfv, sha1)");

        #endregion

        public BuildUrlFeature()
           : base(DisplayName, _flags, _description)
        {
            RequiresInputs = false;

            // Common -- Unused
            Add(DebugInput);
            Add(OutputInput);
            Add(UsernameInput);
            Add(PasswordInput);
            Add(AttemptCountInput);
            Add(TimeoutInput);
            Add(ForceDownloadInput);
            Add(ForceContinueInput);

            // Disc path
            Add(DiscIdInput);
            Add(SubpathInput);

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
            Add(PageInput);
            Add(ProtectionInput);
            Add(QuickSearchInput);
            Add(RegionInput);
            Add(RingcodeInput);
            Add(SortInput);
            Add(SortDirInput);
            Add(StatusInput);
            Add(SystemInput);
            Add(TracksInput);

            // Downloads (Packs) Path
            Add(PackInput);

            // List Path
            Add(HaveInput);

            // New Disc Path
            Add(NewDiscIdInput);
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
            // Get disc path values
            int? discId = DiscIdInput.Value;
            string? subpathString = SubpathInput.Value;
            DiscSubpath? subpath = subpathString.ToDiscSubpath();

            // Get discs path values
            bool? antimodchip = AntiModchipInput.Value;
            bool barcode = BarcodeInput.Value;
            string? categoryString = CategoryInput.Value;
            DiscCategory? category = categoryString.ToDiscCategory();
            bool comments = CommentsInput.Value;
            bool contents = ContentsInput.Value;
            string? discTypeString = DiscTypeInput.Value;
            DiscType? discType = discTypeString.ToDiscType();
            string? dumper = DumperInput.Value;
            bool? edcBool = EdcInput.Value;
            YesNo? edc = edcBool?.ToYesNo();
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
            int? page = PageInput.Value;
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

            // Get the downloads path values
            string? packString = PackInput.Value;
            PackType? pack = packString.ToPackType();

            // Get the list path values
            bool? have = HaveInput.Value;

            // Get new disc path values
            int? newDiscId = NewDiscIdInput.Value;

            // Get specific values
            string? basePath = BasePathInput.Value;

            // Build and print the URL
            string? url = basePath?.ToLowerInvariant() switch
            {
                "disc" => discId is null
                    ? null
                    : UrlBuilder.BuildDiscUrl(discId.Value, subpath),

                "discs" => UrlBuilder.BuildDiscsUrl(antimodchip,
                    barcode,
                    category,
                    discType,
                    dumper,
                    edc,
                    edition,
                    errors,
                    language,
                    letter,
                    libcrypt,
                    media,
                    offset,
                    quicksearch,
                    region,
                    ringcode,
                    sort,
                    sortDir,
                    status,
                    system,
                    tracks,
                    comments,
                    contents,
                    protection,
                    page),

                "discs-wip" => newDiscId is null
                    ? UrlBuilder.BuildDiscsWipUrl()
                    : UrlBuilder.BuildNewDiscUrl(newDiscId.Value),
                "newdisc" => newDiscId is null
                    ? UrlBuilder.BuildDiscsWipUrl()
                    : UrlBuilder.BuildNewDiscUrl(newDiscId.Value),

                "downloads" => pack is null || system is null
                    ? UrlBuilder.BuildDownloadsUrl()
                    : UrlBuilder.BuildPackUrl(pack.Value, system.Value),
                "pack" => pack is null || system is null
                    ? UrlBuilder.BuildDownloadsUrl()
                    : UrlBuilder.BuildPackUrl(pack.Value, system.Value),

                "list" => dumper is null
                    ? null
                    : UrlBuilder.BuildListUrl(dumper, have, system),

                "member2dumper" => UrlBuilder.BuildMemberPromotionUrl(),
                "memberpromotion" => UrlBuilder.BuildMemberPromotionUrl(),

                "statistics" => UrlBuilder.BuildStatisticsUrl(),

                _ => null,
            };

            if (url is null)
                Console.WriteLine("An error occurred, please check for required inputs");
            else
                Console.WriteLine($"URL: {url}");

            return true;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;
    }
}
