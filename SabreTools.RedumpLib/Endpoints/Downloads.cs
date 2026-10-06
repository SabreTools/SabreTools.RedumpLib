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

                // HACK: Make copies of the pack and system
                var pack = Pack;
                var system = System;

                // HACK: Normalize old BIOS systems
#pragma warning disable CS0618 // Type or member is obsolete
                if (Pack == PackType.Datfile && System == PhysicalSystem.MicrosoftXboxBIOS)
                {
                    pack = PackType.BiosDatfile;
                    system = PhysicalSystem.MicrosoftXbox;
                }
                else if (Pack == PackType.Datfile && System == PhysicalSystem.NintendoGameCubeBIOS)
                {
                    pack = PackType.BiosDatfile;
                    system = PhysicalSystem.NintendoGameCube;
                }
                else if (Pack == PackType.Datfile && System == PhysicalSystem.SonyPlayStationBIOS)
                {
                    pack = PackType.BiosDatfile;
                    system = PhysicalSystem.SonyPlayStation;
                }
                else if (Pack == PackType.Datfile && System == PhysicalSystem.SonyPlayStation2BIOS)
                {
                    pack = PackType.BiosDatfile;
                    system = PhysicalSystem.SonyPlayStation2;
                }
#pragma warning restore CS0618 // Type or member is obsolete

                string systemName = system?.Code ?? string.Empty;
                switch (pack)
                {
                    case PackType.Cuesheets: ub.Path = $"cues/{systemName}"; break;
                    case PackType.Datfile: ub.Path = $"datfile/{systemName}"; break;
                    case PackType.Sbis: ub.Path = $"sbi/{systemName}"; break;
                    case PackType.BiosDatfile: ub.Path = $"bios/{systemName}"; break;

                    // Invalid
                    default: throw new ArgumentOutOfRangeException(nameof(pack));
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
