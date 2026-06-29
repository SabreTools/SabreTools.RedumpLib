using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Information pertaining to Redump systems
    /// </summary>
    /// TODO: Remove all references to redump.org submission information
    public static class Extensions
    {
        #region Non-Enumerable

        /// <summary>
        /// Extract the size from XML hash data
        /// </summary>
        /// <param name="hashData">String representing the combined hash data</param>
        /// <returns>Extracted size on success, -1 on error</returns>
        public static long ExtractSizeFromHashData(string? hashData)
        {
            if (string.IsNullOrEmpty(hashData))
                return -1;

            var hashreg = new Regex(@"<rom name="".*?"" size=""(.*?)"" crc=""(.*?)"" md5=""(.*?)"" sha1=""(.*?)""", RegexOptions.Compiled);
            Match m = hashreg.Match(hashData);
            if (m.Success)
            {
                if (long.TryParse(m.Groups[1].Value, out long size))
                    return size;
            }

            // Everything else is a failure case
            return -1;
        }

        /// <summary>
        /// Adjust the disc type based on size and layerbreak information
        /// </summary>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <returns>Corrected disc type, if possible</returns>
        public static void NormalizeDiscType(this SubmissionInfo info)
        {
            // If we have nothing valid, do nothing
            if (info.DiscIdentity.Media is null)
                return;

            // Some systems have limitations on media type when there's ambiguity
            var system = info.DiscIdentity.System;

#pragma warning disable IDE0010
            switch (info.DiscIdentity.Media)
            {
                case MediaType.DVD5:
                case MediaType.DVD9:
                    // 2-layer
                    if (info.DiscIdentifiers.Layerbreak != default)
                        info.DiscIdentity.Media = MediaType.DVD9;

                    // 1-layer
                    else
                        info.DiscIdentity.Media = MediaType.DVD5;

                    break;

                case MediaType.BD25:
                case MediaType.BD33:
                case MediaType.BD50:
                case MediaType.BD66:
                case MediaType.BD100:
                case MediaType.BD128:
                    // Extract the size from the hashes
                    long size = ExtractSizeFromHashData(info.DumpMetadata.Dat);

                    // 4-layer
                    if (info.DiscIdentifiers.Layerbreak3 != default)
                        info.DiscIdentity.Media = MediaType.BD128;

                    // 3-layer
                    else if (info.DiscIdentifiers.Layerbreak2 != default)
                        info.DiscIdentity.Media = MediaType.BD100;

                    // 2-layer
                    else if (info.DiscIdentifiers.Layerbreak != default && info.DumpMetadata.PICIdentifier == "BDU")
                        info.DiscIdentity.Media = MediaType.BD66;
                    else if (info.DiscIdentifiers.Layerbreak != default && size > 50_050_629_632)
                        info.DiscIdentity.Media = MediaType.BD66;
                    else if (info.DiscIdentifiers.Layerbreak != default && system == PhysicalSystem.SonyPlayStation5)
                        info.DiscIdentity.Media = MediaType.BD66;
                    else if (info.DiscIdentifiers.Layerbreak != default)
                        info.DiscIdentity.Media = MediaType.BD50;

                    // 1-layer
                    else if (info.DumpMetadata.PICIdentifier == "BDU")
                        info.DiscIdentity.Media = MediaType.BD33;
                    else if (size > 25_025_314_816)
                        info.DiscIdentity.Media = MediaType.BD33;
                    else if (system == PhysicalSystem.SonyPlayStation5)
                        info.DiscIdentity.Media = MediaType.BD33;
                    else
                        info.DiscIdentity.Media = MediaType.BD25;
                    break;

                case MediaType.HDDVDSL:
                case MediaType.HDDVDDL:
                    // 2-layer
                    if (info.DiscIdentifiers.Layerbreak != default)
                        info.DiscIdentity.Media = MediaType.HDDVDDL;

                    // 1-layer
                    else
                        info.DiscIdentity.Media = MediaType.HDDVDSL;
                    break;

                case MediaType.UMDSL:
                case MediaType.UMDDL:
                    // 2-layer
                    if (info.DiscIdentifiers.Layerbreak != default)
                        info.DiscIdentity.Media = MediaType.UMDDL;

                    // 1-layer
                    else
                        info.DiscIdentity.Media = MediaType.UMDSL;

                    break;

                // All other disc types are not processed
                default:
                    break;
            }
#pragma warning restore IDE0010
        }

        /// <summary>
        /// Adjust the disc type based on size and layerbreak information
        /// </summary>
        /// <param name="info">Existing SubmissionInfo object to fill</param>
        /// <returns>Corrected disc type, if possible</returns>
        public static void NormalizeDiscType(this RedumpOrg.SubmissionInfo info)
        {
            // If we have nothing valid, do nothing
            if (info.CommonDiscInfo.Media is null || info.SizeAndChecksums == default)
                return;

#pragma warning disable IDE0010
            switch (info.CommonDiscInfo.Media)
            {
                case MediaType.DVD5:
                case MediaType.DVD9:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.DVD9;
                    else
                        info.CommonDiscInfo.Media = MediaType.DVD5;
                    break;

                case MediaType.BD25:
                case MediaType.BD33:
                case MediaType.BD50:
                case MediaType.BD66:
                case MediaType.BD100:
                case MediaType.BD128:
                    // Extract the size from the hashes
                    long size = ExtractSizeFromHashData(info.TracksAndWriteOffsets.ClrMameProData);

                    if (info.SizeAndChecksums.Layerbreak3 != default)
                        info.CommonDiscInfo.Media = MediaType.BD128;
                    else if (info.SizeAndChecksums.Layerbreak2 != default)
                        info.CommonDiscInfo.Media = MediaType.BD100;
                    else if (info.SizeAndChecksums.Layerbreak != default && info.SizeAndChecksums.PICIdentifier == "BDU")
                        info.CommonDiscInfo.Media = MediaType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default && size > 50_050_629_632)
                        info.CommonDiscInfo.Media = MediaType.BD66;
                    else if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.BD50;
                    else if (info.SizeAndChecksums.PICIdentifier == "BDU")
                        info.CommonDiscInfo.Media = MediaType.BD33;
                    else if (size > 25_025_314_816)
                        info.CommonDiscInfo.Media = MediaType.BD33;
                    else
                        info.CommonDiscInfo.Media = MediaType.BD25;
                    break;

                case MediaType.HDDVDSL:
                case MediaType.HDDVDDL:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.HDDVDDL;
                    else
                        info.CommonDiscInfo.Media = MediaType.HDDVDSL;
                    break;

                case MediaType.UMDSL:
                case MediaType.UMDDL:
                    if (info.SizeAndChecksums.Layerbreak != default)
                        info.CommonDiscInfo.Media = MediaType.UMDDL;
                    else
                        info.CommonDiscInfo.Media = MediaType.UMDSL;
                    break;

                // All other disc types are not processed
                default:
                    break;
            }
#pragma warning restore IDE0010
        }

        #endregion

        #region Cross-Enumeration

        /// <summary>
        /// Get a list of valid MediaTypes for a given PhysicalSystem
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>MediaTypes, if possible</returns>
        public static List<PhysicalMediaType?> MediaTypes(this PhysicalSystem? system)
        {
            var types = new List<PhysicalMediaType?>();

            switch (system)
            {
                #region Consoles

                // https://en.wikipedia.org/wiki/Apple_Bandai_Pippin
                case PhysicalSystem.AppleBandaiPippin:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Atari_Jaguar_CD
                case PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Playdia
                case PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Amiga_CD32
                case PhysicalSystem.CommodoreAmigaCD32:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Commodore_CDTV
                case PhysicalSystem.CommodoreAmigaCDTV:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/EVO_Smart_Console
                case PhysicalSystem.EnvizionsEVOSmartConsole:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/FM_Towns_Marty
                case PhysicalSystem.FujitsuFMTownsMarty:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // http://videogamekraken.com/ion-educational-gaming-system-by-hasbro
                case PhysicalSystem.HasbroiONEducationalGamingSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case PhysicalSystem.HasbroVideoNow:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case PhysicalSystem.HasbroVideoNowColor:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case PhysicalSystem.HasbroVideoNowJr:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case PhysicalSystem.HasbroVideoNowXP:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                case PhysicalSystem.MattelFisherPriceiXL:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/HyperScan
                case PhysicalSystem.MattelHyperScan:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_(console)
                case PhysicalSystem.MicrosoftXbox:
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_360
                case PhysicalSystem.MicrosoftXbox360:
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_One
                case PhysicalSystem.MicrosoftXboxOne:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_Series_X_and_Series_S
                case PhysicalSystem.MicrosoftXboxSeriesXS:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/TurboGrafx-16
                case PhysicalSystem.NECPCEngineCDTurboGrafxCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PC-FX
                case PhysicalSystem.NECPCFXPCFXGA:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/GameCube
                case PhysicalSystem.NintendoGameCube:
                    types.Add(PhysicalMediaType.DVD); // Only added here to help users; not strictly correct
                    types.Add(PhysicalMediaType.NintendoGameCubeGameDisc);
                    break;

                // https://en.wikipedia.org/wiki/Super_NES_CD-ROM
                case PhysicalSystem.NintendoSonySuperNESCDROMSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Wii
                case PhysicalSystem.NintendoWii:
                    types.Add(PhysicalMediaType.DVD); // Only added here to help users; not strictly correct
                    types.Add(PhysicalMediaType.NintendoWiiOpticalDisc);
                    break;

                // https://en.wikipedia.org/wiki/Wii_U
                case PhysicalSystem.NintendoWiiU:
                    types.Add(PhysicalMediaType.NintendoWiiUOpticalDisc);
                    break;

                // https://en.wikipedia.org/wiki/3DO_Interactive_Multiplayer
                case PhysicalSystem.Panasonic3DOInteractiveMultiplayer:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Philips_CD-i
                case PhysicalSystem.PhilipsCDi:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Polymega
                case PhysicalSystem.PlaymajiPolymega:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/LaserActive
                case PhysicalSystem.PioneerLaserActive:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.LaserDisc);
                    break;

                // https://en.wikipedia.org/wiki/Sega_CD
                case PhysicalSystem.SegaMegaCDSegaCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Dreamcast
                case PhysicalSystem.SegaDreamcast:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition, MIL-CD
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // https://en.wikipedia.org/wiki/Sega_Saturn
                case PhysicalSystem.SegaSaturn:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Neo_Geo_CD
                case PhysicalSystem.SNKNeoGeoCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_(console)
                case PhysicalSystem.SonyPlayStation:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_2
                case PhysicalSystem.SonyPlayStation2:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_3
                case PhysicalSystem.SonyPlayStation3:
                    types.Add(PhysicalMediaType.BluRay);
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_4
                case PhysicalSystem.SonyPlayStation4:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_5
                case PhysicalSystem.SonyPlayStation5:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_Portable
                case PhysicalSystem.SonyPlayStationPortable:
                    types.Add(PhysicalMediaType.UMD);
                    types.Add(PhysicalMediaType.CDROM); // Development discs only
                    types.Add(PhysicalMediaType.DVD); // Development discs only
                    break;

                // https://en.wikipedia.org/wiki/Tandy_Video_Information_System
                case PhysicalSystem.MemorexVisualInformationSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Nuon_(DVD_technology)
                case PhysicalSystem.VMLabsNUON:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/V.Flash
                case PhysicalSystem.VTechVFlashVSmilePro:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Game_Wave_Family_Entertainment_System
                case PhysicalSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem:
                    types.Add(PhysicalMediaType.CDROM); // Firmware discs only(?)
                    types.Add(PhysicalMediaType.DVD);
                    break;

                #endregion

                #region Computers

                // https://en.wikipedia.org/wiki/Acorn_Archimedes
                case PhysicalSystem.AcornArchimedesAndRiscPC:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/Macintosh
                case PhysicalSystem.AppleMacintosh:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    types.Add(PhysicalMediaType.HardDisk);
                    break;

                // https://en.wikipedia.org/wiki/Atari_ST
                case PhysicalSystem.AtariSTSeries:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Commodore_64
                case PhysicalSystem.Commodore64:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Amiga
                case PhysicalSystem.CommodoreAmigaCD:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/FM_Towns
                case PhysicalSystem.FujitsuFMTownsSeries:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/IBM_PC_compatible
                case PhysicalSystem.IBMPCcompatible:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.BluRay);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    types.Add(PhysicalMediaType.HardDisk);
                    types.Add(PhysicalMediaType.DataCartridge);
                    break;

                // https://en.wikipedia.org/wiki/MSX
                case PhysicalSystem.MicrosoftMSX:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PC-8800_series
                case PhysicalSystem.NECPC88Series:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/PC-9800_series
                case PhysicalSystem.NECPC98Series:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/X68000
                case PhysicalSystem.SharpX68000:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/ZX_Spectrum
                case PhysicalSystem.SinclairZXSpectrum:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Sun_Microsystems
                case PhysicalSystem.SunMicrosystemsUltra:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                #endregion

                #region Arcade

                // https://en.wikipedia.org/wiki/Orbatak
                case PhysicalSystem.AmericanLaserGames3DO:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=779
                case PhysicalSystem.Atari3DO:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://newlifegames.net/nlg/index.php?topic=22003.0
                // http://newlifegames.net/nlg/index.php?topic=5486.msg119440
                case PhysicalSystem.Atronic:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://www.arcade-museum.com/members/member_detail.php?member_id=406530
                case PhysicalSystem.AUSCOMSystem1:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://newlifegames.net/nlg/index.php?topic=285.0
                case PhysicalSystem.BallyGameMagic:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/CP_System_III
                case PhysicalSystem.CapcomPlaySystemIII:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://www.bigbookofamigahardware.com/bboah/product.aspx?id=36
                // https://amiga.resource.cx/exp/cubo
                case PhysicalSystem.CDExpressCuboCD32:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.FunworldPhotoPlay:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/FuRyu
                case PhysicalSystem.FuRyuOmronPurikura:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // UNKNOWN
                case PhysicalSystem.GlobalVRVarious:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://service.globalvr.com/troubleshooting/vortek.html
                case PhysicalSystem.GlobalVRVortek:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://service.globalvr.com/downloads/v3/040-1001-01c-V3-System-Manual.pdf
                case PhysicalSystem.GlobalVRVortekV3:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://www.icegame.com/games
                case PhysicalSystem.ICEPCHardware:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://github.com/mamedev/mame/blob/master/src/mame/drivers/iteagle.cpp
                case PhysicalSystem.IncredibleTechnologiesEagle:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.IncredibleTechnologiesVarious:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // UNKNOWN
                case PhysicalSystem.JVLiTouch:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/E-Amusement
                case PhysicalSystem.KonamieAmusement:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=828
                case PhysicalSystem.KonamiFireBeat:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=577
                case PhysicalSystem.KonamiSystemGV:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=575
                case PhysicalSystem.KonamiM2:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=586
                // http://system16.com/hardware.php?id=977
                case PhysicalSystem.KonamiPython:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=976
                // http://system16.com/hardware.php?id=831
                case PhysicalSystem.KonamiPython2:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=582
                // http://system16.com/hardware.php?id=822
                // http://system16.com/hardware.php?id=823
                case PhysicalSystem.KonamiSystem573:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=827
                case PhysicalSystem.KonamiTwinkle:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.KonamiVarious:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://www.meritgames.com/Support_Center/manuals/PM0591-01.pdf
                case PhysicalSystem.MeritIndustriesBoardwalk:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://www.meritgames.com/Support_Center/Force%20Elite/PM0380-09.pdf
                // http://www.meritgames.com/Support_Center/Force%20Upright/PM0382-07%20FORCE%20Upright%20manual.pdf
                // http://www.meritgames.com/Support_Center/Force%20Upright/PM0383-07%20FORCE%20Upright%20manual.pdf
                case PhysicalSystem.MeritIndustriesMegaTouchForce:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://www.meritgames.com/Service%20Center/Ion%20Troubleshooting.pdf
                case PhysicalSystem.MeritIndustriesMegaTouchION:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://www.meritgames.com/Support_Center/EZ%20Maxx/Manuals/MAXX%20Elite%20with%20coin.pdf
                // http://www.meritgames.com/Support_Center/EZ%20Maxx/Manuals/MAXX%20Elite.pdf
                // http://www.meritgames.com/Support_Center/manuals/90003010%20Maxx%20TSM_Rev%20C.pdf
                case PhysicalSystem.MeritIndustriesMegaTouchMaxx:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://www.meritgames.com/Support_Center/manuals/pm0076_OA_Megatouch%20XL%20Trouble%20Shooting%20Manual.pdf
                // http://www.meritgames.com/Support_Center/MEGA%20XL/manuals/Megatouch_XL_pm0109-0D.pdf
                // http://www.meritgames.com/Support_Center/MEGA%20XL/manuals/Megatouch_XL_Super_5000_manual.pdf
                case PhysicalSystem.MeritIndustriesMegaTouchXL:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.NamcoPurikura:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=543
                case PhysicalSystem.NamcoSystem246:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=546
                // http://system16.com/hardware.php?id=872
                case PhysicalSystem.NamcoSystem256:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=545
                case PhysicalSystem.NamcoSegaNintendoTriforce:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // http://system16.com/hardware.php?id=535
                case PhysicalSystem.NamcoSystem12:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://system16.com/hardware.php?id=537
                case PhysicalSystem.NamcoSystem22:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://www.arcade-history.com/?n=the-yakyuuken-part-1&page=detail&id=33049
                case PhysicalSystem.NewJatreCDi:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://blog.system11.org/?p=2499
                case PhysicalSystem.NichibutsuHighRateSystem:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://blog.system11.org/?p=2514
                case PhysicalSystem.NichibutsuSuperCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://collectedit.com/collectors/shou-time-213/arcade-pcbs-281/x-rate-dvd-series-17-newlywed-life-japan-by-nichibutsu-32245
                case PhysicalSystem.NichibutsuXRateSystem:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/Panasonic_M2
                case PhysicalSystem.PanasonicM2:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // UNKNOWN
                case PhysicalSystem.PCBasedArcade:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://github.com/mamedev/mame/blob/master/src/mame/drivers/photoply.cpp
                case PhysicalSystem.PhotoPlayVarious:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.RawThrillsVarious:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // UNKNOWN
                case PhysicalSystem.SegaALLS:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=729
                case PhysicalSystem.SegaChihiro:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // http://system16.com/hardware.php?id=907
                case PhysicalSystem.SegaEuropaR:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=985
                // http://system16.com/hardware.php?id=731
                // http://system16.com/hardware.php?id=984
                // http://system16.com/hardware.php?id=986
                case PhysicalSystem.SegaLindbergh:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=721
                // http://system16.com/hardware.php?id=723
                // http://system16.com/hardware.php?id=906
                // http://system16.com/hardware.php?id=722
                case PhysicalSystem.SegaNaomi:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // http://system16.com/hardware.php?id=725
                // http://system16.com/hardware.php?id=726
                // http://system16.com/hardware.php?id=727
                case PhysicalSystem.SegaNaomi2:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // https://segaretro.org/Sega_NAOMI#NAOMI_Satellite_Terminal
                case PhysicalSystem.SegaNaomiSatelliteTerminalPC:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
                case PhysicalSystem.SegaNu:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
                case PhysicalSystem.SegaNu11:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
                case PhysicalSystem.SegaNu2:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://segaretro.org/Nu_SX
                case PhysicalSystem.SegaNuSX:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=910
                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
                case PhysicalSystem.SegaRingEdge:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=982
                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
                case PhysicalSystem.SegaRingEdge2:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=911
                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
                case PhysicalSystem.SegaRingWide:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=711
                case PhysicalSystem.SegaTitanVideo:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=709
                // http://system16.com/hardware.php?id=710
                case PhysicalSystem.SegaSystem32:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://github.com/mamedev/mame/blob/master/src/mame/drivers/seibucats.cpp
                case PhysicalSystem.SeibuCATSSystem:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://www.tab.at/en/support/support/downloads
                case PhysicalSystem.TABAustriaQuizard:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://primetimeamusements.com/product/tsumo-multi-game-motion-system/
                // https://www.highwaygames.com/arcade-machines/tsumo-tsunami-motion-8117/
                case PhysicalSystem.TsunamiTsuMoMultiGameMotionSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/UltraCade_Technologies
                case PhysicalSystem.UltraCade:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                #endregion

                #region Others

                // https://en.wikipedia.org/wiki/Audio_CD
                case PhysicalSystem.AudioCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Blu-ray#Player_profiles
                case PhysicalSystem.BDVideo:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // UNKNOWN
                case PhysicalSystem.DatelPlayStationCheatDeviceUpdates:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/DVD-Audio
                case PhysicalSystem.DVDAudio:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/DVD-Video
                case PhysicalSystem.DVDVideo:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/Blue_Book_(CD_standard)
                case PhysicalSystem.EnhancedCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/HD_DVD
                case PhysicalSystem.HDDVDVideo:
                    types.Add(PhysicalMediaType.HDDVD);
                    break;

                // UNKNOWN
                case PhysicalSystem.MicrosoftPocketPC:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/Compressed_audio_optical_disc
                case PhysicalSystem.MP3AudioDisc:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.NavisoftNaviken:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.PalmOS:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/Photo_CD
                case PhysicalSystem.PhotoCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.Psion:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Doors_and_Windows_(EP)
                case PhysicalSystem.RainbowDisc:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://segaretro.org/Prologue_21
                case PhysicalSystem.SegaPrologue21MultimediaKaraokeSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.SharpZaurus:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case PhysicalSystem.SonyElectronicBook:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Super_Audio_CD
                case PhysicalSystem.SuperAudioCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://www.cnet.com/products/tao-music-iktv-karaoke-station-karaoke-system-series/
                case PhysicalSystem.TaoiKTV:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://ultimateconsoledatabase.com/golden/kiss_site.htm
                case PhysicalSystem.TomyKissSite:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Video_CD
                case PhysicalSystem.VideoCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                #endregion

                // BIOS systems can't have a media type
                case PhysicalSystem.MicrosoftXboxBIOS:
                case PhysicalSystem.NintendoGameCubeBIOS:
                case PhysicalSystem.SonyPlayStationBIOS:
                case PhysicalSystem.SonyPlayStation2BIOS:
                    types.Add(PhysicalMediaType.NONE);
                    break;

                // Marker systems can't have a media type
                case PhysicalSystem.MarkerDiscBasedConsoleEnd:
                case PhysicalSystem.MarkerComputerEnd:
                case PhysicalSystem.MarkerArcadeEnd:
                case PhysicalSystem.MarkerOtherEnd:
                    types.Add(PhysicalMediaType.NONE);
                    break;

                case null:
                default:
                    types.Add(PhysicalMediaType.NONE);
                    break;
            }

            return types;
        }

        /// <summary>
        /// Convert master list of all media types to currently known Redump disc types
        /// </summary>
        /// <param name="mediaType">MediaType value to check</param>
        /// <returns>DiscType if possible, null on error</returns>
        public static MediaType? ToMediaType(this PhysicalMediaType? mediaType)
        {
            return mediaType switch
            {
                PhysicalMediaType.BluRay => MediaType.BD50,
                PhysicalMediaType.CDROM => MediaType.CD,
                PhysicalMediaType.DVD => MediaType.DVD9,
                PhysicalMediaType.GDROM => MediaType.GDROM,
                PhysicalMediaType.HDDVD => MediaType.HDDVDSL,
                // PhysicalMediaType.MILCD => MediaType.MILCD, // TODO: Support this?
                PhysicalMediaType.NintendoGameCubeGameDisc => MediaType.NintendoGameCubeGameDisc,
                PhysicalMediaType.NintendoWiiOpticalDisc => MediaType.NintendoWiiOpticalDiscDL,
                PhysicalMediaType.NintendoWiiUOpticalDisc => MediaType.NintendoWiiUOpticalDiscSL,
                PhysicalMediaType.UMD => MediaType.UMDDL,

                // Invalid cases for conversion
                PhysicalMediaType.NONE => null,
                PhysicalMediaType.ApertureCard => null,
                PhysicalMediaType.JacquardLoomCard => null,
                PhysicalMediaType.MagneticStripeCard => null,
                PhysicalMediaType.OpticalPhonecard => null,
                PhysicalMediaType.PunchedCard => null,
                PhysicalMediaType.PunchedTape => null,
                PhysicalMediaType.Cassette => null,
                PhysicalMediaType.DataCartridge => null,
                PhysicalMediaType.OpenReel => null,
                PhysicalMediaType.FloppyDisk => null,
                PhysicalMediaType.Floptical => null,
                PhysicalMediaType.HardDisk => null,
                PhysicalMediaType.IomegaBernoulliDisk => null,
                PhysicalMediaType.IomegaJaz => null,
                PhysicalMediaType.IomegaZip => null,
                PhysicalMediaType.LaserDisc => null,
                PhysicalMediaType.Nintendo64DD => null,
                PhysicalMediaType.NintendoFamicomDiskSystem => null,
                PhysicalMediaType.Cartridge => null,
                PhysicalMediaType.CED => null,
                PhysicalMediaType.CompactFlash => null,
                PhysicalMediaType.MMC => null,
                PhysicalMediaType.SDCard => null,
                PhysicalMediaType.FlashDrive => null,
                null => null,
                _ => null,
            };
        }

        /// <summary>
        /// Convert currently known Redump disc types to master list of all media types
        /// </summary>
        /// <param name="discType">DiscType value to check</param>
        /// <returns>MediaType if possible, null on error</returns>
        public static PhysicalMediaType? ToPhysicalMediaType(this MediaType? discType)
        {
            return discType switch
            {
                MediaType.BD25
                    or MediaType.BD33
                    or MediaType.BD50
                    or MediaType.BD66
                    or MediaType.BD100
                    or MediaType.BD128
                    or MediaType.MaxTest4Layer => PhysicalMediaType.BluRay,
                MediaType.CD => PhysicalMediaType.CDROM,
                MediaType.DVD5
                    or MediaType.DVD9 => PhysicalMediaType.DVD,
                MediaType.GDROM => PhysicalMediaType.GDROM,
                MediaType.HDDVDSL
                    or MediaType.HDDVDDL => PhysicalMediaType.HDDVD,
                // MediaType.MILCD => PhysicalMediaType.MILCD, // TODO: Support this?
                MediaType.NintendoGameCubeGameDisc => PhysicalMediaType.NintendoGameCubeGameDisc,
                MediaType.NintendoWiiOpticalDiscSL
                    or MediaType.NintendoWiiOpticalDiscDL => PhysicalMediaType.NintendoWiiOpticalDisc,
                MediaType.NintendoWiiUOpticalDiscSL => PhysicalMediaType.NintendoWiiUOpticalDisc,
                MediaType.UMDSL
                    or MediaType.UMDDL => PhysicalMediaType.UMD,

                // Invalid cases for conversion
                MediaType.NONE => null,
                MediaType.MILCD => null,
                null => null,
                _ => null,
            };
        }

        #endregion

        #region Disc Category

        /// <summary>
        /// Get the Redump longnames for each known category
        /// </summary>
        public static string? LongName(this DiscCategory category)
            => ((DiscCategory?)category).LongName();

        /// <summary>
        /// Get the Redump longnames for each known category
        /// </summary>
        public static string? LongName(this DiscCategory? category)
            => AttributeHelper<DiscCategory?>.GetHumanReadableAttribute(category)?.LongName;

        /// <summary>
        /// Get the Category enum value for a given string
        /// </summary>
        /// <param name="category">String value to convert</param>
        /// <returns>Category represented by the string, if possible</returns>
        public static DiscCategory? ToDiscCategory(this string? category)
        {
            // No value means no match
            if (category is null || category.Length == 0)
                return null;

            category = category?.ToLowerInvariant();
            var categories = (DiscCategory[])Enum.GetValues(typeof(DiscCategory));

            // Check long names
            int index = Array.FindIndex(categories, c => category == c.LongName()?.ToLowerInvariant()
                || category == c.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return categories[index];

            return null;
        }

        #endregion

        #region Disc Subpath

        /// <summary>
        /// Get the human readable name for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? LongName(this DiscSubpath discSubpath)
            => AttributeHelper<DiscSubpath>.GetHumanReadableAttribute(discSubpath)?.LongName;

        /// <summary>
        /// Get the human readable name for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? LongName(this DiscSubpath? discSubpath)
            => AttributeHelper<DiscSubpath?>.GetHumanReadableAttribute(discSubpath)?.LongName;

        /// <summary>
        /// Get the URL path part for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? ShortName(this DiscSubpath discSubpath)
            => AttributeHelper<DiscSubpath>.GetHumanReadableAttribute(discSubpath)?.ShortName;

        /// <summary>
        /// Get the URL path part for a DiscSubpath
        /// </summary>
        /// <param name="discSubpath"></param>
        /// <returns></returns>
        public static string? ShortName(this DiscSubpath? discSubpath)
            => AttributeHelper<DiscSubpath?>.GetHumanReadableAttribute(discSubpath)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="discSubpath">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static DiscSubpath? ToDiscSubpath(this string? discSubpath)
        {
            // No value means no match
            if (discSubpath is null || discSubpath.Length == 0)
                return null;

            discSubpath = discSubpath.ToLowerInvariant();
            var discSubpaths = (DiscSubpath[])Enum.GetValues(typeof(DiscSubpath));

            // Check short names
            int index = Array.FindIndex(discSubpaths, s => discSubpath == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return discSubpaths[index];

            // Check long names
            index = Array.FindIndex(discSubpaths, s => discSubpath == s.LongName()?.ToLowerInvariant()
                || discSubpath == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return discSubpaths[index];

            return null;
        }

        #endregion

        #region Dump Status

        /// <summary>
        /// Get the human readable name for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? LongName(this DumpStatus dumpStatus)
            => AttributeHelper<DumpStatus>.GetHumanReadableAttribute(dumpStatus)?.LongName;

        /// <summary>
        /// Get the human readable name for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? LongName(this DumpStatus? dumpStatus)
            => AttributeHelper<DumpStatus?>.GetHumanReadableAttribute(dumpStatus)?.LongName;

        /// <summary>
        /// Get the URL path part for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? ShortName(this DumpStatus dumpStatus)
            => AttributeHelper<DumpStatus>.GetHumanReadableAttribute(dumpStatus)?.ShortName;

        /// <summary>
        /// Get the URL path part for a DumpStatus
        /// </summary>
        /// <param name="dumpStatus"></param>
        /// <returns></returns>
        public static string? ShortName(this DumpStatus? dumpStatus)
            => AttributeHelper<DumpStatus?>.GetHumanReadableAttribute(dumpStatus)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="dumpStatus">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static DumpStatus? ToDumpStatus(this string? dumpStatus)
        {
            // No value means no match
            if (dumpStatus is null || dumpStatus.Length == 0)
                return null;

            dumpStatus = dumpStatus.ToLowerInvariant();
            var dumpStatuses = (DumpStatus[])Enum.GetValues(typeof(DumpStatus));

            // Check short names
            int index = Array.FindIndex(dumpStatuses, s => dumpStatus == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return dumpStatuses[index];

            // Check long names
            index = Array.FindIndex(dumpStatuses, s => dumpStatus == s.LongName()?.ToLowerInvariant()
                || dumpStatus == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return dumpStatuses[index];

            // Check numeric values
            if (int.TryParse(dumpStatus, out int dumpStatusInt) && Enum.IsDefined(typeof(DumpStatus), dumpStatusInt))
                return (DumpStatus)dumpStatusInt;

            return null;
        }

        #endregion

        #region Language

        /// <summary>
        /// Get the Redump longnames for each known language
        /// </summary>
        public static string? LongName(this Language language)
            => ((Language?)language).LongName();

        /// <summary>
        /// Get the Redump longnames for each known language
        /// </summary>
        public static string? LongName(this Language? language)
            => AttributeHelper<Language?>.GetHumanReadableAttribute(language)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known language
        /// </summary>
        public static string? ShortName(this Language language)
            => ((Language?)language).ShortName();

        /// <summary>
        /// Get the Redump shortnames for each known language
        /// </summary>
        public static string? ShortName(this Language? language)
        {
            // Some languages need to use the alternate code instead
#pragma warning disable IDE0072 // Add missing cases
            return language switch
            {
                Language.Albanian
                    or Language.Armenian
                    or Language.Icelandic
                    or Language.Macedonian
                    or Language.Romanian
                    or Language.Slovak => language.ThreeLetterCodeAlt(),
                _ => language.ThreeLetterCode(),
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Get the Language enum value for a given string
        /// </summary>
        /// <param name="lang">String value to convert</param>
        /// <returns>Language represented by the string, if possible</returns>
        public static Language? ToLanguage(this string? lang)
        {
            // No value means no match
            if (lang is null || lang.Length == 0)
                return null;

            lang = lang.ToLowerInvariant();
            var languages = (Language[])Enum.GetValues(typeof(Language));

            // Check ISO 639-1 codes
            int index = Array.FindIndex(languages, l => lang == l.TwoLetterCode());
            if (index > -1)
                return languages[index];

            // Check standard ISO 639-2 codes
            index = Array.FindIndex(languages, l => lang == l.ThreeLetterCode());
            if (index > -1)
                return languages[index];

            // Check alternate ISO 639-2 codes
            index = Array.FindIndex(languages, l => lang == l.ThreeLetterCodeAlt());
            if (index > -1)
                return languages[index];

            return null;
        }

        /// <summary>
        /// Get the ISO 639-2 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCode(this Language language)
            => (AttributeHelper<Language>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.ThreeLetterCode;

        /// <summary>
        /// Get the ISO 639-2 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCode(this Language? language)
            => (AttributeHelper<Language?>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.ThreeLetterCode;

        /// <summary>
        /// Get the ISO 639-2 alternate code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCodeAlt(this Language language)
            => (AttributeHelper<Language>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.ThreeLetterCodeAlt;

        /// <summary>
        /// Get the ISO 639-2 alternate code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCodeAlt(this Language? language)
            => (AttributeHelper<Language?>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.ThreeLetterCodeAlt;

        /// <summary>
        /// Get the ISO 639-1 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? TwoLetterCode(this Language language)
            => (AttributeHelper<Language>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.TwoLetterCode;

        /// <summary>
        /// Get the ISO 639-1 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? TwoLetterCode(this Language? language)
            => (AttributeHelper<Language?>.GetHumanReadableAttribute(language) as LanguageCodeAttribute)?.TwoLetterCode;

        #endregion

        #region Language Selection

        /// <summary>
        /// Get the string representation of the LanguageSelection enum values
        /// </summary>
        /// <param name="langSelect">LanguageSelection value to convert</param>
        /// <returns>String representing the value, if possible</returns>
        public static string? LongName(this LanguageSelection langSelect)
            => ((LanguageSelection?)langSelect).LongName();

        /// <summary>
        /// Get the string representation of the LanguageSelection enum values
        /// </summary>
        /// <param name="langSelect">LanguageSelection value to convert</param>
        /// <returns>String representing the value, if possible</returns>
        public static string? LongName(this LanguageSelection? langSelect)
            => AttributeHelper<LanguageSelection?>.GetHumanReadableAttribute(langSelect)?.LongName;

        /// <summary>
        /// Get the LanguageSelection enum value for a given string
        /// </summary>
        /// <param name="langSelect">String value to convert</param>
        /// <returns>LanguageSelection represented by the string, if possible</returns>
        public static LanguageSelection? ToLanguageSelection(this string? langSelect)
        {
            // No value means no match
            if (langSelect is null || langSelect.Length == 0)
                return null;

            langSelect = langSelect?.ToLowerInvariant();
            var selects = (LanguageSelection[])Enum.GetValues(typeof(LanguageSelection));

            // Check long names
            int index = Array.FindIndex(selects, l => langSelect == l.LongName()?.ToLowerInvariant()
                || langSelect == l.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return selects[index];

            return null;
        }

        #endregion

        #region Media Type

        /// <summary>
        /// Get the Redump longnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? LongName(this MediaType mediaType)
            => ((MediaType?)mediaType).LongName();

        /// <summary>
        /// Get the Redump longnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? LongName(this MediaType? mediaType)
            => AttributeHelper<MediaType?>.GetHumanReadableAttribute(mediaType)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this MediaType mediaType)
            => AttributeHelper<MediaType>.GetHumanReadableAttribute(mediaType)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this MediaType? mediaType)
            => AttributeHelper<MediaType?>.GetHumanReadableAttribute(mediaType)?.ShortName;

        /// <summary>
        /// Get the MediaType enum value for a given string
        /// </summary>
        /// <param name="mediaType">String value to convert</param>
        /// <returns>MediaType represented by the string, if possible</returns>
        public static MediaType? ToMediaType(this string? mediaType)
        {
            // No value means no match
            if (mediaType is null || mediaType.Length == 0)
                return null;

            mediaType = mediaType.ToLowerInvariant();
            var mediaTypes = (MediaType[])Enum.GetValues(typeof(MediaType));

            // Check short names
            int index = Array.FindIndex(mediaTypes, s => mediaType == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return mediaTypes[index];

            // Check long names
            index = Array.FindIndex(mediaTypes, s => mediaType == s.LongName()?.ToLowerInvariant()
                || mediaType == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant()
                || mediaType == s.LongName()?.Replace("-", string.Empty)?.ToLowerInvariant()
                || mediaType == s.LongName()?
                    .Replace(" ", string.Empty)?
                    .Replace("-", string.Empty)?
                    .ToLowerInvariant());
            if (index > -1)
                return mediaTypes[index];

            // Check numeric values
            if (int.TryParse(mediaType, out int mediaTypeInt) && Enum.IsDefined(typeof(MediaType), mediaTypeInt))
                return (MediaType)mediaTypeInt;

            // Special cases
            return (mediaType?.ToLowerInvariant()) switch
            {
                "bd"
                    or "bdrom"
                    or "bd-rom"
                    or "bluray"
                    or "blu-ray" => MediaType.BD25,
                "cdrom"
                    or "cd-rom" => MediaType.CD,
                "dvd"
                    or "dvd-rom" => MediaType.DVD5,
                "gc" => MediaType.NintendoGameCubeGameDisc,
                "gd" => MediaType.GDROM,
                "hddvd"
                    or "hd-dvd" => MediaType.HDDVDSL,
                "umd" => MediaType.UMDSL,
                "wii" => MediaType.NintendoWiiOpticalDiscSL,

                _ => null,
            };
        }

        #endregion

        #region Pack Type

        /// <summary>
        /// Get the human readable name for a PackType
        /// </summary>
        /// <param name="packType"></param>
        /// <returns></returns>
        public static string? LongName(this PackType packType)
            => AttributeHelper<PackType>.GetHumanReadableAttribute(packType)?.LongName;

        /// <summary>
        /// Get the human readable name for a PackType
        /// </summary>
        /// <param name="packType"></param>
        /// <returns></returns>
        public static string? LongName(this PackType? packType)
            => AttributeHelper<PackType?>.GetHumanReadableAttribute(packType)?.LongName;

        /// <summary>
        /// Get the URL path part for a PackType
        /// </summary>
        /// <param name="packType"></param>
        /// <returns></returns>
        public static string? ShortName(this PackType packType)
            => AttributeHelper<PackType>.GetHumanReadableAttribute(packType)?.ShortName;

        /// <summary>
        /// Get the URL path part for a PackType
        /// </summary>
        /// <param name="packType"></param>
        /// <returns></returns>
        public static string? ShortName(this PackType? packType)
            => AttributeHelper<PackType?>.GetHumanReadableAttribute(packType)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="packType">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static PackType? ToPackType(this string? packType)
        {
            // No value means no match
            if (packType is null || packType.Length == 0)
                return null;

            packType = packType.ToLowerInvariant();
            var packTypes = (PackType[])Enum.GetValues(typeof(PackType));

            // Check short names
            int index = Array.FindIndex(packTypes, s => packType == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return packTypes[index];

            // Check long names
            index = Array.FindIndex(packTypes, s => packType == s.LongName()?.ToLowerInvariant()
                || packType == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return packTypes[index];

            return null;
        }

        #endregion

        #region Physical Media Type

        /// <summary>
        /// List all media types with their short usable names
        /// </summary>
        public static List<string> ListMediaTypes()
        {
            var mediaTypes = new List<string>();

            foreach (var val in Enum.GetValues(typeof(PhysicalMediaType)))
            {
                if (val is null || ((PhysicalMediaType)val) == PhysicalMediaType.NONE)
                    continue;

                mediaTypes.Add($"{((PhysicalMediaType?)val).ShortName()} - {((PhysicalMediaType?)val).LongName()}");
            }

            return mediaTypes;
        }

        /// <summary>
        /// Get the longnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? LongName(this PhysicalMediaType? mediaType)
            => AttributeHelper<PhysicalMediaType?>.GetHumanReadableAttribute(mediaType)?.LongName;

        /// <summary>
        /// Get the shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this PhysicalMediaType? mediaType)
            => AttributeHelper<PhysicalMediaType?>.GetHumanReadableAttribute(mediaType)?.ShortName;

        #endregion

        #region Physical System

        /// <summary>
        /// Determine if a system is okay if it's not detected by Windows
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if Windows show see a disc when dumping, false otherwise</returns>
        public static bool DetectedByWindows(this PhysicalSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                // BIOS Sets
                PhysicalSystem.MicrosoftXboxBIOS
                    or PhysicalSystem.NintendoGameCubeBIOS
                    or PhysicalSystem.SonyPlayStationBIOS
                    or PhysicalSystem.SonyPlayStation2BIOS => false,

                // Disc-Based Consoles
                PhysicalSystem.AppleBandaiPippin
                    or PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem
                    or PhysicalSystem.BandaiPlaydiaQuickInteractiveSystem
                    or PhysicalSystem.HasbroVideoNow
                    or PhysicalSystem.HasbroVideoNowColor
                    or PhysicalSystem.HasbroVideoNowJr
                    or PhysicalSystem.HasbroVideoNowXP
                    or PhysicalSystem.NintendoGameCube
                    or PhysicalSystem.NintendoWii
                    or PhysicalSystem.NintendoWiiU
                    or PhysicalSystem.Panasonic3DOInteractiveMultiplayer
                    or PhysicalSystem.PhilipsCDi
                    or PhysicalSystem.PioneerLaserActive
                    or PhysicalSystem.MarkerDiscBasedConsoleEnd => false,

                // Computers
                PhysicalSystem.AppleMacintosh
                    or PhysicalSystem.MarkerComputerEnd => false,

                // Arcade
                PhysicalSystem.AmericanLaserGames3DO
                    or PhysicalSystem.Atari3DO
                    or PhysicalSystem.NewJatreCDi
                    or PhysicalSystem.PanasonicM2
                    or PhysicalSystem.MarkerArcadeEnd => false,

                // Other
                PhysicalSystem.DatelPlayStationCheatDeviceUpdates
                    or PhysicalSystem.SuperAudioCD
                    or PhysicalSystem.MarkerOtherEnd => false,

                null => false,
                _ => true,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system has reversed ringcodes
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system has reversed ringcodes, false otherwise</returns>
        public static bool HasReversedRingcodes(this PhysicalSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                PhysicalSystem.SonyPlayStation2
                    or PhysicalSystem.SonyPlayStation3
                    or PhysicalSystem.SonyPlayStation4
                    or PhysicalSystem.SonyPlayStation5
                    or PhysicalSystem.SonyPlayStationPortable => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system is considered audio-only
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system is audio-only, false otherwise</returns>
        /// <remarks>
        /// Philips CD-i should NOT be in this list. It's being included until there's a
        /// reasonable distinction between CD-i and CD-i ready on the database side.
        /// </remarks>
        public static bool IsAudio(this PhysicalSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                PhysicalSystem.AtariJaguarCDInteractiveMultimediaSystem
                    or PhysicalSystem.AudioCD
                    or PhysicalSystem.DatelPlayStationCheatDeviceUpdates
                    or PhysicalSystem.DVDAudio
                    or PhysicalSystem.HasbroiONEducationalGamingSystem
                    or PhysicalSystem.HasbroVideoNow
                    or PhysicalSystem.HasbroVideoNowColor
                    or PhysicalSystem.HasbroVideoNowJr
                    or PhysicalSystem.HasbroVideoNowXP
                    or PhysicalSystem.PhilipsCDi
                    or PhysicalSystem.SuperAudioCD => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system is a marker value
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system is a marker value, false otherwise</returns>
        public static bool IsMarker(this PhysicalSystem system)
            => ((PhysicalSystem?)system).IsMarker();

        /// <summary>
        /// Determine if a system is a marker value
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system is a marker value, false otherwise</returns>
        public static bool IsMarker(this PhysicalSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                PhysicalSystem.MarkerArcadeEnd
                    or PhysicalSystem.MarkerComputerEnd
                    or PhysicalSystem.MarkerDiscBasedConsoleEnd
                    or PhysicalSystem.MarkerOtherEnd => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Determine if a system is considered XGD
        /// </summary>
        /// <param name="system">PhysicalSystem value to check</param>
        /// <returns>True if the system is XGD, false otherwise</returns>
        public static bool IsXGD(this PhysicalSystem? system)
        {
#pragma warning disable IDE0072 // Add missing cases
            return system switch
            {
                PhysicalSystem.MicrosoftXbox
                    or PhysicalSystem.MicrosoftXbox360
                    or PhysicalSystem.MicrosoftXboxOne
                    or PhysicalSystem.MicrosoftXboxSeriesXS => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// List all systems with their short usable names
        /// </summary>
        public static List<string> ListSystems()
        {
            var systems = (PhysicalSystem[])Enum.GetValues(typeof(PhysicalSystem));
            var knownSystems = Array.FindAll(systems, s => !s.IsMarker() && s.GetCategory() != SystemCategory.NONE);
            Array.Sort(knownSystems, (x, y) => (x.LongName() ?? string.Empty).CompareTo(y.LongName() ?? string.Empty));
            return [.. Array.ConvertAll(knownSystems, val => $"{val.ShortName()} - {val.LongName()}")];
        }

        /// <summary>
        /// Get the Redump longnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? LongName(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.LongName;

        /// <summary>
        /// Get the Redump longnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? LongName(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortNameAlt(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.RedumpOrgCode;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortNameAlt(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.RedumpOrgCode;

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this PhysicalSystem system)
            => ((PhysicalSystem?)system).GetCategory();

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.Category ?? SystemCategory.NONE;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a decrypted keys pack
        /// </summary>
        public static bool HasDkeys(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDkeys ?? false;

        /// <summary>
        /// Determine if a system has a decrypted keys pack
        /// </summary>
        public static bool HasDkeys(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDkeys ?? false;

        /// <summary>
        /// Determine if a system has a GDI pack
        /// </summary>
        public static bool HasGdi(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasGdi ?? false;

        /// <summary>
        /// Determine if a system has a GDI pack
        /// </summary>
        public static bool HasGdi(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasGdi ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has an LSD pack
        /// </summary>
        public static bool HasLsd(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasLsd ?? false;

        /// <summary>
        /// Determine if a system has an LSD pack
        /// </summary>
        public static bool HasLsd(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasLsd ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this PhysicalSystem system)
            => (AttributeHelper<PhysicalSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasSbi ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this PhysicalSystem? system)
            => (AttributeHelper<PhysicalSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasSbi ?? false;

        /// <summary>
        /// Get the PhysicalSystem enum value for a given string
        /// </summary>
        /// <param name="system">String value to convert</param>
        /// <returns>PhysicalSystem represented by the string, if possible</returns>
        public static PhysicalSystem? ToPhysicalSystem(this string? system)
        {
            // No value means no match
            if (system is null || system.Length == 0)
                return null;

            system = system.ToLowerInvariant();
            var redumpSystems = (PhysicalSystem[])Enum.GetValues(typeof(PhysicalSystem));

            // Check short names
            int index = Array.FindIndex(redumpSystems, s => system == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

            // Check redump.org short names
            index = Array.FindIndex(redumpSystems, s => system == s.ShortNameAlt()?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

            // Check long names
            index = Array.FindIndex(redumpSystems, s => system == s.LongName()?.ToLowerInvariant()
                || system == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

            return null;
        }

        #endregion

        #region Region

        /// <summary>
        /// Get the Redump longnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? LongName(this Region region)
            => (AttributeHelper<Region>.GetHumanReadableAttribute(region) as RegionCodeAttribute)?.LongName;

        /// <summary>
        /// Get the Redump longnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? LongName(this Region? region)
            => (AttributeHelper<Region?>.GetHumanReadableAttribute(region) as RegionCodeAttribute)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortName(this Region region)
            => (AttributeHelper<Region>.GetHumanReadableAttribute(region) as RegionCodeAttribute)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortName(this Region? region)
            => (AttributeHelper<Region?>.GetHumanReadableAttribute(region) as RegionCodeAttribute)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortNameAlt(this Region region)
            => (AttributeHelper<Region>.GetHumanReadableAttribute(region) as RegionCodeAttribute)?.RedumpOrgCode;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortNameAlt(this Region? region)
            => (AttributeHelper<Region?>.GetHumanReadableAttribute(region) as RegionCodeAttribute)?.RedumpOrgCode;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="region">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static Region? ToRegion(this string? region)
        {
            // No value means no match
            if (region is null || region.Length == 0)
                return null;

            region = region.ToLowerInvariant();
            var redumpSystems = (Region[])Enum.GetValues(typeof(Region));

            // Check short names
            int index = Array.FindIndex(redumpSystems, s => region == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

            // Check redump.org short names
            index = Array.FindIndex(redumpSystems, s => region == s.ShortNameAlt()?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

            // Check long names
            index = Array.FindIndex(redumpSystems, s => region == s.LongName()?.ToLowerInvariant()
                || region == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return redumpSystems[index];

            return null;
        }

        #endregion

        #region Site Code

        /// <summary>
        /// List all site codes with their short usable names
        /// </summary>
        public static List<string> ListSiteCodes()
        {
            var siteCodes = new List<string>();

            foreach (var val in Enum.GetValues(typeof(SiteCode)))
            {
                string? shortName = ((SiteCode?)val).ShortName()?.TrimEnd(':');
                string? longName = ((SiteCode?)val).LongName()?.TrimEnd(':');

                bool isCommentCode = ((SiteCode?)val).IsCommentCode();
                bool isContentCode = ((SiteCode?)val).IsContentCode();
                bool isMultiline = ((SiteCode?)val).IsMultiLine();

                // Invalid codes should be skipped
                if (shortName is null || longName is null)
                    continue;

                // Handle site tags
                string siteCode;
                if (shortName == longName)
                    siteCode = "***".PadRight(16, ' ');
                else
                    siteCode = shortName.PadRight(16, ' ');

                // Handle expanded tags
                siteCode += longName.PadRight(32, ' ');

                // Include special indicators, if necessary
                var additionalInfo = new List<string>();
                if (isCommentCode)
                    additionalInfo.Add("Comment Field");
                if (isContentCode)
                    additionalInfo.Add("Content Field");
                if (isMultiline)
                    additionalInfo.Add("Multiline");
                if (additionalInfo.Count > 0)
                    siteCode += $"[{string.Join(", ", [.. additionalInfo])}]";

                // Add the formatted site code
                siteCodes.Add(siteCode);
            }

            return siteCodes;
        }

        /// <summary>
        /// Check if a site code is boolean or not
        /// </summary>
        /// <param name="siteCode">SiteCode to check</param>
        /// <returns>True if the code field is a flag with no value, false otherwise</returns>
        public static bool IsBoolean(this SiteCode siteCode)
            => ((SiteCode?)siteCode).IsBoolean();

        /// <summary>
        /// Check if a site code is boolean or not
        /// </summary>
        /// <param name="siteCode">SiteCode to check</param>
        /// <returns>True if the code field is a flag with no value, false otherwise</returns>
        public static bool IsBoolean(this SiteCode? siteCode)
        {
#pragma warning disable IDE0072 // Add missing cases
            return siteCode switch
            {
                SiteCode.PCMacHybrid => true,
                SiteCode.PostgapType => true,
                SiteCode.VCD => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Check if a site code should live in the comments section
        /// </summary>
        /// <returns>True if the code field is in comments by default, false otherwise</returns>
        public static bool IsCommentCode(this SiteCode siteCode)
            => ((SiteCode?)siteCode).IsCommentCode();

        /// <summary>
        /// Check if a site code should live in the comments section
        /// </summary>
        /// <returns>True if the code field is in comments by default, false otherwise</returns>
        public static bool IsCommentCode(this SiteCode? siteCode)
        {
#pragma warning disable IDE0072 // Add missing cases
            return siteCode switch
            {
                // Identifying Info
                SiteCode.AdditionalBCAData => true,
                SiteCode.AlternativeTitle => true,
                SiteCode.AlternativeForeignTitle => true,
                SiteCode.BBFCRegistrationNumber => true,
                SiteCode.CompatibleOS => true,
                SiteCode.CoverID => true,
                SiteCode.DiscHologramID => true,
                SiteCode.DiscID => true,
                SiteCode.DiscTitleNonLatin => true,
                SiteCode.DMIHash => true,
                SiteCode.DNASDiscID => true,
                SiteCode.EditionNonLatin => true,
                SiteCode.Filename => true,
                SiteCode.Genre => true,
                SiteCode.HighSierraVolumeDescriptor => true,
                SiteCode.InternalName => true,
                SiteCode.InternalSerialName => true,
                SiteCode.ISBN => true,
                SiteCode.ISSN => true,
                SiteCode.LogsLink => true,
                SiteCode.Multisession => true,
                SiteCode.PCMacHybrid => true,
                SiteCode.PFIHash => true,
                SiteCode.PostgapType => true,
                SiteCode.PPN => true,
                SiteCode.Protection => true,
                SiteCode.RingNonZeroDataStart => true,
                SiteCode.RingPerfectAudioOffset => true,
                SiteCode.Series => true,
                SiteCode.SSHash => true,
                SiteCode.SSVersion => true,
                SiteCode.SteamAppID => true,
                SiteCode.TitleID => true,
                SiteCode.UniversalHash => true,
                SiteCode.VCD => true,
                SiteCode.VFCCode => true,
                SiteCode.VolumeLabel => true,
                SiteCode.XeMID => true,
                SiteCode.XMID => true,

                // Publisher / Company IDs
                SiteCode.TwoKGamesID => true,
                SiteCode.AcclaimID => true,
                SiteCode.AccoladeID => true,
                SiteCode.ActivisionID => true,
                SiteCode.BandaiID => true,
                SiteCode.BethesdaID => true,
                SiteCode.CDProjektID => true,
                SiteCode.DisneyInteractiveID => true,
                SiteCode.EidosID => true,
                SiteCode.ElectronicArtsID => true,
                SiteCode.FoxInteractiveID => true,
                SiteCode.GTInteractiveID => true,
                SiteCode.InterplayID => true,
                SiteCode.JASRACID => true,
                SiteCode.KingRecordsID => true,
                SiteCode.KoeiID => true,
                SiteCode.KonamiID => true,
                SiteCode.LucasArtsID => true,
                SiteCode.MicrosoftID => true,
                SiteCode.NaganoID => true,
                SiteCode.NamcoID => true,
                SiteCode.NipponIchiSoftwareID => true,
                SiteCode.OriginID => true,
                SiteCode.PonyCanyonID => true,
                SiteCode.SegaID => true,
                SiteCode.SelenID => true,
                SiteCode.SierraID => true,
                SiteCode.TaitoID => true,
                SiteCode.UbisoftID => true,
                SiteCode.ValveID => true,

                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Check if a site code should live in the contents section
        /// </summary>
        /// <returns>True if the code field is in contents by default, false otherwise</returns>
        public static bool IsContentCode(this SiteCode siteCode)
            => ((SiteCode?)siteCode).IsContentCode();

        /// <summary>
        /// Check if a site code should live in the contents section
        /// </summary>
        /// <returns>True if the code field is in contents by default, false otherwise</returns>
        public static bool IsContentCode(this SiteCode? siteCode)
        {
#pragma warning disable IDE0072 // Add missing cases
            return siteCode switch
            {
                SiteCode.Applications => true,
                SiteCode.Extras => true,
                SiteCode.GameFootage => true,
                SiteCode.Games => true,
                SiteCode.NetYarozeGames => true,
                SiteCode.Patches => true,
                SiteCode.PlayableDemos => true,
                SiteCode.RollingDemos => true,
                SiteCode.Savegames => true,
                SiteCode.SteamSimSidDepotID => true,
                SiteCode.SteamCsmCsdDepotID => true,
                SiteCode.TechDemos => true,
                SiteCode.Videos => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Check if a site code is multi-line or not
        /// </summary>
        /// <returns>True if the code field is multiline by default, false otherwise</returns>
        public static bool IsMultiLine(this SiteCode siteCode)
            => ((SiteCode?)siteCode).IsMultiLine();

        /// <summary>
        /// Check if a site code is multi-line or not
        /// </summary>
        /// <returns>True if the code field is multiline by default, false otherwise</returns>
        public static bool IsMultiLine(this SiteCode? siteCode)
        {
#pragma warning disable IDE0072 // Add missing cases
            return siteCode switch
            {
                SiteCode.Extras => true,
                SiteCode.Filename => true,
                SiteCode.Games => true,
                SiteCode.GameFootage => true,
                SiteCode.HighSierraVolumeDescriptor => true,
                SiteCode.Multisession => true,
                SiteCode.NetYarozeGames => true,
                SiteCode.Patches => true,
                SiteCode.PlayableDemos => true,
                SiteCode.RollingDemos => true,
                SiteCode.Savegames => true,
                SiteCode.SteamSimSidDepotID => true,
                SiteCode.SteamCsmCsdDepotID => true,
                SiteCode.TechDemos => true,
                SiteCode.Videos => true,
                _ => false,
            };
#pragma warning restore IDE0072 // Add missing cases
        }

        /// <summary>
        /// Get the HTML version for each known site code
        /// </summary>
        public static string? LongName(this SiteCode siteCode)
            => AttributeHelper<SiteCode>.GetHumanReadableAttribute(siteCode)?.LongName;

        /// <summary>
        /// Get the HTML version for each known site code
        /// </summary>
        public static string? LongName(this SiteCode? siteCode)
            => AttributeHelper<SiteCode?>.GetHumanReadableAttribute(siteCode)?.LongName;

        /// <summary>
        /// Get the short tag for each known site code
        /// </summary>
        public static string? ShortName(this SiteCode siteCode)
            => AttributeHelper<SiteCode>.GetHumanReadableAttribute(siteCode)?.ShortName;

        /// <summary>
        /// Get the short tag for each known site code
        /// </summary>
        public static string? ShortName(this SiteCode? siteCode)
            => AttributeHelper<SiteCode?>.GetHumanReadableAttribute(siteCode)?.ShortName;

        #endregion

        #region Sort Category

        /// <summary>
        /// Get the human readable name for a SortCategory
        /// </summary>
        /// <param name="sortCategory"></param>
        /// <returns></returns>
        public static string? LongName(this SortCategory sortCategory)
            => AttributeHelper<SortCategory>.GetHumanReadableAttribute(sortCategory)?.LongName;

        /// <summary>
        /// Get the human readable name for a SortCategory
        /// </summary>
        /// <param name="sortCategory"></param>
        /// <returns></returns>
        public static string? LongName(this SortCategory? sortCategory)
            => AttributeHelper<SortCategory?>.GetHumanReadableAttribute(sortCategory)?.LongName;

        /// <summary>
        /// Get the URL path part for a SortCategory
        /// </summary>
        /// <param name="sortCategory"></param>
        /// <returns></returns>
        public static string? ShortName(this SortCategory sortCategory)
            => AttributeHelper<SortCategory>.GetHumanReadableAttribute(sortCategory)?.ShortName;

        /// <summary>
        /// Get the URL path part for a SortCategory
        /// </summary>
        /// <param name="sortCategory"></param>
        /// <returns></returns>
        public static string? ShortName(this SortCategory? sortCategory)
            => AttributeHelper<SortCategory?>.GetHumanReadableAttribute(sortCategory)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="sortCategory">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static SortCategory? ToSortCategory(this string? sortCategory)
        {
            // No value means no match
            if (sortCategory is null || sortCategory.Length == 0)
                return null;

            sortCategory = sortCategory.ToLowerInvariant();
            var sortCategories = (SortCategory[])Enum.GetValues(typeof(SortCategory));

            // Check short names
            int index = Array.FindIndex(sortCategories, s => sortCategory == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

            // Check long names
            index = Array.FindIndex(sortCategories, s => sortCategory == s.LongName()?.ToLowerInvariant()
                || sortCategory == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

            return null;
        }

        #endregion

        #region Sort Direction

        /// <summary>
        /// Get the human readable name for a SortDirection
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? LongName(this SortDirection sortDirection)
            => AttributeHelper<SortDirection>.GetHumanReadableAttribute(sortDirection)?.LongName;

        /// <summary>
        /// Get the human readable name for a SortDirection
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? LongName(this SortDirection? sortDirection)
            => AttributeHelper<SortDirection?>.GetHumanReadableAttribute(sortDirection)?.LongName;

        /// <summary>
        /// Get the URL path part for a SortDirection
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? ShortName(this SortDirection sortDirection)
            => AttributeHelper<SortDirection>.GetHumanReadableAttribute(sortDirection)?.ShortName;

        /// <summary>
        /// Get the URL path part for a SortDirection
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static string? ShortName(this SortDirection? sortDirection)
            => AttributeHelper<SortDirection?>.GetHumanReadableAttribute(sortDirection)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="sortDirection">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static SortDirection? ToSortDirection(this string? sortDirection)
        {
            // No value means no match
            if (sortDirection is null || sortDirection.Length == 0)
                return null;

            sortDirection = sortDirection.ToLowerInvariant();
            var sortCategories = (SortDirection[])Enum.GetValues(typeof(SortDirection));

            // Check short names
            int index = Array.FindIndex(sortCategories, s => sortDirection == s.ShortName()?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

            // Check long names
            index = Array.FindIndex(sortCategories, s => sortDirection == s.LongName()?.ToLowerInvariant()
                || sortDirection == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant());
            if (index > -1)
                return sortCategories[index];

            return null;
        }

        #endregion

        #region System Category

        /// <summary>
        /// Get the string representation of the system category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string? LongName(this SystemCategory? category)
            => AttributeHelper<SystemCategory?>.GetHumanReadableAttribute(category)?.LongName;

        #endregion

        #region Yes/No

        /// <summary>
        /// Get the string representation of the YesNo value
        /// </summary>
        /// <param name="yesno"></param>
        /// <returns></returns>
        public static string LongName(this YesNo? yesno)
            => AttributeHelper<YesNo?>.GetHumanReadableAttribute(yesno)?.LongName ?? "Yes/No";

        /// <summary>
        /// Get the YesNo enum value for a given nullable boolean
        /// </summary>
        /// <param name="yesno">Nullable boolean value to convert</param>
        /// <returns>YesNo represented by the nullable boolean, if possible</returns>
        public static YesNo ToYesNo(this bool yesno)
        {
            return yesno switch
            {
                false => YesNo.No,
                true => YesNo.Yes,
            };
        }

        /// <summary>
        /// Get the YesNo enum value for a given nullable boolean
        /// </summary>
        /// <param name="yesno">Nullable boolean value to convert</param>
        /// <returns>YesNo represented by the nullable boolean, if possible</returns>
        public static YesNo? ToYesNo(this bool? yesno)
        {
            return yesno switch
            {
                false => YesNo.No,
                true => YesNo.Yes,
                _ => YesNo.NULL,
            };
        }

        /// <summary>
        /// Get the YesNo enum value for a given string
        /// </summary>
        /// <param name="yesno">String value to convert</param>
        /// <returns>YesNo represented by the string, if possible</returns>
        public static YesNo? ToYesNo(this string? yesno)
        {
            return (yesno?.ToLowerInvariant()) switch
            {
                "no" or "false" => YesNo.No,
                "yes" or "true" => YesNo.Yes,
                _ => YesNo.NULL,
            };
        }

        #endregion
    }
}
