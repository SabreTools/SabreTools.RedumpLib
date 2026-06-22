using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Dump metadata edit form section
    /// </summary>
    public class DumpMetadataSection : ICloneable
    {
        [JsonProperty(PropertyName = "comments", NullValueHandling = NullValueHandling.Ignore)]
        public string? Comments { get; set; }

        /// <remarks>
        /// These should be formatted and included in <see cref="Comments"/>
        /// </remarks>
        [JsonIgnore]
        public Dictionary<SiteCode, string> CommentsSpecialFields { get; set; } = [];

        [JsonProperty(PropertyName = "contents", NullValueHandling = NullValueHandling.Ignore)]
        public string? Contents { get; set; }

        /// <remarks>
        /// These should be formatted and included in <see cref="Contents"/>
        /// </remarks>
        [JsonIgnore]
        public Dictionary<SiteCode, string> ContentsSpecialFields { get; set; } = [];

        [JsonProperty(PropertyName = "protection", NullValueHandling = NullValueHandling.Ignore)]
        public string? Protection { get; set; }

        [JsonProperty(PropertyName = "sector_ranges", NullValueHandling = NullValueHandling.Ignore)]
        public string? SectorRanges { get; set; }

        [JsonProperty(PropertyName = "sbi", NullValueHandling = NullValueHandling.Ignore)]
        public string? SBI { get; set; }

        [JsonProperty(PropertyName = "pvd", NullValueHandling = NullValueHandling.Ignore)]
        public string? PVD { get; set; }

        [JsonProperty(PropertyName = "header", NullValueHandling = NullValueHandling.Ignore)]
        public string? Header { get; set; }

        [JsonProperty(PropertyName = "bca", NullValueHandling = NullValueHandling.Ignore)]
        public string? BCA { get; set; }

        [JsonProperty(PropertyName = "pic", NullValueHandling = NullValueHandling.Ignore)]
        public string? PIC { get; set; }

        [JsonProperty(PropertyName = "cue", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cuesheet { get; set; }

        [JsonProperty(PropertyName = "files_xml", NullValueHandling = NullValueHandling.Ignore)]
        public string? Dat { get; set; }

        /// <inheritdoc/>
        public object Clone()
        {
            Dictionary<SiteCode, string> commentsSpecialFields = [];
            foreach (var kvp in this.CommentsSpecialFields)
            {
                commentsSpecialFields[kvp.Key] = kvp.Value;
            }

            Dictionary<SiteCode, string> contentsSpecialFields = [];
            foreach (var kvp in this.ContentsSpecialFields)
            {
                contentsSpecialFields[kvp.Key] = kvp.Value;
            }

            return new DumpMetadataSection
            {
                Comments = this.Comments,
                CommentsSpecialFields = commentsSpecialFields,
                Contents = this.Contents,
                ContentsSpecialFields = contentsSpecialFields,
                Protection = this.Protection,
                SectorRanges = this.SectorRanges,
                SBI = this.SBI,
                PVD = this.PVD,
                Header = this.Header,
                BCA = this.BCA,
                PIC = this.PIC,
                Cuesheet = this.Cuesheet,
                Dat = this.Dat,
            };
        }
    }
}
