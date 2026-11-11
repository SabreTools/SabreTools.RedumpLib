using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Represents the data returned from an `/api/disc` query
    /// </summary>
    /// TODO: Add custom converters for things like System
    /// TODO: Verify the actual types of all of these properties
    public class DiscResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("system")]
        public string? System { get; set; }

        [JsonProperty("media_type")]
        public string? MediaType { get; set; }

        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("title_foreign")]
        public string? TitleForeign { get; set; }

        [JsonProperty("disc_title")]
        public string? DiscTitle { get; set; }

        [JsonProperty("disc_title_foreign")]
        public string? DiscTitleForeign { get; set; }

        [JsonProperty("disc_number")]
        public string? DiscNumber { get; set; }

        [JsonProperty("filename_suffix")]
        public string? FilenameSuffix { get; set; }

        [JsonProperty("serial")]
        public string? Serial { get; set; }

        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("edition")]
        public string? Edition { get; set; }

        [JsonProperty("barcode")]
        public string? Barcode { get; set; }

        [JsonProperty("exe_date")]
        public string? ExeDate { get; set; }

        [JsonProperty("error_count")]
        public string? ErrorCount { get; set; }

        [JsonProperty("edc")]
        public string? Edc { get; set; }

        [JsonProperty("layerbreaks")]
        public string? Layerbreaks { get; set; }

        [JsonProperty("protection")]
        public string? Protection { get; set; }

        [JsonProperty("comments")]
        public string? Comments { get; set; }

        [JsonProperty("contents")]
        public string? Contents { get; set; }

        [JsonProperty("regions")]
        public string[]? Regions { get; set; }

        [JsonProperty("languages")]
        public string[]? Languages { get; set; }

        [JsonProperty("added_at")]
        public string? AddedAt { get; set; }

        [JsonProperty("modified_at")]
        public string? ModifiedAt { get; set; }

        [JsonProperty("files")]
        public DiscResponseFile[]? Files { get; set; }

        [JsonProperty("files")]
        public DiscResponseExtra[]? Extras { get; set; }

        [JsonProperty("ring_codes")]
        public string? RingCodes { get; set; }

        [JsonProperty("sector_ranges")]
        public string[]? SectorRanges { get; set; }

        [JsonProperty("cue")]
        public string? Cue { get; set; }

        [JsonProperty("universal_hash")]
        public string? UniversalHash { get; set; }

        [JsonProperty("pvd")]
        public string? Pvd { get; set; }

        [JsonProperty("pic")]
        public string? Pic { get; set; }

        [JsonProperty("header")]
        public string? Header { get; set; }

        [JsonProperty("bca_mastering")]
        public string[]? BcaMastering { get; set; }
    }

    /// <summary>
    /// A single file entry in a disc response
    /// </summary>
    public class DiscResponseFile
    {
        // `null` indicates the cuesheet
        [JsonProperty("track_number")]
        public string? TrackNumber { get; set; }

        [JsonProperty("size")]
        public string? Size { get; set; }

        [JsonProperty("crc32")]
        public string? Crc32 { get; set; }

        [JsonProperty("md5")]
        public string? Md5 { get; set; }

        [JsonProperty("sha1")]
        public string? Sha1 { get; set; }
    }

    /// <summary>
    /// A single extra entry in a disc response
    /// </summary>
    public class DiscResponseExtra
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("size")]
        public string? Size { get; set; }

        [JsonProperty("crc32")]
        public string? Crc32 { get; set; }

        [JsonProperty("md5")]
        public string? Md5 { get; set; }

        [JsonProperty("sha1")]
        public string? Sha1 { get; set; }
    }
}
