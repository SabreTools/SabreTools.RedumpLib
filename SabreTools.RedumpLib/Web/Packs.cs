using System;
using System.Threading.Tasks;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.Web
{
    /// <summary>
    /// Contains logic for dealing with packs
    /// </summary>
    public static class Packs
    {
        /// <summary>
        /// Download premade packs
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadPacks(this RedumpClient client, string? outDir, bool useSubfolders)
        {
            var systems = (RedumpSystem[])Enum.GetValues(typeof(RedumpSystem));

            await client.DownloadPacks(Constants.PackCuesUrl, Array.FindAll(systems, s => s.HasCues()), "CUEs", outDir, useSubfolders ? "cue" : null);
            await client.DownloadPacks(Constants.PackDatfileUrl, Array.FindAll(systems, s => s.HasDat()), "DATs", outDir, useSubfolders ? "dat" : null);
            await client.DownloadPacks(Constants.PackDkeysUrl, Array.FindAll(systems, s => s.HasDkeys()), "Decrypted KEYS", outDir, useSubfolders ? "dkey" : null);
            await client.DownloadPacks(Constants.PackGdiUrl, Array.FindAll(systems, s => s.HasGdi()), "GDIs", outDir, useSubfolders ? "gdi" : null);
            await client.DownloadPacks(Constants.PackKeysUrl, Array.FindAll(systems, s => s.HasKeys()), "KEYS", outDir, useSubfolders ? "keys" : null);
            await client.DownloadPacks(Constants.PackLsdUrl, Array.FindAll(systems, s => s.HasLsd()), "LSD", outDir, useSubfolders ? "lsd" : null);
            await client.DownloadPacks(Constants.PackSbiUrl, Array.FindAll(systems, s => s.HasSbi()), "SBIs", outDir, useSubfolders ? "sbi" : null);

            return true;
        }

        /// <summary>
        /// Download premade packs for an individual system
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="system">RedumpSystem to get all possible packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadPacksForSystem(this RedumpClient client, RedumpSystem? system, string? outDir, bool useSubfolders)
        {
            if (system is null)
                return false;

            var systemAsArray = new RedumpSystem[] { system.Value };

            if (system.HasCues())
                await client.DownloadPacks(Constants.PackCuesUrl, systemAsArray, "CUEs", outDir, useSubfolders ? "cue" : null);

            if (system.HasDat())
                await client.DownloadPacks(Constants.PackDatfileUrl, systemAsArray, "DATs", outDir, useSubfolders ? "dat" : null);

            if (system.HasDkeys())
                await client.DownloadPacks(Constants.PackDkeysUrl, systemAsArray, "Decrypted KEYS", outDir, useSubfolders ? "dkey" : null);

            if (system.HasGdi())
                await client.DownloadPacks(Constants.PackGdiUrl, systemAsArray, "GDIs", outDir, useSubfolders ? "gdi" : null);

            if (system.HasKeys())
                await client.DownloadPacks(Constants.PackKeysUrl, systemAsArray, "KEYS", outDir, useSubfolders ? "keys" : null);

            if (system.HasLsd())
                await client.DownloadPacks(Constants.PackLsdUrl, systemAsArray, "LSD", outDir, useSubfolders ? "lsd" : null);

            if (system.HasSbi())
                await client.DownloadPacks(Constants.PackSbiUrl, systemAsArray, "SBIs", outDir, useSubfolders ? "sbi" : null);

            return true;
        }
    }
}
