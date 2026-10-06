using System;
using Newtonsoft.Json;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Endpoints
{
    [JsonObject("downloads")]
    public class Downloads
    {
        [JsonProperty("database")]
        public bool? Database { get; set; }

        [JsonProperty("pack")]
        public PackType? Pack { get; set; }

        [JsonProperty("system")]
        public PhysicalSystem? System { get; set; }

        /// <summary>
        /// Build a /downloads/ path URL
        /// </summary>
        /// <remarks>Also includes pack URLs, e.g. `/cues/`</remarks>
        public string BuildUrl()
        {
            // Handle pack downloads -- TODO: Separate into different endpoints
            if (Pack is not null)
            {
                var ub = new UriBuilder
                {
                    Scheme = "https",
                    Host = "redump.info",
                };

                string systemName = System?.Code ?? string.Empty;
                switch (Pack)
                {
                    case PackType.Cuesheets:
                        ub.Path = $"cues/{systemName}";
                        break;

                    case PackType.Datfile:
                        // BIOS systems need to map back to their actual system names
                        if (System == PhysicalSystem.MicrosoftXboxBIOS)
                            ub.Path = $"bios/{PhysicalSystem.MicrosoftXbox.Code}";
                        else if (System == PhysicalSystem.NintendoGameCubeBIOS)
                            ub.Path = $"bios/{PhysicalSystem.NintendoGameCube.Code}";
                        else if (System == PhysicalSystem.SonyPlayStationBIOS)
                            ub.Path = $"bios/{PhysicalSystem.SonyPlayStation.Code}";
                        else if (System == PhysicalSystem.SonyPlayStation2BIOS)
                            ub.Path = $"bios/{PhysicalSystem.SonyPlayStation2.Code}";
                        else
                            ub.Path = $"datfile/{systemName}";

                        break;

                    case PackType.Sbis:
                        ub.Path = $"sbi/{systemName}";
                        break;

                    // Invalid
                    default: throw new ArgumentOutOfRangeException(nameof(Pack));
                }

                return ub.ToString();
            }

            // Otherwise, handle the default path
            else
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
}
