using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Submission controls edit form section
    /// </summary>
    public class SubmissionControlsSection : ICloneable
    {
        [JsonProperty(PropertyName = "dump_log", DefaultValueHandling = DefaultValueHandling.Include)]
        public string? DumpLog { get; set; }

        [JsonProperty(PropertyName = "extra_upload_url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? LogsArchiveURL { get; set; }

        /// <remarks>Hidden from submission</remarks>
        [JsonProperty(PropertyName = "review_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string? ReviewComment { get; set; }

        [JsonProperty(PropertyName = "submission_comment", NullValueHandling = NullValueHandling.Ignore)]
        public string? SubmissionComment { get; set; }

        [JsonProperty(PropertyName = "submit_as", NullValueHandling = NullValueHandling.Ignore)]
        public string? SubmitAs { get; set; }

        /// <inheritdoc/>
        public object Clone()
        {
            return new SubmissionControlsSection
            {
                DumpLog = this.DumpLog,
                LogsArchiveURL = this.LogsArchiveURL,
                ReviewComment = this.ReviewComment,
                SubmissionComment = this.SubmissionComment,
                SubmitAs = this.SubmitAs,
            };
        }
    }
}
