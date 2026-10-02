using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Endpoints
{
    [JsonObject("downloads")]
    public class Downloads
    {
        [JsonProperty("database")]
        public bool? Database { get; set; }

        /// <summary>
        /// Build a /downloads/ path URL
        /// </summary>
        public string BuildUrl()
        {
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = "downloads",
            };

            if (Database == true)
                ub.Path += "/database";

            return ub.ToString();
        }
    }
}
