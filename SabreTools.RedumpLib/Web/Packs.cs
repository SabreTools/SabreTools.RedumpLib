using System;
#if NET20 || NET35
using System.Collections.Generic;
#endif
#if NET40_OR_GREATER || NETCOREAPP
using System.Linq;
#endif
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
#if NET20 || NET35
            var systems = new List<RedumpSystem?>();
            foreach (RedumpSystem s in Enum.GetValues(typeof(RedumpSystem)))
            {
                systems.Add(new Nullable<RedumpSystem>(s));
            }
#elif NET40_OR_GREATER || NETCOREAPP3_1
            var systems = Enum.GetValues(typeof(RedumpSystem))
                .OfType<RedumpSystem>()
                .Select(s => new Nullable<RedumpSystem>(s));
#else
            var systems = Enum.GetValues<RedumpSystem>().Select(s => new RedumpSystem?(s));
#endif

#if NET20 || NET35
            var filtered = new List<RedumpSystem?>();
            foreach (var s in systems)
            {
                if (s.HasCues())
                    filtered.Add(s);
            }
            await rc.DownloadPacks(Constants.PackCuesUrl, filtered.ToArray(), "CUEs", outDir, useSubfolders ? "cue" : null);

            filtered = new List<RedumpSystem?>();
            foreach (var s in systems)
            {
                if (s.HasDat())
                    filtered.Add(s);
            }
            await rc.DownloadPacks(Constants.PackDatfileUrl, filtered.ToArray(), "DATs", outDir, useSubfolders ? "dat" : null);

            filtered = new List<RedumpSystem?>();
            foreach (var s in systems)
            {
                if (s.HasCues())
                    filtered.Add(s);
            }

            filtered = new List<RedumpSystem?>();
            foreach (var s in systems)
            {
                if (s.HasDkeys())
                    filtered.Add(s);
            }
            await rc.DownloadPacks(Constants.PackDkeysUrl, filtered.ToArray(), "Decrypted KEYS", outDir, useSubfolders ? "dkey" : null);

            filtered = new List<RedumpSystem?>();
            foreach (var s in systems)
            {
                if (s.HasGdi())
                    filtered.Add(s);
            }
            await rc.DownloadPacks(Constants.PackGdiUrl, filtered.ToArray(), "GDIs", outDir, useSubfolders ? "gdi" : null);

            filtered = new List<RedumpSystem?>();
            foreach (var s in systems)
            {
                if (s.HasKeys())
                    filtered.Add(s);
            }
            await rc.DownloadPacks(Constants.PackKeysUrl, filtered.ToArray(), "KEYS", outDir, useSubfolders ? "keys" : null);

            filtered = new List<RedumpSystem?>();
            foreach (var s in systems)
            {
                if (s.HasLsd())
                    filtered.Add(s);
            }
            await rc.DownloadPacks(Constants.PackLsdUrl, filtered.ToArray(), "LSD", outDir, useSubfolders ? "lsd" : null);

            filtered = new List<RedumpSystem?>();
            foreach (var s in systems)
            {
                if (s.HasSbi())
                    filtered.Add(s);
            }
            await rc.DownloadPacks(Constants.PackSbiUrl, filtered.ToArray(), "SBIs", outDir, useSubfolders ? "sbi" : null);
#else
            await rc.DownloadPacks(Constants.PackCuesUrl, systems.Where(s => s.HasCues()).ToArray(), "CUEs", outDir, useSubfolders ? "cue" : null);
            await rc.DownloadPacks(Constants.PackDatfileUrl, systems.Where(s => s.HasDat()).ToArray(), "DATs", outDir, useSubfolders ? "dat" : null);
            await rc.DownloadPacks(Constants.PackDkeysUrl, systems.Where(s => s.HasDkeys()).ToArray(), "Decrypted KEYS", outDir, useSubfolders ? "dkey" : null);
            await rc.DownloadPacks(Constants.PackGdiUrl, systems.Where(s => s.HasGdi()).ToArray(), "GDIs", outDir, useSubfolders ? "gdi" : null);
            await rc.DownloadPacks(Constants.PackKeysUrl, systems.Where(s => s.HasKeys()).ToArray(), "KEYS", outDir, useSubfolders ? "keys" : null);
            await rc.DownloadPacks(Constants.PackLsdUrl, systems.Where(s => s.HasLsd()).ToArray(), "LSD", outDir, useSubfolders ? "lsd" : null);
            await rc.DownloadPacks(Constants.PackSbiUrl, systems.Where(s => s.HasSbi()).ToArray(), "SBIs", outDir, useSubfolders ? "sbi" : null);
#endif

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