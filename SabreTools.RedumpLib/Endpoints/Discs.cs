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

        [JsonProperty("status")]
        public DumpStatus? Status { get; set; }

        [JsonProperty("letter")]
        public char? Letter { get; set; }

        [JsonProperty("dumper")]
        public string? Dumper { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("title_exact")]
        public bool? TitleExact { get; set; }

        [JsonProperty("title_foreign")]
        public string? TitleForeign { get; set; }

        [JsonProperty("title_foreign_exact")]
        public bool? TitleForeignExact { get; set; }

        [JsonProperty("serial")]
        public string? Serial { get; set; }

        [JsonProperty("serial_exact")]
        public bool? SerialExact { get; set; }

        [JsonProperty("edition")]
        public string? Edition { get; set; }

        [JsonProperty("edition_exact")]
        public bool? EditionExact { get; set; }

        [JsonProperty("barcode")]
        public string? Barcode { get; set; }

        [JsonProperty("barcode_exact")]
        public bool? BarcodeExact { get; set; }

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

        [JsonProperty("ringcode")]
        public string? Ringcode { get; set; }

        [JsonProperty("offset")]
        public long? Offset { get; set; }

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
            if (Title is not null && TitleExact is not null)
                sb.Append($"title_exact={TitleExact.ToYesNo().LongName()}&");

            // Foreign Title
            if (TitleForeign is not null)
                sb.Append($"title_foreign={TitleForeign}&");
            if (TitleForeign is not null && TitleForeignExact is not null)
                sb.Append($"title_foreign_exact={TitleForeignExact.ToYesNo().LongName()}&");

            // Serial
            if (Serial is not null)
                sb.Append($"serial={Serial}&");
            if (Serial is not null && SerialExact is not null)
                sb.Append($"serial_exact={SerialExact.ToYesNo().LongName()}&");

            // Edition
            if (Edition is not null)
                sb.Append($"edition={Edition}&");
            if (Edition is not null && EditionExact is not null)
                sb.Append($"edition_exact={EditionExact.ToYesNo().LongName()}&");

            // Barcode
            if (Barcode is not null)
                sb.Append($"barcode={Barcode}&");
            if (Barcode is not null && BarcodeExact is not null)
                sb.Append($"barcode_exact={BarcodeExact.ToYesNo().LongName()}&");

            // Universal Hash
            if (UniversalHash is not null)
                sb.Append($"universal_hash={UniversalHash}&");

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

            // Ringcode
            if (Ringcode is not null)
                sb.Append($"ringcode={Ringcode}&");

            // Offset
            if (Offset is not null)
                sb.Append($"offset={Offset}&");

            // General Query
            if (Query is not null)
                sb.Append($"q={Query}&");

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

            return sb.ToString();
        }
    }
}
