using System;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Endpoints
{
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

            // `disc` endpoint returns JSON-encoded data
            if (Subpath == ApiSubpath.Disc)
            {
                ub.Path += "/disc";
                ub.Query = $"id={Math.Abs(Id)}";
            }

            // `discs` returns a JSON array of disc IDs
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
