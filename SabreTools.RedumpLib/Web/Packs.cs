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

            Console.WriteLine("Downloading CUEs");
            await client.DownloadPacks(PackType.Cuesheets, systems, outDir, useSubfolders ? "cue" : null);

            Console.WriteLine("Downloading DATs");
            await client.DownloadPacks(PackType.Datfile, systems, outDir, useSubfolders ? "dat" : null);

            Console.WriteLine("Downloading Decrypted KEYS");
            await client.DownloadPacks(PackType.DecryptedKeys, systems, outDir, useSubfolders ? "dkey" : null);

            Console.WriteLine("Downloading GDIs");
            await client.DownloadPacks(PackType.Gdis, systems, outDir, useSubfolders ? "gdi" : null);

            Console.WriteLine("Downloading KEYS");
            await client.DownloadPacks(PackType.Keys, systems, outDir, useSubfolders ? "keys" : null);

            Console.WriteLine("Downloading LSD");
            await client.DownloadPacks(PackType.Lsds, systems, outDir, useSubfolders ? "lsd" : null);

            Console.WriteLine("Downloading SBIs");
            await client.DownloadPacks(PackType.Sbis, systems, outDir, useSubfolders ? "sbi" : null);

            return true;
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

            var systemAsArray = new RedumpSystem[] { system.Value };

            if (system.HasCues())
            {
                Console.WriteLine("Downloading CUEs");
                await client.DownloadPacks(PackType.Cuesheets, systemAsArray, outDir, useSubfolders ? "cue" : null);
            }

            if (system.HasDat())
            {
                Console.WriteLine("Downloading DATs");
                await client.DownloadPacks(PackType.Datfile, systemAsArray, outDir, useSubfolders ? "dat" : null);
            }

            if (system.HasDkeys())
            {
                Console.WriteLine("Downloading Decrypted KEYS");
                await client.DownloadPacks(PackType.DecryptedKeys, systemAsArray, outDir, useSubfolders ? "dkey" : null);
            }

            if (system.HasGdi())
            {
                Console.WriteLine("Downloading GDIs");
                await client.DownloadPacks(PackType.Gdis, systemAsArray, outDir, useSubfolders ? "gdi" : null);
            }

            if (system.HasKeys())
            {
                Console.WriteLine("Downloading KEYS");
                await client.DownloadPacks(PackType.Keys, systemAsArray, outDir, useSubfolders ? "keys" : null);
            }

            if (system.HasLsd())
            {
                Console.WriteLine("Downloading LSD");
                await client.DownloadPacks(PackType.Lsds, systemAsArray, outDir, useSubfolders ? "lsd" : null);
            }

            if (system.HasSbi())
            {
                Console.WriteLine("Downloading SBIs");
                await client.DownloadPacks(PackType.Sbis, systemAsArray, outDir, useSubfolders ? "sbi" : null);
            }

            return true;
        }
    }
}
