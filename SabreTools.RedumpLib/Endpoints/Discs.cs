using System;
using System.Text;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Endpoints
{
    [JsonObject("discs")]
    public class Discs
    {
        [JsonProperty("system")]
        public PhysicalSystem? System { get; set; }

        [JsonProperty("region")]
        public RegionCode? Region { get; set; }

        [JsonProperty("language")]
        public LanguageCode? Language { get; set; }

        [JsonProperty("media")]
        public MediaType? Media { get; set; }

        [JsonProperty("category")]
        public DiscCategory? Category { get; set; }

        [JsonProperty("tags")]
        public DiscTag[]? Tags { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("tag_mode")]
        public string? TagMode { get; set; }

        [JsonProperty("not_tags")]
        public DiscTag[]? NotTags { get; set; }

        [JsonProperty("status")]
        public DumpStatus? Status { get; set; }

        [JsonProperty("letter")]
        public char? Letter { get; set; }

        [JsonProperty("dumper")]
        public string? Dumper { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("title_mode")]
        public string? TitleMode { get; set; }

        [JsonProperty("title_foreign")]
        public string? TitleForeign { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("title_foreign_mode")]
        public string? TitleForeignMode { get; set; }

        [JsonProperty("disc_number")]
        public string? DiscNumber { get; set; }

        [JsonProperty("disc_title")]
        public string? DiscTitle { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("disc_title_mode")]
        public string? DiscTitleMode { get; set; }

        [JsonProperty("disc_title_foreign")]
        public string? DiscTitleForeign { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("disc_title_foreign_mode")]
        public string? DiscTitleForeignMode { get; set; }

        [JsonProperty("serial")]
        public string? Serial { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("serial_mode")]
        public string? SerialMode { get; set; }

        [JsonProperty("filename_suffix")]
        public string? FilenameSuffix { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("filename_suffix_mode")]
        public string? FilenameSuffixMode { get; set; }

        [JsonProperty("exe_date_min")]
        public string? ExeDateMin { get; set; }

        [JsonProperty("exe_date_max")]
        public string? ExeDateMax { get; set; }

        [JsonProperty("pvd_date_min")]
        public string? PvdDateMin { get; set; }

        [JsonProperty("pvd_date_max")]
        public string? PvdDateMax { get; set; }

        [JsonProperty("edition")]
        public string? Edition { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("edition_mode")]
        public string? EditionMode { get; set; }

        [JsonProperty("barcode")]
        public string? Barcode { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("barcode_mode")]
        public string? BarcodeMode { get; set; }

        [JsonProperty("universal_hash")]
        public string? UniversalHash { get; set; }

        [JsonProperty("tracks_min")]
        public long? TracksMin { get; set; }

        [JsonProperty("tracks_max")]
        public long? TracksMax { get; set; }

        [JsonProperty("errors_min")]
        public long? ErrorsMin { get; set; }

        [JsonProperty("errors_max")]
        public long? ErrorsMax { get; set; }

        [JsonProperty("edc")]
        public YesNo? Edc { get; set; }

        [JsonProperty("protection")]
        public string? Protection { get; set; }

        [JsonProperty("comments")]
        public string? Comments { get; set; }

        [JsonProperty("contents")]
        public string? Contents { get; set; }

        [JsonProperty("mastering_code")]
        public string? MasteringCode { get; set; }

        [JsonProperty("mastering_sid")]
        public string? MasteringSid { get; set; }

        [JsonProperty("toolstamp")]
        public string? Toolstamp { get; set; }

        [JsonProperty("mould_sid")]
        public string? MouldSid { get; set; }

        [JsonProperty("additional_mould")]
        public string? AdditionalMould { get; set; }

        [JsonProperty("offset_min")]
        public long? OffsetMin { get; set; }

        [JsonProperty("offset_max")]
        public long? OffsetMax { get; set; }

        [JsonProperty("volume_label")]
        public string? VolumeLabel { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("volume_label_mode")]
        public string? VolumeLabelMode { get; set; }

        [JsonProperty("internal_serial")]
        public string? InternalSerial { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("internal_serial_mode")]
        public string? InternalSerialMode { get; set; }

        [JsonProperty("version")]
        public string? Version { get; set; }

        // TODO: Limit this to a set of a values?
        [JsonProperty("version_mode")]
        public string? VersionMode { get; set; }

        [JsonProperty("q")]
        public string? Query { get; set; }

        [JsonProperty("sort")]
        public SortCategory? Sort { get; set; }

        [JsonProperty("order")]
        public SortDirection? Order { get; set; }

        [JsonProperty("page")]
        public long? Page { get; set; }

        [JsonProperty("advanced")]
        public bool? Advanced { get; set; }

        /// <summary>
        /// Build a /discs/ path URL
        /// </summary>
        public string BuildUrl()
        {
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = "discs",
                Query = BuildQuery(),
            };

            return ub.ToString();
        }

        /// <summary>
        /// Build a /discs/ path query
        /// </summary>
        /// <remarks>Ordered according to site source code</remarks>
        private string BuildQuery()
        {
            var sb = new StringBuilder();

            // System
            string? system = System?.Code;
            if (system is not null)
                sb.Append($"system={system}&");

            // Region
            string? region = Region?.Code;
            if (region is not null)
                sb.Append($"region={region}&");

            // Language
            string? langauge = Language?.TwoLetterCode;
            if (langauge is not null)
                sb.Append($"language={langauge}&");

            // Media
            string? media = Media.ShortName();
            if (media is not null)
                sb.Append($"media={media}&");

            // Category
            string? category = Category.LongName();
            if (category is not null)
                sb.Append($"category={category}&");

            // Tag Mode
            if (TagMode is not null)
                sb.Append($"tag_mode={TagMode}&");

            // Status
            string? status = Status.LongName();
            if (status is not null)
                sb.Append($"status={status}&");

            // Letter
            if (Letter is not null)
                sb.Append($"letter={char.ToUpperInvariant(Letter.Value)}&");

            // Dumper
            if (Dumper is not null)
                sb.Append($"dumper={Dumper}&");

            // Title
            if (Title is not null)
                sb.Append($"title={Title}&");
            if (TitleMode is not null)
                sb.Append($"title_mode={TitleMode}&");

            // Foreign Title
            if (TitleForeign is not null)
                sb.Append($"title_foreign={TitleForeign}&");
            if (TitleForeignMode is not null)
                sb.Append($"title_foreign_mode={TitleForeignMode}&");

            // Disc Number
            if (DiscNumber is not null)
                sb.Append($"disc_number={DiscNumber}&");

            // Disc Title
            if (DiscTitle is not null)
                sb.Append($"disc_title={DiscTitle}&");
            if (DiscTitleMode is not null)
                sb.Append($"disc_title_mode={DiscTitleMode}&");

            // Foreign Disc Title
            if (DiscTitleForeign is not null)
                sb.Append($"disc_title_foreign={DiscTitleForeign}&");
            if (DiscTitleForeignMode is not null)
                sb.Append($"disc_title_foreign_mode={DiscTitleForeignMode}&");

            // Serial
            if (Serial is not null)
                sb.Append($"serial={Serial}&");
            if (SerialMode is not null)
                sb.Append($"serial_mode={SerialMode}&");

            // Filename Suffix
            if (FilenameSuffix is not null)
                sb.Append($"filename_suffix={FilenameSuffix}&");
            if (FilenameSuffixMode is not null)
                sb.Append($"filename_suffix_mode={FilenameSuffixMode}&");

            // Edition
            if (Edition is not null)
                sb.Append($"edition={Edition}&");
            if (Edition is not null && EditionMode is not null)
                sb.Append($"edition_mode={EditionMode}&");

            // Barcode
            if (Barcode is not null)
                sb.Append($"barcode={Barcode}&");
            if (Barcode is not null && BarcodeMode is not null)
                sb.Append($"barcode_mode={BarcodeMode}&");

            // Universal Hash
            if (UniversalHash is not null)
                sb.Append($"universal_hash={UniversalHash}&");

            // EXE Date
            if (ExeDateMin is not null)
                sb.Append($"exe_date_min={ExeDateMin}&");
            if (ExeDateMax is not null)
                sb.Append($"exe_date_max={ExeDateMax}&");

            // PVD Date
            if (PvdDateMin is not null)
                sb.Append($"pvd_date_min={PvdDateMin}&");
            if (PvdDateMax is not null)
                sb.Append($"pvd_date_max={PvdDateMax}&");

            // Track Count
            if (TracksMin is not null)
                sb.Append($"tracks_min={TracksMin}&");
            if (TracksMax is not null)
                sb.Append($"tracks_max={TracksMax}&");

            // Error Count
            if (ErrorsMin is not null)
                sb.Append($"errors_min={ErrorsMin}&");
            if (ErrorsMax is not null)
                sb.Append($"errors_max={ErrorsMax}&");

            // EDC
            if (Edc is not null && Edc != YesNo.NULL)
                sb.Append($"edc={Edc.LongName()}&");

            // Protection
            if (Protection is not null)
                sb.Append($"protection={Protection}&");

            // Comments
            if (Comments is not null)
                sb.Append($"comments={Comments}&");

            // Contents
            if (Contents is not null)
                sb.Append($"contents={Contents}&");

            // Mastering Code
            if (MasteringCode is not null)
                sb.Append($"mastering_code={MasteringCode}&");

            // Mastering SID
            if (MasteringSid is not null)
                sb.Append($"mastering_sid={MasteringSid}&");

            // Toolstamp
            if (Toolstamp is not null)
                sb.Append($"toolstamp={Toolstamp}&");

            // Mould SID
            if (MouldSid is not null)
                sb.Append($"mould_sid={MouldSid}&");

            // Additional Mould
            if (AdditionalMould is not null)
                sb.Append($"additional_mould={AdditionalMould}&");

            // Offset
            if (OffsetMin is not null)
                sb.Append($"offset_min={OffsetMin}&");
            if (OffsetMax is not null)
                sb.Append($"offset_max={OffsetMax}&");

            // Volume Label
            if (VolumeLabel is not null)
                sb.Append($"volume_label={VolumeLabel}&");
            if (VolumeLabelMode is not null)
                sb.Append($"volume_label_mode={VolumeLabelMode}&");

            // Internal Serial
            if (InternalSerial is not null)
                sb.Append($"internal_serial={InternalSerial}&");
            if (InternalSerialMode is not null)
                sb.Append($"internal_serial_mode={InternalSerialMode}&");

            // General Query
            if (Query is not null)
                sb.Append($"q={NormalizeQuery(Query)}&");

            // Sorting
            if (Sort is not null && Enum.IsDefined(typeof(SortCategory), Sort))
                sb.Append($"sort={Sort.ShortName()}&");

            // Sort Direction
            if (Order is not null && Enum.IsDefined(typeof(SortDirection), Order))
                sb.Append($"order={Order.ShortName()}&");

            // Page Number
            if (Page is not null)
                sb.Append($"page={Page}");

            // Advanced
            if (Advanced is not null)
                sb.Append($"advanced={(Advanced.Value ? "1" : "0")}&");

            // Tags
            if (Tags is not null && Tags.Length > 0)
            {
                foreach (var tag in Tags)
                {
                    // TODO: Should be URL encoded
                    sb.Append($"tag={tag}&");
                }
            }

            if (NotTags is not null && NotTags.Length > 0)
            {
                foreach (var tag in NotTags)
                {
                    // TODO: Should be URL encoded
                    sb.Append($"not_tag={tag}&");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Normalize a URL query string
        /// </summary>
        /// <param name="query">Query string to normalize</param>
        /// <returns>Normalized query</returns>
        private static string NormalizeQuery(string query)
        {
            // Strip quotes
            query = query!.Trim('"', '\'');

            // Special characters become dashes
            query = query.Replace(' ', '-');
            query = query.Replace('\\', '-');
            query = query.Replace('/', '-');

            // Lowercase is defined per language
            query = query.ToLowerInvariant();
            return query;
        }
    }
}
