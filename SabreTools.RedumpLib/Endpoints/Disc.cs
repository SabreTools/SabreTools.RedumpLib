using System;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Endpoints
{
    /// TODO: Handle submit path?
    [JsonObject("disc")]
    public class Disc
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("subpath")]
        public DiscSubpath? Subpath { get; set; }

        /// <summary>
        /// Build a /discs/ path URL
        /// </summary>
        public string BuildUrl()
        {
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = $"disc/{Math.Abs(Id)}",
            };

            switch (Subpath)
            {
                case DiscSubpath.Cuesheet:
                case DiscSubpath.Edit:
                case DiscSubpath.SBI:
                    ub.Path += $"/{Subpath.ShortName()}";
                    break;

                // History and null are invalid for a disc page
                case DiscSubpath.History:
                case null:
                default:
                    break;
            }

            return ub.ToString();
        }
    }
}
