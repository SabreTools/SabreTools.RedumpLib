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
        /// <param name="includeDatabase">Include database in downloads</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadAllPacks(this Client client,
            string? outDir,
            bool includeDatabase,
            bool useSubfolders)
        {
            var systems = (PhysicalSystem[])Enum.GetValues(typeof(PhysicalSystem));
            return await client.DownloadPacksForSystems(systems, outDir, includeDatabase, useSubfolders);
        }

        /// <summary>
        /// Download premade packs for an individual system
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="system">PhysicalSystem to get all possible packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="includeDatabase">Include database in downloads</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadPacksForSystem(this Client client,
            PhysicalSystem? system,
            string? outDir,
            bool includeDatabase,
            bool useSubfolders)
        {
            if (system is null)
                return false;

            var systems = new PhysicalSystem[] { system };
            return await client.DownloadPacksForSystems(systems, outDir, includeDatabase, useSubfolders);
        }

        /// <summary>
        /// Download premade packs for all provided systems
        /// </summary>
        /// <param name="client">RedumpClient for connectivity</param>
        /// <param name="systems">Systems to download packs for</param>
        /// <param name="outDir">Output directory to save data to</param>
        /// <param name="includeDatabase">Include database in downloads</param>
        /// <param name="useSubfolders">True to use named subfolders to store downloads, false to store directly in the output directory</param>
        public static async Task<bool> DownloadPacksForSystems(this Client client,
            PhysicalSystem[] systems,
            string? outDir,
            bool includeDatabase,
            bool useSubfolders)
        {
            Console.WriteLine("Downloading CUEs");
            _ = await client.DownloadPacks(PackType.Cuesheets, systems, outDir, useSubfolders ? "cue" : null);

            Console.WriteLine("Downloading DATs");
            _ = await client.DownloadPacks(PackType.Datfile, systems, outDir, useSubfolders ? "dat" : null);

            Console.WriteLine("Downloading SBIs");
            _ = await client.DownloadPacks(PackType.Sbis, systems, outDir, useSubfolders ? "sbi" : null);

            if (includeDatabase)
            {
                Console.WriteLine("Downloading database export");
                _ = await client.DownloadDatabase(outDir, useSubfolders ? "db" : null);
            }

            return true;
        }
    }
}
