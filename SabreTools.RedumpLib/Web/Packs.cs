using System;
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
            var systems = (RedumpSystem?[])Enum.GetValues(typeof(RedumpSystem));

            await rc.DownloadPacks(Constants.PackCuesUrl, Array.FindAll(systems, s => s.HasCues()), "CUEs", outDir, useSubfolders ? "cue" : null);
            await rc.DownloadPacks(Constants.PackDatfileUrl, Array.FindAll(systems, s => s.HasDat()), "DATs", outDir, useSubfolders ? "dat" : null);
            await rc.DownloadPacks(Constants.PackDkeysUrl, Array.FindAll(systems, s => s.HasDkeys()), "Decrypted KEYS", outDir, useSubfolders ? "dkey" : null);
            await rc.DownloadPacks(Constants.PackGdiUrl, Array.FindAll(systems, s => s.HasGdi()), "GDIs", outDir, useSubfolders ? "gdi" : null);
            await rc.DownloadPacks(Constants.PackKeysUrl, Array.FindAll(systems, s => s.HasKeys()), "KEYS", outDir, useSubfolders ? "keys" : null);
            await rc.DownloadPacks(Constants.PackLsdUrl, Array.FindAll(systems, s => s.HasLsd()), "LSD", outDir, useSubfolders ? "lsd" : null);
            await rc.DownloadPacks(Constants.PackSbiUrl, Array.FindAll(systems, s => s.HasSbi()), "SBIs", outDir, useSubfolders ? "sbi" : null);

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