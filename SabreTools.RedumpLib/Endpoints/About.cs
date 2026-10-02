using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Endpoints
{
    [JsonObject("about")]
    public class About
    {
        /// <summary>
        /// Build a /discs/ path URL
        /// </summary>
        public string BuildUrl()
        {
            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = "about",
            };

            return ub.ToString();
        }
    }
}
