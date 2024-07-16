using System;
using System.Linq;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// Contains logic for dealing with packs
    /// </summary>
    internal static class Packs
    {
        /// <summary>
        /// Download premade packs
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadPacks(RedumpClient rc, string? outDir, bool useSubfolders)
        {
#if NETFRAMEWORK || NETCOREAPP3_1
            var systems = Enum.GetValues(typeof(RedumpSystem)).OfType<RedumpSystem>().Select(s => new Nullable<RedumpSystem>(s));
#else
            var systems = Enum.GetValues<RedumpSystem>().Select(s => new RedumpSystem?(s));
#endif

            await rc.DownloadPacks(Constants.PackCuesUrl, systems.Where(s => s.HasCues()).ToArray(), "CUEs", outDir, useSubfolders ? "cue" : null);
            await rc.DownloadPacks(Constants.PackDatfileUrl, systems.Where(s => s.HasDat()).ToArray(), "DATs", outDir, useSubfolders ? "dat" : null);
            await rc.DownloadPacks(Constants.PackDkeysUrl, systems.Where(s => s.HasDkeys()).ToArray(), "Decrypted KEYS", outDir, useSubfolders ? "dkey" : null);
            await rc.DownloadPacks(Constants.PackGdiUrl, systems.Where(s => s.HasGdi()).ToArray(), "GDIs", outDir, useSubfolders ? "gdi" : null);
            await rc.DownloadPacks(Constants.PackKeysUrl, systems.Where(s => s.HasKeys()).ToArray(), "KEYS", outDir, useSubfolders ? "keys" : null);
            await rc.DownloadPacks(Constants.PackLsdUrl, systems.Where(s => s.HasLsd()).ToArray(), "LSD", outDir, useSubfolders ? "lsd" : null);
            await rc.DownloadPacks(Constants.PackSbiUrl, systems.Where(s => s.HasSbi()).ToArray(), "SBIs", outDir, useSubfolders ? "sbi" : null);

            return true;
        }

        /// <summary>
        /// Download premade packs for an individual system
        /// </summary>
        /// <param name="rc">RedumpClient for connectivity</param>
        /// <param name="system">RedumpSystem to get all possible packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadPacksForSystem(RedumpClient rc, RedumpSystem? system, string? outDir, bool useSubfolders)
        {
            var systemAsArray = new RedumpSystem?[] { system };

            if (system.HasCues())
                await rc.DownloadPacks(Constants.PackCuesUrl, systemAsArray, "CUEs", outDir, useSubfolders ? "cue" : null);

            if (system.HasDat())
                await rc.DownloadPacks(Constants.PackDatfileUrl, systemAsArray, "DATs", outDir, useSubfolders ? "dat" : null);

            if (system.HasDkeys())
                await rc.DownloadPacks(Constants.PackDkeysUrl, systemAsArray, "Decrypted KEYS", outDir, useSubfolders ? "dkey" : null);

            if (system.HasGdi())
                await rc.DownloadPacks(Constants.PackGdiUrl, systemAsArray, "GDIs", outDir, useSubfolders ? "gdi" : null);

            if (system.HasKeys())
                await rc.DownloadPacks(Constants.PackKeysUrl, systemAsArray, "KEYS", outDir, useSubfolders ? "keys" : null);

            if (system.HasLsd())
                await rc.DownloadPacks(Constants.PackLsdUrl, systemAsArray, "LSD", outDir, useSubfolders ? "lsd" : null);

            if (system.HasSbi())
                await rc.DownloadPacks(Constants.PackSbiUrl, systemAsArray, "SBIs", outDir, useSubfolders ? "sbi" : null);

            return true;
        }
    }
}