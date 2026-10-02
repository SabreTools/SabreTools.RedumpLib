using System;
using System.Text;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Endpoints
{
    [JsonObject("queue")]
    public class Queue
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("status")]
        public DumpStatus? Status { get; set; }

        [JsonProperty("sub_type")]
        public SubmissionType? SubType { get; set; }

        [JsonProperty("system")]
        public PhysicalSystem? System { get; set; }

        [JsonProperty("submitter")]
        public string? Submitter { get; set; }

        [JsonProperty("disc_id")]
        public long? DiscId { get; set; }

        [JsonProperty("sort")]
        public SortCategory? Sort { get; set; }

        [JsonProperty("order")]
        public SortDirection? Order { get; set; }

        [JsonProperty("page")]
        public long? Page { get; set; }

        [JsonProperty("is_disc_history")]
        public bool? IsDiscHistory { get; set; }

        /// <summary>
        /// Build a /queue/ path URL
        /// </summary>
        public string BuildUrl()
        {
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
            };

            // If an ID is present, it stands alone
            if (Id is not null)
            {
                ub.Path = $"queue/{Math.Abs(Id.Value)}/";
            }
            else
            {
                ub.Path = "queue";
                ub.Query = BuildQuery();
            }

            return ub.ToString();
        }

        /// <summary>
        /// Build a /queue/ path query
        /// </summary>
        /// <remarks>Ordered according to site source code</remarks>
        private string BuildQuery()
        {
            var sb = new StringBuilder();

            // Status
            string? status = Status.LongName();
            if (status is not null)
                sb.Append($"status={status}&");

            // Submission Type
            string? subType = SubType.ShortName();
            if (subType is not null)
                sb.Append($"sub_type={subType}&");

            // System
            string? system = System?.Code;
            if (system is not null)
                sb.Append($"system={system}&");

            // Submitter
            if (Submitter is not null)
                sb.Append($"submitter={Submitter}&");

            // Disc ID
            if (DiscId is not null)
                sb.Append($"disc_id={DiscId}&");

            // Sorting
            if (Sort is not null && Enum.IsDefined(typeof(SortCategory), Sort))
                sb.Append($"sort={Sort.ShortName()}&");

            // Sort Direction
            if (Order is not null && Enum.IsDefined(typeof(SortDirection), Order))
                sb.Append($"order={Order.ShortName()}&");

            // Page Number
            if (Page is not null)
                sb.Append($"page={Page}");

            // Is Disc History
            if (IsDiscHistory is not null)
                sb.Append($"is_disc_history={IsDiscHistory.ToYesNo().LongName()}&");

            return sb.ToString();
        }
    }
}
