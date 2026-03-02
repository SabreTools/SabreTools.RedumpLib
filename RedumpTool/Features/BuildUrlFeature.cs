using System;
using SabreTools.RedumpLib.Data;
using SabreTools.RedumpLib.Web;

namespace RedumpTool.Features
{
    // TODO: Support more than just /discs/ paths
    internal sealed class BuildUrlFeature : BaseFeature
    {
        #region Feature Definition

        public const string DisplayName = "build-url";

        private static readonly string[] _flags = ["build-url"];

        private const string _description = "Builds and outputs a requested discs URL";

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
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
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

            // Format and print the discs URL
            string discsUrl = UrlBuilder.BuildDiscsUrl(antimodchip,
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
                page);
            Console.WriteLine($"URL: {discsUrl}");
            return true;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;
    }
}
