using System;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// URL builder helper
    /// </summary>
    public static class UrlBuilder
    {
        #region BIOS File Names

        /// <summary>
        /// Microsoft XBOX BIOS datfile filename
        /// </summary>
        public const string MicrosoftXboxBIOSFilename = "Microsoft%20-%20Xbox%20-%20BIOS%20Images%20%289%29%20%282026-06-16%29.dat";

        /// <summary>
        /// Nintendo GameCube BIOS datfile filename
        /// </summary>
        public const string NintendoGameCubeBIOSFilename = "Nintendo%20-%20GameCube%20-%20BIOS%20Images%20%2817%29%20%282026-06-16%29.dat";

        /// <summary>
        /// Sony PlayStation BIOS datfile filename
        /// </summary>
        public const string SonyPlayStationBIOSFilename = "Sony%20-%20PlayStation%20-%20BIOS%20Images%20%2824%29%20%282026-06-16%29.dat";

        /// <summary>
        /// Sony PlayStation 2 BIOS datfile filename
        /// </summary>
        public const string SonyPlayStation2BIOSFilename = "Sony%20-%20PlayStation%202%20-%20BIOS%20Datfile%20%28140%29%20%282026-06-16%29.dat";

        #endregion

        /// <summary>
        /// Build a direct-download path URL
        /// </summary>
        /// <param name="packType">Pack type</param>
        /// <param name="system">System for download</param>
        /// <remarks>Does not check for invalid systems</remarks>
        /// TODO: Handle download_dat_variant?
        public static string BuildPackUrl(PackType packType, PhysicalSystem system)
        {
            // Hack to support the static BIOS sets
            if (packType == PackType.Datfile
                && (system == PhysicalSystem.MicrosoftXboxBIOS
                    || system == PhysicalSystem.NintendoGameCubeBIOS
                    || system == PhysicalSystem.SonyPlayStationBIOS
                    || system == PhysicalSystem.SonyPlayStation2BIOS))
            {
                return BuildBiosUrl(system);
            }

            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
            };

            string systemName = system.Code ?? string.Empty;
            switch (packType)
            {
                case PackType.Cuesheets: ub.Path = $"cues/{systemName}"; break;
                case PackType.Datfile: ub.Path = $"datfile/{systemName}"; break;
                case PackType.Sbis: ub.Path = $"sbi/{systemName}"; break;

                // Invalid
                default: throw new ArgumentOutOfRangeException(nameof(packType));
            }

            return ub.ToString();
        }

        /// <summary>
        /// Build a /static/bios/ path URL
        /// </summary>
        /// <param name="system">System to retrieve static BIOS datfile for, required</param>
        /// <remarks>Handles the non-BIOS variants of systems for compatibility</remarks>
        private static string BuildBiosUrl(PhysicalSystem system)
        {
            string? filename;
            if (system == PhysicalSystem.MicrosoftXbox || system == PhysicalSystem.MicrosoftXboxBIOS)
                filename = MicrosoftXboxBIOSFilename;
            else if (system == PhysicalSystem.NintendoGameCube || system == PhysicalSystem.NintendoGameCubeBIOS)
                filename = NintendoGameCubeBIOSFilename;
            else if (system == PhysicalSystem.SonyPlayStation || system == PhysicalSystem.SonyPlayStationBIOS)
                filename = SonyPlayStationBIOSFilename;
            else if (system == PhysicalSystem.SonyPlayStation2 || system == PhysicalSystem.SonyPlayStation2BIOS)
                filename = SonyPlayStation2BIOSFilename;
            else
                filename = null;

            // Ignore invalid BIOS systems
            if (filename is null)
                return string.Empty;

            var ub = new UriBuilder
            {
                Scheme = "https",
                Host = "redump.info",
                Path = $"static/bios/{filename}",
            };

            return ub.ToString();
        }
    }
}
