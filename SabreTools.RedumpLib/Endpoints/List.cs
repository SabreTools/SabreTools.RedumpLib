using System;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Endpoints
{
    // TODO: Implement download and client extensions for these paths
    [JsonObject("list")]
    public class List
    {
        [JsonProperty("subpath")]
        public ListSubpath? Subpath { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("syscode")]
        public PhysicalSystem? System { get; set; }

        /// <summary>
        /// Build a /list/ path URL
        /// </summary>
        public string BuildUrl()
        {
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = "list",
            };

            switch (Subpath)
            {
                case ListSubpath.Have:
                case ListSubpath.Cuesheets:
                case ListSubpath.Datfile:
                    ub.Path += $"/{Subpath.ShortName()}";
                    break;

                // Nothing else is valid as a subpath
                default:
                    break;
            }

            ub.Path += $"/{Username}";
            if (System?.Code is not null)
                ub.Path += $"/{System.Code}";

            return ub.ToString();
        }
    }
}
