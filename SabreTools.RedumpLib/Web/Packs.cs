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
        /// Download premade packs for all available systems
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadAllPacks(this RedumpClient client, string? outDir, bool useSubfolders)
        {
            var systems = (RedumpSystem[])Enum.GetValues(typeof(RedumpSystem));
            return await client.DownloadPacksForSystems(systems, outDir, useSubfolders);
        }

        /// <summary>
        /// Download premade packs for an individual system
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="system">RedumpSystem to get all possible packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadPacksForSystem(this RedumpClient client,
            RedumpSystem? system,
            string? outDir,
            bool useSubfolders)
        {
            if (system is null)
                return false;

            var systems = new RedumpSystem[] { system.Value };
            return await client.DownloadPacksForSystems(systems, outDir, useSubfolders);
        }

        /// <summary>
        /// Download premade packs for all provided systems
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="systems">Systems to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadPacksForSystems(this RedumpClient client,
            RedumpSystem[] systems,
            string? outDir,
            bool useSubfolders)
        {
            Console.WriteLine("Downloading CUEs");
            _ = await client.DownloadPacks(PackType.Cuesheets, systems, outDir, useSubfolders ? "cue" : null);

            Console.WriteLine("Downloading DATs");
            _ = await client.DownloadPacks(PackType.Datfile, systems, outDir, useSubfolders ? "dat" : null);

            Console.WriteLine("Downloading Decrypted KEYS");
            _ = await client.DownloadPacks(PackType.DecryptedKeys, systems, outDir, useSubfolders ? "dkey" : null);

            Console.WriteLine("Downloading GDIs");
            _ = await client.DownloadPacks(PackType.Gdis, systems, outDir, useSubfolders ? "gdi" : null);

            Console.WriteLine("Downloading KEYS");
            _ = await client.DownloadPacks(PackType.Keys, systems, outDir, useSubfolders ? "keys" : null);

            Console.WriteLine("Downloading LSD");
            _ = await client.DownloadPacks(PackType.Lsds, systems, outDir, useSubfolders ? "lsd" : null);

            Console.WriteLine("Downloading SBIs");
            _ = await client.DownloadPacks(PackType.Sbis, systems, outDir, useSubfolders ? "sbi" : null);

            return true;
        }
    }
}
