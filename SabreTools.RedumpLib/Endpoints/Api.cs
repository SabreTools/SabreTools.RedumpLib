using System;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Endpoints
{
    // TODO: Create models for the returned API data
    // - `discs` returns a JSON array of disc IDs
    // - `disc` endpoint returns JSON-encoded data
    [JsonObject("api")]
    public class Api
    {
        [JsonProperty("subpath")]
        public ApiSubpath? Subpath { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("crc32")]
        public string? Crc32 { get; set; }

        [JsonProperty("md5")]
        public string? Md5 { get; set; }

        [JsonProperty("sha1")]
        public string? Sha1 { get; set; }

        /// <summary>
        /// Build a /disc/ path URL
        /// </summary>
        public string BuildUrl()
        {
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = "api",
            };

            if (Subpath == ApiSubpath.Disc)
            {
                ub.Path += "/disc";
                ub.Query = $"id={Math.Abs(Id)}";
            }
            else if (Subpath == ApiSubpath.Discs)
            {
                ub.Path += "/discs";
                if (Sha1 is not null)
                    ub.Query = $"sha1={Sha1}";
                else if (Md5 is not null)
                    ub.Query = $"md5={Md5}";
                else if (Crc32 is not null)
                    ub.Query = $"crc32={Crc32}";
            }

            return ub.ToString();
        }
    }
}
