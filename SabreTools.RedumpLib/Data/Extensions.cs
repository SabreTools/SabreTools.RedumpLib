﻿using System;
using System.Collections.Generic;
using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.Data
{
    /// <summary>
    /// Information pertaining to Redump systems
    /// </summary>
    public static class Extensions
    {
        #region Cross-Enumeration

        /// <summary>
        /// Get a list of valid MediaTypes for a given RedumpSystem
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>MediaTypes, if possible</returns>
        public static List<MediaType?> MediaTypes(this RedumpSystem? system)
        {
            var types = new List<MediaType?>();

            switch (system)
            {
                #region Consoles

                // https://en.wikipedia.org/wiki/Atari_Jaguar_CD
                case RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Playdia
                case RedumpSystem.BandaiPlaydiaQuickInteractiveSystem:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Apple_Bandai_Pippin
                case RedumpSystem.BandaiPippin:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Amiga_CD32
                case RedumpSystem.CommodoreAmigaCD32:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Commodore_CDTV
                case RedumpSystem.CommodoreAmigaCDTV:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/EVO_Smart_Console
                case RedumpSystem.EnvizionsEVOSmartConsole:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/FM_Towns_Marty
                case RedumpSystem.FujitsuFMTownsMarty:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.FloppyDisk);
                    break;

                // http://videogamekraken.com/ion-educational-gaming-system-by-hasbro
                case RedumpSystem.HasbroiONEducationalGamingSystem:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case RedumpSystem.HasbroVideoNow:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case RedumpSystem.HasbroVideoNowColor:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case RedumpSystem.HasbroVideoNowJr:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case RedumpSystem.HasbroVideoNowXP:
                    types.Add(MediaType.CDROM);
                    break;

                case RedumpSystem.MattelFisherPriceiXL:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/HyperScan
                case RedumpSystem.MattelHyperScan:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_(console)
                case RedumpSystem.MicrosoftXbox:
                    types.Add(MediaType.DVD);
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_360
                case RedumpSystem.MicrosoftXbox360:
                    types.Add(MediaType.DVD);
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_One
                case RedumpSystem.MicrosoftXboxOne:
                    types.Add(MediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_Series_X_and_Series_S
                case RedumpSystem.MicrosoftXboxSeriesXS:
                    types.Add(MediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/TurboGrafx-16
                case RedumpSystem.NECPCEngineCDTurboGrafxCD:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PC-FX
                case RedumpSystem.NECPCFXPCFXGA:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/GameCube
                case RedumpSystem.NintendoGameCube:
                    types.Add(MediaType.DVD); // Only added here to help users; not strictly correct
                    types.Add(MediaType.NintendoGameCubeGameDisc);
                    break;

                // https://en.wikipedia.org/wiki/Super_NES_CD-ROM
                case RedumpSystem.NintendoSonySuperNESCDROMSystem:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Wii
                case RedumpSystem.NintendoWii:
                    types.Add(MediaType.DVD); // Only added here to help users; not strictly correct
                    types.Add(MediaType.NintendoWiiOpticalDisc);
                    break;

                // https://en.wikipedia.org/wiki/Wii_U
                case RedumpSystem.NintendoWiiU:
                    types.Add(MediaType.NintendoWiiUOpticalDisc);
                    break;

                // https://en.wikipedia.org/wiki/3DO_Interactive_Multiplayer
                case RedumpSystem.Panasonic3DOInteractiveMultiplayer:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Philips_CD-i
                case RedumpSystem.PhilipsCDi:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/LaserActive
                case RedumpSystem.PioneerLaserActive:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.LaserDisc);
                    break;

                // https://en.wikipedia.org/wiki/Sega_CD
                case RedumpSystem.SegaMegaCDSegaCD:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Dreamcast
                case RedumpSystem.SegaDreamcast:
                    types.Add(MediaType.CDROM); // Low density partition, MIL-CD
                    types.Add(MediaType.GDROM); // High density partition
                    break;

                // https://en.wikipedia.org/wiki/Sega_Saturn
                case RedumpSystem.SegaSaturn:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Neo_Geo_CD
                case RedumpSystem.SNKNeoGeoCD:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_(console)
                case RedumpSystem.SonyPlayStation:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_2
                case RedumpSystem.SonyPlayStation2:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_3
                case RedumpSystem.SonyPlayStation3:
                    types.Add(MediaType.BluRay);
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_4
                case RedumpSystem.SonyPlayStation4:
                    types.Add(MediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_5
                case RedumpSystem.SonyPlayStation5:
                    types.Add(MediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_Portable
                case RedumpSystem.SonyPlayStationPortable:
                    types.Add(MediaType.UMD);
                    types.Add(MediaType.CDROM); // Development discs only
                    types.Add(MediaType.DVD); // Development discs only
                    break;

                // https://en.wikipedia.org/wiki/Tandy_Video_Information_System
                case RedumpSystem.MemorexVisualInformationSystem:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Nuon_(DVD_technology)
                case RedumpSystem.VMLabsNUON:
                    types.Add(MediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/V.Flash
                case RedumpSystem.VTechVFlashVSmilePro:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Game_Wave_Family_Entertainment_System
                case RedumpSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem:
                    types.Add(MediaType.CDROM); // Firmware discs only(?)
                    types.Add(MediaType.DVD);
                    break;

                #endregion

                #region Computers

                // https://en.wikipedia.org/wiki/Acorn_Archimedes
                case RedumpSystem.AcornArchimedes:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/Macintosh
                case RedumpSystem.AppleMacintosh:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    types.Add(MediaType.FloppyDisk);
                    types.Add(MediaType.HardDisk);
                    break;

                // https://en.wikipedia.org/wiki/Amiga
                case RedumpSystem.CommodoreAmigaCD:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/FM_Towns
                case RedumpSystem.FujitsuFMTownsseries:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/IBM_PC_compatible
                case RedumpSystem.IBMPCcompatible:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    types.Add(MediaType.BluRay);
                    types.Add(MediaType.FloppyDisk);
                    types.Add(MediaType.HardDisk);
                    types.Add(MediaType.DataCartridge);
                    break;

                // https://en.wikipedia.org/wiki/PC-8800_series
                case RedumpSystem.NECPC88series:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/PC-9800_series
                case RedumpSystem.NECPC98series:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    types.Add(MediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/X68000
                case RedumpSystem.SharpX68000:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.FloppyDisk);
                    break;

                #endregion

                #region Arcade

                // https://www.bigbookofamigahardware.com/bboah/product.aspx?id=36
                case RedumpSystem.AmigaCUBOCD32:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Orbatak
                case RedumpSystem.AmericanLaserGames3DO:
                    types.Add(MediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=779
                case RedumpSystem.Atari3DO:
                    types.Add(MediaType.CDROM);
                    break;

                // http://newlifegames.net/nlg/index.php?topic=22003.0
                // http://newlifegames.net/nlg/index.php?topic=5486.msg119440
                case RedumpSystem.Atronic:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // https://www.arcade-museum.com/members/member_detail.php?member_id=406530
                case RedumpSystem.AUSCOMSystem1:
                    types.Add(MediaType.CDROM);
                    break;

                // http://newlifegames.net/nlg/index.php?topic=285.0
                case RedumpSystem.BallyGameMagic:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/CP_System_III
                case RedumpSystem.CapcomCPSystemIII:
                    types.Add(MediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.funworldPhotoPlay:
                    types.Add(MediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.GlobalVRVarious:
                    types.Add(MediaType.CDROM);
                    break;

                // https://service.globalvr.com/troubleshooting/vortek.html
                case RedumpSystem.GlobalVRVortek:
                    types.Add(MediaType.CDROM);
                    break;

                // https://service.globalvr.com/downloads/v3/040-1001-01c-V3-System-Manual.pdf
                case RedumpSystem.GlobalVRVortekV3:
                    types.Add(MediaType.CDROM);
                    break;

                // https://www.icegame.com/games
                case RedumpSystem.ICEPCHardware:
                    types.Add(MediaType.DVD);
                    break;

                // https://github.com/mamedev/mame/blob/master/src/mame/drivers/iteagle.cpp
                case RedumpSystem.IncredibleTechnologiesEagle:
                    types.Add(MediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.IncredibleTechnologiesVarious:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // UNKNOWN
                case RedumpSystem.JVLiTouch:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/E-Amusement
                case RedumpSystem.KonamieAmusement:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=828
                case RedumpSystem.KonamiFireBeat:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=577
                case RedumpSystem.KonamiSystemGV:
                    types.Add(MediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=575
                case RedumpSystem.KonamiM2:
                    types.Add(MediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=586
                // http://system16.com/hardware.php?id=977
                case RedumpSystem.KonamiPython:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=976
                // http://system16.com/hardware.php?id=831
                case RedumpSystem.KonamiPython2:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=582
                // http://system16.com/hardware.php?id=822
                // http://system16.com/hardware.php?id=823
                case RedumpSystem.KonamiSystem573:
                    types.Add(MediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=827
                case RedumpSystem.KonamiTwinkle:
                    types.Add(MediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.KonamiVarious:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // http://www.meritgames.com/Support_Center/manuals/PM0591-01.pdf
                case RedumpSystem.MeritIndustriesBoardwalk:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // http://www.meritgames.com/Support_Center/Force%20Elite/PM0380-09.pdf
                // http://www.meritgames.com/Support_Center/Force%20Upright/PM0382-07%20FORCE%20Upright%20manual.pdf
                // http://www.meritgames.com/Support_Center/Force%20Upright/PM0383-07%20FORCE%20Upright%20manual.pdf
                case RedumpSystem.MeritIndustriesMegaTouchForce:
                    types.Add(MediaType.CDROM);
                    break;

                // http://www.meritgames.com/Service%20Center/Ion%20Troubleshooting.pdf
                case RedumpSystem.MeritIndustriesMegaTouchION:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // http://www.meritgames.com/Support_Center/EZ%20Maxx/Manuals/MAXX%20Elite%20with%20coin.pdf
                // http://www.meritgames.com/Support_Center/EZ%20Maxx/Manuals/MAXX%20Elite.pdf
                // http://www.meritgames.com/Support_Center/manuals/90003010%20Maxx%20TSM_Rev%20C.pdf
                case RedumpSystem.MeritIndustriesMegaTouchMaxx:
                    types.Add(MediaType.CDROM);
                    break;

                // http://www.meritgames.com/Support_Center/manuals/pm0076_OA_Megatouch%20XL%20Trouble%20Shooting%20Manual.pdf
                // http://www.meritgames.com/Support_Center/MEGA%20XL/manuals/Megatouch_XL_pm0109-0D.pdf
                // http://www.meritgames.com/Support_Center/MEGA%20XL/manuals/Megatouch_XL_Super_5000_manual.pdf
                case RedumpSystem.MeritIndustriesMegaTouchXL:
                    types.Add(MediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=543
                // http://system16.com/hardware.php?id=546
                // http://system16.com/hardware.php?id=872
                case RedumpSystem.NamcoSystem246256:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=545
                case RedumpSystem.NamcoSegaNintendoTriforce:
                    types.Add(MediaType.CDROM); // Low density partition
                    types.Add(MediaType.GDROM); // High density partition
                    break;

                // http://system16.com/hardware.php?id=535
                case RedumpSystem.NamcoSystem12:
                    types.Add(MediaType.CDROM);
                    break;

                // https://www.arcade-history.com/?n=the-yakyuuken-part-1&page=detail&id=33049
                case RedumpSystem.NewJatreCDi:
                    types.Add(MediaType.CDROM);
                    break;

                // http://blog.system11.org/?p=2499
                case RedumpSystem.NichibutsuHighRateSystem:
                    types.Add(MediaType.DVD);
                    break;

                // http://blog.system11.org/?p=2514
                case RedumpSystem.NichibutsuSuperCD:
                    types.Add(MediaType.CDROM);
                    break;

                // http://collectedit.com/collectors/shou-time-213/arcade-pcbs-281/x-rate-dvd-series-17-newlywed-life-japan-by-nichibutsu-32245
                case RedumpSystem.NichibutsuXRateSystem:
                    types.Add(MediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/Panasonic_M2
                case RedumpSystem.PanasonicM2:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                // https://github.com/mamedev/mame/blob/master/src/mame/drivers/photoply.cpp
                case RedumpSystem.PhotoPlayVarious:
                    types.Add(MediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.RawThrillsVarious:
                    types.Add(MediaType.DVD);
                    break;

                // UNKNOWN
                case RedumpSystem.SegaALLS:
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=729
                case RedumpSystem.SegaChihiro:
                    types.Add(MediaType.CDROM); // Low density partition
                    types.Add(MediaType.GDROM); // High density partition
                    break;

                // http://system16.com/hardware.php?id=907
                case RedumpSystem.SegaEuropaR:
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=985
                // http://system16.com/hardware.php?id=731
                // http://system16.com/hardware.php?id=984
                // http://system16.com/hardware.php?id=986
                case RedumpSystem.SegaLindbergh:
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=721
                // http://system16.com/hardware.php?id=723
                // http://system16.com/hardware.php?id=906
                // http://system16.com/hardware.php?id=722
                case RedumpSystem.SegaNaomi:
                    types.Add(MediaType.CDROM); // Low density partition
                    types.Add(MediaType.GDROM); // High density partition
                    break;

                // http://system16.com/hardware.php?id=725
                // http://system16.com/hardware.php?id=726
                // http://system16.com/hardware.php?id=727
                case RedumpSystem.SegaNaomi2:
                    types.Add(MediaType.CDROM); // Low density partition
                    types.Add(MediaType.GDROM); // High density partition
                    break;

                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
                case RedumpSystem.SegaNu:
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=910
                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
                case RedumpSystem.SegaRingEdge:
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=982
                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
                case RedumpSystem.SegaRingEdge2:
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=911
                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
                case RedumpSystem.SegaRingWide:
                    types.Add(MediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=711
                case RedumpSystem.SegaTitanVideo:
                    types.Add(MediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=709
                // http://system16.com/hardware.php?id=710
                case RedumpSystem.SegaSystem32:
                    types.Add(MediaType.CDROM);
                    break;

                // https://github.com/mamedev/mame/blob/master/src/mame/drivers/seibucats.cpp
                case RedumpSystem.SeibuCATSSystem:
                    types.Add(MediaType.DVD);
                    break;

                // https://www.tab.at/en/support/support/downloads
                case RedumpSystem.TABAustriaQuizard:
                    types.Add(MediaType.CDROM);
                    break;

                // https://primetimeamusements.com/product/tsumo-multi-game-motion-system/
                // https://www.highwaygames.com/arcade-machines/tsumo-tsunami-motion-8117/
                case RedumpSystem.TsunamiTsuMoMultiGameMotionSystem:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/UltraCade_Technologies
                case RedumpSystem.UltraCade:
                    types.Add(MediaType.CDROM);
                    types.Add(MediaType.DVD);
                    break;

                #endregion

                #region Others

                // https://en.wikipedia.org/wiki/Audio_CD
                case RedumpSystem.AudioCD:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Blu-ray#Player_profiles
                case RedumpSystem.BDVideo:
                    types.Add(MediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/DVD-Audio
                case RedumpSystem.DVDAudio:
                    types.Add(MediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/DVD-Video
                case RedumpSystem.DVDVideo:
                    types.Add(MediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/Blue_Book_(CD_standard)
                case RedumpSystem.EnhancedCD:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/HD_DVD
                case RedumpSystem.HDDVDVideo:
                    types.Add(MediaType.HDDVD);
                    break;

                // UNKNOWN
                case RedumpSystem.NavisoftNaviken21:
                    types.Add(MediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.PalmOS:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Photo_CD
                case RedumpSystem.PhotoCD:
                    types.Add(MediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.PlayStationGameSharkUpdates:
                    types.Add(MediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.PocketPC:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Doors_and_Windows_(EP)
                case RedumpSystem.RainbowDisc:
                    types.Add(MediaType.CDROM);
                    break;

                // https://segaretro.org/Prologue_21
                case RedumpSystem.SegaPrologue21MultimediaKaraokeSystem:
                    types.Add(MediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.SonyElectronicBook:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Super_Audio_CD
                case RedumpSystem.SuperAudioCD:
                    types.Add(MediaType.CDROM);
                    break;

                // https://www.cnet.com/products/tao-music-iktv-karaoke-station-karaoke-system-series/
                case RedumpSystem.TaoiKTV:
                    types.Add(MediaType.CDROM);
                    break;

                // http://ultimateconsoledatabase.com/golden/kiss_site.htm
                case RedumpSystem.TomyKissSite:
                    types.Add(MediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Video_CD
                case RedumpSystem.VideoCD:
                    types.Add(MediaType.CDROM);
                    break;

                #endregion

                default:
                    types.Add(MediaType.NONE);
                    break;
            }

            return types;
        }

        /// <summary>
        /// Convert master list of all media types to currently known Redump disc types
        /// </summary>
        /// <param name="mediaType">MediaType value to check</param>
        /// <returns>DiscType if possible, null on error</returns>
        public static DiscType? ToDiscType(this MediaType? mediaType)
        {
            return mediaType switch
            {
                MediaType.BluRay => DiscType.BD50,
                MediaType.CDROM => DiscType.CD,
                MediaType.DVD => DiscType.DVD9,
                MediaType.GDROM => DiscType.GDROM,
                MediaType.HDDVD => DiscType.HDDVDSL,
                // MediaType.MILCD => DiscType.MILCD, // TODO: Support this?
                MediaType.NintendoGameCubeGameDisc => DiscType.NintendoGameCubeGameDisc,
                MediaType.NintendoWiiOpticalDisc => DiscType.NintendoWiiOpticalDiscDL,
                MediaType.NintendoWiiUOpticalDisc => DiscType.NintendoWiiUOpticalDiscSL,
                MediaType.UMD => DiscType.UMDDL,
                _ => null,
            };
        }

        /// <summary>
        /// Convert currently known Redump disc types to master list of all media types
        /// </summary>
        /// <param name="discType">DiscType value to check</param>
        /// <returns>MediaType if possible, null on error</returns>
        public static MediaType? ToMediaType(this DiscType? discType)
        {
            return discType switch
            {
                DiscType.BD25
                    or DiscType.BD33
                    or DiscType.BD50
                    or DiscType.BD66
                    or DiscType.BD100
                    or DiscType.BD128 => MediaType.BluRay,
                DiscType.CD => MediaType.CDROM,
                DiscType.DVD5
                    or DiscType.DVD9 => MediaType.DVD,
                DiscType.GDROM => MediaType.GDROM,
                DiscType.HDDVDSL
                    or DiscType.HDDVDDL => MediaType.HDDVD,
                // DiscType.MILCD => MediaType.MILCD, // TODO: Support this?
                DiscType.NintendoGameCubeGameDisc => MediaType.NintendoGameCubeGameDisc,
                DiscType.NintendoWiiOpticalDiscSL
                    or DiscType.NintendoWiiOpticalDiscDL => MediaType.NintendoWiiOpticalDisc,
                DiscType.NintendoWiiUOpticalDiscSL => MediaType.NintendoWiiUOpticalDisc,
                DiscType.UMDSL
                    or DiscType.UMDDL => MediaType.UMD,
                _ => null,
            };
        }

        #endregion

        #region Disc Category

        /// <summary>
        /// Get the Redump longnames for each known category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string? LongName(this DiscCategory? category)
            => AttributeHelper<DiscCategory?>.GetAttribute(category)?.LongName;

        /// <summary>
        /// Get the Category enum value for a given string
        /// </summary>
        /// <param name="category">String value to convert</param>
        /// <returns>Category represented by the string, if possible</returns>
        public static DiscCategory? ToDiscCategory(string category)
        {
            return (category?.ToLowerInvariant()) switch
            {
                "games" => (DiscCategory?)DiscCategory.Games,
                "demos" => (DiscCategory?)DiscCategory.Demos,
                "video" => (DiscCategory?)DiscCategory.Video,
                "audio" => (DiscCategory?)DiscCategory.Audio,
                "multimedia" => (DiscCategory?)DiscCategory.Multimedia,
                "applications" => (DiscCategory?)DiscCategory.Applications,
                "coverdiscs" => (DiscCategory?)DiscCategory.Coverdiscs,
                "educational" => (DiscCategory?)DiscCategory.Educational,
                "bonusdiscs"
                    or "bonus discs" => (DiscCategory?)DiscCategory.BonusDiscs,
                "preproduction" => (DiscCategory?)DiscCategory.Preproduction,
                "addons"
                    or "add-ons" => (DiscCategory?)DiscCategory.AddOns,
                _ => (DiscCategory?)DiscCategory.Games,
            };
        }

        #endregion

        #region Disc Type

        /// <summary>
        /// Get the Redump longnames for each known disc type
        /// </summary>
        /// <param name="discType"></param>
        /// <returns></returns>
        public static string? LongName(this DiscType? discType)
            => AttributeHelper<DiscType?>.GetAttribute(discType)?.LongName;

        /// <summary>
        /// Get the DiscType enum value for a given string
        /// </summary>
        /// <param name="discType">String value to convert</param>
        /// <returns>DiscType represented by the string, if possible</returns>
        public static DiscType? ToDiscType(string discType)
        {
            return (discType?.ToLowerInvariant()) switch
            {
                "bd25"
                    or "bd-25" => (DiscType?)DiscType.BD25,
                "bd33"
                    or "bd-33" => (DiscType?)DiscType.BD33,
                "bd50"
                    or "bd-50" => (DiscType?)DiscType.BD50,
                "bd66"
                    or "bd-66" => (DiscType?)DiscType.BD66,
                "bd100"
                    or "bd-100" => (DiscType?)DiscType.BD100,
                "bd128"
                    or "bd-128" => (DiscType?)DiscType.BD128,
                "cd"
                    or "cdrom"
                    or "cd-rom" => (DiscType?)DiscType.CD,
                "dvd5"
                    or "dvd-5" => (DiscType?)DiscType.DVD5,
                "dvd9"
                    or "dvd-9" => (DiscType?)DiscType.DVD9,
                "gd"
                    or "gdrom"
                    or "gd-rom" => (DiscType?)DiscType.GDROM,
                "hddvd"
                    or "hddvdsl"
                    or "hd-dvd sl" => (DiscType?)DiscType.HDDVDSL,
                "hddvddl"
                    or "hd-dvd dl" => (DiscType?)DiscType.HDDVDDL,
                "milcd"
                    or "mil-cd" => (DiscType?)DiscType.MILCD,
                "nintendogamecubegamedisc"
                    or "nintendo game cube game disc" => (DiscType?)DiscType.NintendoGameCubeGameDisc,
                "nintendowiiopticaldiscsl"
                    or "nintendo wii optical disc sl" => (DiscType?)DiscType.NintendoWiiOpticalDiscSL,
                "nintendowiiopticaldiscdl"
                    or "nintendo wii optical disc dl" => (DiscType?)DiscType.NintendoWiiOpticalDiscDL,
                "nintendowiiuopticaldiscsl"
                    or "nintendo wii u optical disc sl" => (DiscType?)DiscType.NintendoWiiUOpticalDiscSL,
                "umd"
                    or "umdsl"
                    or "umd sl" => (DiscType?)DiscType.UMDSL,
                "umddl"
                    or "umd dl" => (DiscType?)DiscType.UMDDL,
                _ => null,
            };
        }

        #endregion

        #region Language

        /// <summary>
        /// Get the Redump longnames for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? LongName(this Language? language)
            => AttributeHelper<Language?>.GetAttribute(language)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ShortName(this Language? language)
        {
            // Some languages need to use the alternate code instead
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
        }

        /// <summary>
        /// Get the Language enum value for a given string
        /// </summary>
        /// <param name="lang">String value to convert</param>
        /// <returns>Language represented by the string, if possible</returns>
        public static Language? ToLanguage(string lang)
        {
            lang = lang.ToLowerInvariant();
            var languages = (Language[])Enum.GetValues(typeof(Language));

            // Check ISO 639-1 codes
            int index = Array.FindIndex(languages, l => lang == l.TwoLetterCode());
            if (index > -1)
                return languages[index];

            // Check standard ISO 639-2 codes
            index = Array.FindIndex(languages, r => lang == r.ThreeLetterCode());
            if (index > -1)
                return languages[index];

            // Check alternate ISO 639-2 codes
            index = Array.FindIndex(languages, r => lang == r.ThreeLetterCodeAlt());
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
            => (AttributeHelper<Language>.GetAttribute(language) as LanguageAttribute)?.ThreeLetterCode;

        /// <summary>
        /// Get the ISO 639-2 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCode(this Language? language)
            => (AttributeHelper<Language?>.GetAttribute(language) as LanguageAttribute)?.ThreeLetterCode;

        /// <summary>
        /// Get the ISO 639-2 alternate code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCodeAlt(this Language language)
            => (AttributeHelper<Language>.GetAttribute(language) as LanguageAttribute)?.ThreeLetterCodeAlt;

        /// <summary>
        /// Get the ISO 639-2 alternate code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? ThreeLetterCodeAlt(this Language? language)
            => (AttributeHelper<Language?>.GetAttribute(language) as LanguageAttribute)?.ThreeLetterCodeAlt;

        /// <summary>
        /// Get the ISO 639-1 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? TwoLetterCode(this Language language)
            => (AttributeHelper<Language>.GetAttribute(language) as LanguageAttribute)?.TwoLetterCode;

        /// <summary>
        /// Get the ISO 639-1 code for each known language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string? TwoLetterCode(this Language? language)
            => (AttributeHelper<Language?>.GetAttribute(language) as LanguageAttribute)?.TwoLetterCode;

        #endregion

        #region Language Selection

        /// <summary>
        /// Get the string representation of the LanguageSelection enum values
        /// </summary>
        /// <param name="langSelect">LanguageSelection value to convert</param>
        /// <returns>String representing the value, if possible</returns>
        public static string? LongName(this LanguageSelection? langSelect) => AttributeHelper<LanguageSelection?>.GetAttribute(langSelect)?.LongName;

        /// <summary>
        /// Get the LanguageSelection enum value for a given string
        /// </summary>
        /// <param name="langSelect">String value to convert</param>
        /// <returns>LanguageSelection represented by the string, if possible</returns>
        public static LanguageSelection? ToLanguageSelection(string langSelect)
        {
            return (langSelect?.ToLowerInvariant()) switch
            {
                "bios"
                    or "biossettings"
                    or "bios settings" => (LanguageSelection?)LanguageSelection.BiosSettings,
                "selector"
                    or "langselector"
                    or "lang selector"
                    or "langauge selector" => (LanguageSelection?)LanguageSelection.LanguageSelector,
                "options"
                    or "optionsmenu"
                    or "options menu" => (LanguageSelection?)LanguageSelection.OptionsMenu,
                _ => null,
            };
        }

        #endregion

        #region Media Type

        /// <summary>
        /// List all media types with their short usable names
        /// </summary>
        public static List<string> ListMediaTypes()
        {
            var mediaTypes = new List<string>();

            foreach (var val in Enum.GetValues(typeof(MediaType)))
            {
                if (val == null || ((MediaType)val) == MediaType.NONE)
                    continue;

                mediaTypes.Add($"{((MediaType?)val).ShortName()} - {((MediaType?)val).LongName()}");
            }

            return mediaTypes;
        }

        /// <summary>
        /// Get the Redump longnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? LongName(this MediaType? mediaType) => AttributeHelper<MediaType?>.GetAttribute(mediaType)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known media type
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string? ShortName(this MediaType? mediaType) => AttributeHelper<MediaType?>.GetAttribute(mediaType)?.ShortName;

        #endregion

        #region Region

        /// <summary>
        /// Get the Redump longnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? LongName(this Region region)
            => AttributeHelper<Region>.GetAttribute(region)?.LongName;

        /// <summary>
        /// Get the Redump longnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? LongName(this Region? region)
            => AttributeHelper<Region?>.GetAttribute(region)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortName(this Region region)
            => AttributeHelper<Region>.GetAttribute(region)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortName(this Region? region)
            => AttributeHelper<Region?>.GetAttribute(region)?.ShortName;

        /// <summary>
        /// Get the Region enum value for a given string
        /// </summary>
        /// <param name="region">String value to convert</param>
        /// <returns>Region represented by the string, if possible</returns>
        public static Region? ToRegion(string region)
        {
            region = region.ToLowerInvariant();
            var regions = (Region[])Enum.GetValues(typeof(Region));

            int index = Array.FindIndex(regions, r => region == r.ShortName());
            if (index > -1)
                return regions[index];

            index = Array.FindIndex(regions, r => region == r.LongName());
            if (index > -1)
                return regions[index];

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
                if (shortName == null || longName == null)
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
            return siteCode switch
            {
                SiteCode.PostgapType => true,
                SiteCode.VCD => true,
                _ => false,
            };
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
            return siteCode switch
            {
                // Identifying Info
                SiteCode.AlternativeTitle => true,
                SiteCode.AlternativeForeignTitle => true,
                SiteCode.BBFCRegistrationNumber => true,
                SiteCode.CompatibleOS => true,
                SiteCode.DiscHologramID => true,
                SiteCode.DMIHash => true,
                SiteCode.DNASDiscID => true,
                SiteCode.Filename => true,
                SiteCode.Genre => true,
                SiteCode.InternalName => true,
                SiteCode.InternalSerialName => true,
                SiteCode.ISBN => true,
                SiteCode.ISSN => true,
                SiteCode.Multisession => true,
                SiteCode.PFIHash => true,
                SiteCode.PostgapType => true,
                SiteCode.PPN => true,
                SiteCode.RingNonZeroDataStart => true,
                SiteCode.Series => true,
                SiteCode.SSHash => true,
                SiteCode.SSVersion => true,
                SiteCode.UniversalHash => true,
                SiteCode.VCD => true,
                SiteCode.VFCCode => true,
                SiteCode.VolumeLabel => true,
                SiteCode.XeMID => true,
                SiteCode.XMID => true,

                // Publisher / Company IDs
                SiteCode.AcclaimID => true,
                SiteCode.ActivisionID => true,
                SiteCode.BandaiID => true,
                SiteCode.BethesdaID => true,
                SiteCode.CDProjektID => true,
                SiteCode.EidosID => true,
                SiteCode.ElectronicArtsID => true,
                SiteCode.FoxInteractiveID => true,
                SiteCode.GTInteractiveID => true,
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
                SiteCode.TechDemos => true,
                SiteCode.Videos => true,
                _ => false,
            };
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
            return siteCode switch
            {
                SiteCode.Extras => true,
                SiteCode.Filename => true,
                SiteCode.Games => true,
                SiteCode.GameFootage => true,
                SiteCode.Multisession => true,
                SiteCode.NetYarozeGames => true,
                SiteCode.Patches => true,
                SiteCode.PlayableDemos => true,
                SiteCode.RollingDemos => true,
                SiteCode.Savegames => true,
                SiteCode.TechDemos => true,
                SiteCode.Videos => true,
                _ => false,
            };
        }

        /// <summary>
        /// Get the HTML version for each known site code
        /// </summary>
        public static string? LongName(this SiteCode siteCode)
            => AttributeHelper<SiteCode>.GetAttribute(siteCode)?.LongName;

        /// <summary>
        /// Get the HTML version for each known site code
        /// </summary>
        public static string? LongName(this SiteCode? siteCode)
            => AttributeHelper<SiteCode?>.GetAttribute(siteCode)?.LongName;

        /// <summary>
        /// Get the short tag for each known site code
        /// </summary>
        public static string? ShortName(this SiteCode siteCode)
            => AttributeHelper<SiteCode>.GetAttribute(siteCode)?.ShortName;

        /// <summary>
        /// Get the short tag for each known site code
        /// </summary>
        public static string? ShortName(this SiteCode? siteCode)
            => AttributeHelper<SiteCode?>.GetAttribute(siteCode)?.ShortName;

        #endregion

        #region System

        /// <summary>
        /// Determine if a system is okay if it's not detected by Windows
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if Windows show see a disc when dumping, false otherwise</returns>
        public static bool DetectedByWindows(this RedumpSystem? system)
        {
            return system switch
            {
                // BIOS Sets
                RedumpSystem.MicrosoftXboxBIOS
                    or RedumpSystem.NintendoGameCubeBIOS
                    or RedumpSystem.SonyPlayStationBIOS
                    or RedumpSystem.SonyPlayStation2BIOS => false,

                // Disc-Based Consoles
                RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem
                    or RedumpSystem.BandaiPlaydiaQuickInteractiveSystem
                    or RedumpSystem.BandaiPippin
                    or RedumpSystem.HasbroVideoNow
                    or RedumpSystem.HasbroVideoNowColor
                    or RedumpSystem.HasbroVideoNowJr
                    or RedumpSystem.HasbroVideoNowXP
                    or RedumpSystem.NintendoGameCube
                    or RedumpSystem.NintendoWii
                    or RedumpSystem.NintendoWiiU
                    or RedumpSystem.Panasonic3DOInteractiveMultiplayer
                    or RedumpSystem.PhilipsCDi
                    or RedumpSystem.PhilipsCDiDigitalVideo
                    or RedumpSystem.PioneerLaserActive
                    or RedumpSystem.MarkerDiscBasedConsoleEnd => false,

                // Computers
                RedumpSystem.AppleMacintosh
                    or RedumpSystem.MarkerComputerEnd => false,

                // Arcade
                RedumpSystem.AmericanLaserGames3DO
                    or RedumpSystem.Atari3DO
                    or RedumpSystem.NewJatreCDi
                    or RedumpSystem.PanasonicM2
                    or RedumpSystem.MarkerArcadeEnd => false,

                // Other
                RedumpSystem.PlayStationGameSharkUpdates
                    or RedumpSystem.SuperAudioCD
                    or RedumpSystem.MarkerOtherEnd => false,

                null => false,
                _ => true,
            };
        }

        /// <summary>
        /// Determine if a system has reversed ringcodes
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system has reversed ringcodes, false otherwise</returns>
        public static bool HasReversedRingcodes(this RedumpSystem? system)
        {
            return system switch
            {
                RedumpSystem.SonyPlayStation2
                    or RedumpSystem.SonyPlayStation3
                    or RedumpSystem.SonyPlayStation4
                    or RedumpSystem.SonyPlayStation5
                    or RedumpSystem.SonyPlayStationPortable => true,
                _ => false,
            };
        }

        /// <summary>
        /// Determine if a system is considered audio-only
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system is audio-only, false otherwise</returns>
        /// <remarks>
        /// Philips CD-i should NOT be in this list. It's being included until there's a
        /// reasonable distinction between CD-i and CD-i ready on the database side.
        /// </remarks>
        public static bool IsAudio(this RedumpSystem? system)
        {
            return system switch
            {
                RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem
                    or RedumpSystem.AudioCD
                    or RedumpSystem.DVDAudio
                    or RedumpSystem.HasbroiONEducationalGamingSystem
                    or RedumpSystem.HasbroVideoNow
                    or RedumpSystem.HasbroVideoNowColor
                    or RedumpSystem.HasbroVideoNowJr
                    or RedumpSystem.HasbroVideoNowXP
                    or RedumpSystem.PhilipsCDi
                    or RedumpSystem.PlayStationGameSharkUpdates
                    or RedumpSystem.SuperAudioCD => true,
                _ => false,
            };
        }

        /// <summary>
        /// Determine if a system is a marker value
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system is a marker value, false otherwise</returns>
        public static bool IsMarker(this RedumpSystem system)
            => ((RedumpSystem?)system).IsMarker();

        /// <summary>
        /// Determine if a system is a marker value
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system is a marker value, false otherwise</returns>
        public static bool IsMarker(this RedumpSystem? system)
        {
            return system switch
            {
                RedumpSystem.MarkerArcadeEnd
                    or RedumpSystem.MarkerComputerEnd
                    or RedumpSystem.MarkerDiscBasedConsoleEnd
                    or RedumpSystem.MarkerOtherEnd => true,
                _ => false,
            };
        }

        /// <summary>
        /// Determine if a system is considered XGD
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>True if the system is XGD, false otherwise</returns>
        public static bool IsXGD(this RedumpSystem? system)
        {
            return system switch
            {
                RedumpSystem.MicrosoftXbox
                    or RedumpSystem.MicrosoftXbox360
                    or RedumpSystem.MicrosoftXboxOne
                    or RedumpSystem.MicrosoftXboxSeriesXS => true,
                _ => false,
            };
        }

        /// <summary>
        /// List all systems with their short usable names
        /// </summary>
        public static List<string> ListSystems()
        {
            var systems = (RedumpSystem[])Enum.GetValues(typeof(RedumpSystem));
            var knownSystems = Array.FindAll(systems, s => !s.IsMarker() && s.GetCategory() != SystemCategory.NONE);
            Array.Sort(knownSystems, (x, y) => (x.LongName() ?? string.Empty).CompareTo(y.LongName() ?? string.Empty));
            return [.. Array.ConvertAll(knownSystems, val => $"{val.ShortName()} - {val.LongName()}")];
        }

        /// <summary>
        /// Get the Redump longnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? LongName(this RedumpSystem system)
            => AttributeHelper<RedumpSystem>.GetAttribute(system)?.LongName;

        /// <summary>
        /// Get the Redump longnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? LongName(this RedumpSystem? system)
            => AttributeHelper<RedumpSystem?>.GetAttribute(system)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this RedumpSystem system)
            => AttributeHelper<RedumpSystem>.GetAttribute(system)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this RedumpSystem? system)
            => AttributeHelper<RedumpSystem?>.GetAttribute(system)?.ShortName;

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this RedumpSystem system)
            => ((RedumpSystem?)system).GetCategory();

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.Category ?? SystemCategory.NONE;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a decrypted keys pack
        /// </summary>
        public static bool HasDkeys(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetAttribute(system) as SystemAttribute)?.HasDkeys ?? false;

        /// <summary>
        /// Determine if a system has a decrypted keys pack
        /// </summary>
        public static bool HasDkeys(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.HasDkeys ?? false;

        /// <summary>
        /// Determine if a system has a GDI pack
        /// </summary>
        public static bool HasGdi(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetAttribute(system) as SystemAttribute)?.HasGdi ?? false;

        /// <summary>
        /// Determine if a system has a GDI pack
        /// </summary>
        public static bool HasGdi(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.HasGdi ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has an LSD pack
        /// </summary>
        public static bool HasLsd(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetAttribute(system) as SystemAttribute)?.HasLsd ?? false;

        /// <summary>
        /// Determine if a system has an LSD pack
        /// </summary>
        public static bool HasLsd(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.HasLsd ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetAttribute(system) as SystemAttribute)?.HasSbi ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetAttribute(system) as SystemAttribute)?.HasSbi ?? false;

        /// <summary>
        /// Get the RedumpSystem enum value for a given string
        /// </summary>
        /// <param name="system">String value to convert</param>
        /// <returns>RedumpSystem represented by the string, if possible</returns>
        public static RedumpSystem? ToRedumpSystem(string system)
        {
            switch (system?.ToLowerInvariant())
            {
                #region BIOS Sets

                case "xboxbios":
                case "xbox bios":
                case "xbox-bios":
                case "microsoftxboxbios":
                case "microsoftxbox bios":
                case "microsoft xbox bios":
                    return RedumpSystem.MicrosoftXboxBIOS;
                case "gcbios":
                case "gc bios":
                case "gc-bios":
                case "gamecubebios":
                case "ngcbios":
                case "ngc bios":
                case "nintendogamecubebios":
                case "nintendo gamecube bios":
                    return RedumpSystem.NintendoGameCubeBIOS;
                case "ps1bios":
                case "ps1 bios":
                case "psxbios":
                case "psx bios":
                case "psx-bios":
                case "playstationbios":
                case "playstation bios":
                case "sonyps1bios":
                case "sonyps1 bios":
                case "sony ps1 bios":
                case "sonypsxbios":
                case "sonypsx bios":
                case "sony psx bios":
                case "sonyplaystationbios":
                case "sonyplaystation bios":
                case "sony playstation bios":
                    return RedumpSystem.SonyPlayStationBIOS;
                case "ps2bios":
                case "ps2 bios":
                case "ps2-bios":
                case "playstation2bios":
                case "playstation2 bios":
                case "playstation 2 bios":
                case "sonyps2bios":
                case "sonyps2 bios":
                case "sony ps2 bios":
                case "sonyplaystation2bios":
                case "sonyplaystation2 bios":
                case "sony playstation 2 bios":
                    return RedumpSystem.SonyPlayStation2BIOS;

                #endregion

                #region Consoles

                case "ajcd":
                case "jaguar":
                case "jagcd":
                case "jaguarcd":
                case "jaguar cd":
                case "atarijaguar":
                case "atarijagcd":
                case "atarijaguarcd":
                case "atari jaguar cd":
                case "atari jaguar cd interactive multimedia system":
                    return RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem;
                case "qis":
                case "playdia":
                case "playdiaqis":
                case "playdiaquickinteractivesystem":
                case "bandaiplaydia":
                case "bandaiplaydiaquickinteractivesystem":
                case "bandai playdia quick interactive system":
                    return RedumpSystem.BandaiPlaydiaQuickInteractiveSystem;
                case "pippin":
                case "bandaipippin":
                case "bandai pippin":
                case "applepippin":
                case "apple pippin":
                case "bandaiapplepippin":
                case "bandai apple pippin":
                case "bandai / apple pippin":
                    return RedumpSystem.BandaiPippin;
                case "cd32":
                case "amigacd32":
                case "amiga cd32":
                case "commodoreamigacd32":
                case "commodore amiga cd32":
                    return RedumpSystem.CommodoreAmigaCD32;
                case "cdtv":
                case "amigacdtv":
                case "amiga cdtv":
                case "commodoreamigacdtv":
                case "commodore amiga cdtv":
                    return RedumpSystem.CommodoreAmigaCDTV;
                case "evosc":
                case "evo sc":
                case "evosmartconsole":
                case "evo smart console":
                case "envizionsevosc":
                case "envizion evo sc":
                case "envizionevosmartconsole":
                case "envizion evo smart console":
                    return RedumpSystem.EnvizionsEVOSmartConsole;
                case "fmtm":
                case "fmtownsmarty":
                case "fm towns marty":
                case "fujitsufmtownsmarty":
                case "fujitsu fm towns marty":
                    return RedumpSystem.FujitsuFMTownsMarty;
                case "ion":
                case "ionegs":
                case "hasbroion":
                case "hasbroioneducationalgamingsystem":
                case "hasbro ion educational gaming system":
                    return RedumpSystem.HasbroiONEducationalGamingSystem;
                case "hvn":
                case "videonow":
                case "hasbrovideonow":
                case "hasbro videonow":
                    return RedumpSystem.HasbroVideoNow;
                case "hvnc":
                case "videonowcolor":
                case "videonow color":
                case "hasbrovideonowcolor":
                case "hasbro videonow color":
                    return RedumpSystem.HasbroVideoNowColor;
                case "hvnjr":
                case "videonowjr":
                case "videonow jr":
                case "hasbrovideonowjr":
                case "hasbro videonow jr":
                case "hasbro videonow jr.":
                    return RedumpSystem.HasbroVideoNowJr;
                case "xvnxp":
                case "videonowxp":
                case "videonow xp":
                case "hasbrovideonowxp":
                case "hasbro videonow xp":
                    return RedumpSystem.HasbroVideoNowXP;
                case "ixl":
                case "mattelixl":
                case "mattel ixl":
                case "fisherpriceixl":
                case "fisher price ixl":
                case "fisher-price ixl":
                case "fisherprice ixl":
                case "mattelfisherpriceixl":
                case "mattel fisher price ixl":
                case "mattelfisherprice ixl":
                case "mattel fisherprice ixl":
                case "mattel fisher-price ixl":
                    return RedumpSystem.MattelFisherPriceiXL;
                case "hs":
                case "hyperscan":
                case "mattelhyperscan":
                case "mattel hyperscan":
                    return RedumpSystem.MattelHyperScan;
                case "xbox":
                case "microsoftxbox":
                case "microsoft xbox":
                    return RedumpSystem.MicrosoftXbox;
                case "x360":
                case "xbox360":
                case "microsoftx360":
                case "microsoftxbox360":
                case "microsoft x360":
                case "microsoft xbox 360":
                    return RedumpSystem.MicrosoftXbox360;
                case "xb1":
                case "xbone":
                case "xboxone":
                case "microsoftxbone":
                case "microsoftxboxone":
                case "microsoft xbone":
                case "microsoft xbox one":
                    return RedumpSystem.MicrosoftXboxOne;
                case "xbs":
                case "xboxsx":
                case "xbseries":
                case "xbseriess":
                case "xbseriesx":
                case "xbseriessx":
                case "xboxseries":
                case "xboxseriess":
                case "xboxseriesx":
                case "xboxseriesxs":
                case "microsoftxboxseries":
                case "microsoftxboxseriess":
                case "microsoftxboxseriesx":
                case "microsoftxboxseriesxs":
                case "microsoft xbox series":
                case "microsoft xbox series s":
                case "microsoft xbox series x":
                case "microsoft xbox series x and s":
                    return RedumpSystem.MicrosoftXboxSeriesXS;
                case "pce":
                case "pcecd":
                case "pce-cd":
                case "tgcd":
                case "tg-cd":
                case "necpcecd":
                case "nectgcd":
                case "nec pc-engine cd":
                case "nec turbografx cd":
                case "nec pc-engine / turbografx cd":
                case "nec pc-engine cd & turbografx cd":
                    return RedumpSystem.NECPCEngineCDTurboGrafxCD;
                case "pcfx":
                case "pc-fx":
                case "pcfxga":
                case "pc-fxga":
                case "necpcfx":
                case "necpcfxga":
                case "nec pc-fx":
                case "nec pc-fxga":
                case "nec pc-fx / pc-fxga":
                case "nec pc-fx & pc-fxga":
                    return RedumpSystem.NECPCFXPCFXGA;
                case "gc":
                case "gamecube":
                case "ngc":
                case "nintendogamecube":
                case "nintendo gamecube":
                    return RedumpSystem.NintendoGameCube;
                case "snescd":
                case "snes cd":
                case "snes-cd":
                case "supernescd":
                case "super nes cd":
                case "super nes-cd":
                case "supernintendocd":
                case "super nintendo cd":
                case "super nintendo-cd":
                case "nintendosnescd":
                case "nintendo snes cd":
                case "nintendosnes-cd":
                case "nintendosupernescd":
                case "nintendo super nes cd":
                case "nintendo super nes-cd":
                case "nintendosupernintendocd":
                case "nintendo super nintendo cd":
                case "nintendo super nintendo-cd":
                case "sonysnescd":
                case "sony snes cd":
                case "sonysnes-cd":
                case "sonysupernescd":
                case "sony super nes cd":
                case "sony super nes-cd":
                case "sonysupernintendocd":
                case "sony super nintendo cd":
                case "sony super nintendo-cd":
                case "nintendo-sony super nes cd-rom system":
                    return RedumpSystem.NintendoSonySuperNESCDROMSystem;
                case "wii":
                case "nintendowii":
                case "nintendo wii":
                    return RedumpSystem.NintendoWii;
                case "wiiu":
                case "wii u":
                case "nintendowiiu":
                case "nintendo wii u":
                    return RedumpSystem.NintendoWiiU;
                case "3do":
                case "3do interactive multiplayer":
                case "panasonic3do":
                case "panasonic 3do":
                case "panasonic 3do interactive multiplayer":
                    return RedumpSystem.Panasonic3DOInteractiveMultiplayer;
                case "cdi":
                case "cd-i":
                case "philipscdi":
                case "philips cdi":
                case "philips cd-i":
                    return RedumpSystem.PhilipsCDi;
                case "laseractive":
                case "pioneerlaseractive":
                case "pioneer laseractive":
                    return RedumpSystem.PioneerLaserActive;
                case "scd":
                case "mcd":
                case "smcd":
                case "segacd":
                case "megacd":
                case "segamegacd":
                case "sega cd":
                case "mega cd":
                case "sega cd / mega cd":
                case "sega mega cd & sega cd":
                    return RedumpSystem.SegaMegaCDSegaCD;
                case "dc":
                case "sdc":
                case "dreamcast":
                case "segadreamcast":
                case "sega dreamcast":
                    return RedumpSystem.SegaDreamcast;
                case "ss":
                case "saturn":
                case "segasaturn":
                case "sega saturn":
                    return RedumpSystem.SegaSaturn;
                case "ngcd":
                case "neogeocd":
                case "neogeo cd":
                case "neo geo cd":
                case "snk ngcd":
                case "snk neogeo cd":
                case "snk neo geo cd":
                    return RedumpSystem.SNKNeoGeoCD;
                case "ps1":
                case "psx":
                case "playstation":
                case "sonyps1":
                case "sony ps1":
                case "sonypsx":
                case "sony psx":
                case "sonyplaystation":
                case "sony playstation":
                    return RedumpSystem.SonyPlayStation;
                case "ps2":
                case "playstation2":
                case "playstation 2":
                case "sonyps2":
                case "sony ps2":
                case "sonyplaystation2":
                case "sony playstation 2":
                    return RedumpSystem.SonyPlayStation2;
                case "ps3":
                case "playstation3":
                case "playstation 3":
                case "sonyps3":
                case "sony ps3":
                case "sonyplaystation3":
                case "sony playstation 3":
                    return RedumpSystem.SonyPlayStation3;
                case "ps4":
                case "playstation4":
                case "playstation 4":
                case "sonyps4":
                case "sony ps4":
                case "sonyplaystation4":
                case "sony playstation 4":
                    return RedumpSystem.SonyPlayStation4;
                case "ps5":
                case "playstation5":
                case "playstation 5":
                case "sonyps5":
                case "sony ps5":
                case "sonyplaystation5":
                case "sony playstation 5":
                    return RedumpSystem.SonyPlayStation5;
                case "psp":
                case "playstationportable":
                case "playstation portable":
                case "sonypsp":
                case "sony psp":
                case "sonyplaystationportable":
                case "sony playstation portable":
                    return RedumpSystem.SonyPlayStationPortable;
                case "vis":
                case "tandyvis":
                case "tandy vis":
                case "tandyvisualinformationsystem":
                case "tandy visual information system":
                case "memorexvis":
                case "memorex vis":
                case "memorexvisualinformationsystem":
                case "memorex visual information sytem":
                case "tandy / memorex visual information system":
                    return RedumpSystem.MemorexVisualInformationSystem;
                case "nuon":
                case "vmlabsnuon":
                case "vm labs nuon":
                    return RedumpSystem.VMLabsNUON;
                case "vflash":
                case "vsmile":
                case "vsmilepro":
                case "vsmile pro":
                case "v.flash":
                case "v.smile":
                case "v.smilepro":
                case "v.smile pro":
                case "vtechvflash":
                case "vtech vflash":
                case "vtech v.flash":
                case "vtechvsmile":
                case "vtech vsmile":
                case "vtech v.smile":
                case "vtechvsmilepro":
                case "vtech vsmile pro":
                case "vtech v.smile pro":
                case "vtech v.flash - v.smile pro":
                case "vtech v.flash & v.smile pro":
                    return RedumpSystem.VTechVFlashVSmilePro;
                case "gamewave":
                case "game wave":
                case "zapit":
                case "zapitgamewave":
                case "zapit game wave":
                case "zapit games game wave family entertainment system":
                    return RedumpSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem;

                #endregion

                #region Computers

                case "acorn":
                case "archcd":
                case "archimedes":
                case "acornarchimedes":
                case "acorn archimedes":
                    return RedumpSystem.AcornArchimedes;
                case "apple":
                case "mac":
                case "applemac":
                case "macintosh":
                case "applemacintosh":
                case "apple mac":
                case "apple macintosh":
                    return RedumpSystem.AppleMacintosh;
                case "acd":
                case "amiga":
                case "commodoreamiga":
                case "commodore amiga":
                case "commodore amiga cd":
                    return RedumpSystem.CommodoreAmigaCD;
                case "fmtowns":
                case "fmt":
                case "fm towns":
                case "fujitsufmtowns":
                case "fujitsu fm towns":
                case "fujitsu fm towns series":
                    return RedumpSystem.FujitsuFMTownsseries;
                case "ibm":
                case "ibmpc":
                case "pc":
                case "ibm pc":
                case "ibm pc compatible":
                    return RedumpSystem.IBMPCcompatible;
                case "pc88":
                case "pc-88":
                case "necpc88":
                case "nec pc88":
                case "nec pc-88":
                case "nec pc-88 series":
                    return RedumpSystem.NECPC88series;
                case "pc98":
                case "pc-98":
                case "necpc98":
                case "nec pc98":
                case "nec pc-98":
                case "nec pc-98 series":
                    return RedumpSystem.NECPC98series;
                case "x68k":
                case "x68kcd":
                case "x68000":
                case "sharpx68k":
                case "sharp x68k":
                case "sharpx68000":
                case "sharp x68000":
                    return RedumpSystem.SharpX68000;

                #endregion

                #region Arcade

                case "cubo":
                case "cubocd32":
                case "cubo cd32":
                case "amigacubocd32":
                case "amiga cubo cd32":
                    return RedumpSystem.AmigaCUBOCD32;
                case "alg3do":
                case "alg 3do":
                case "americanlasergames3do":
                case "american laser games 3do":
                    return RedumpSystem.AmericanLaserGames3DO;
                case "atari3do":
                case "atari 3do":
                    return RedumpSystem.Atari3DO;
                case "atronic":
                    return RedumpSystem.Atronic;
                case "auscom":
                case "auscomsystem1":
                case "auscom system 1":
                    return RedumpSystem.AUSCOMSystem1;
                case "gamemagic":
                case "game magic":
                case "ballygamemagic":
                case "bally game magic":
                    return RedumpSystem.BallyGameMagic;
                case "cps3":
                case "cpsiii":
                case "cps 3":
                case "cp system 3":
                case "cp system iii":
                case "capcomcps3":
                case "capcomcpsiii":
                case "capcom cps 3":
                case "capcom cps iii":
                case "capcom cp system 3":
                case "capcom cp system iii":
                    return RedumpSystem.CapcomCPSystemIII;
                case "fpp":
                case "funworldphotoplay":
                case "funworld photoplay":
                case "funworld photo play":
                    return RedumpSystem.funworldPhotoPlay;
                case "globalvr":
                case "global vr":
                case "global vr pc-based systems":
                    return RedumpSystem.GlobalVRVarious;
                case "vortek":
                case "globalvrvortek":
                case "global vr vortek":
                    return RedumpSystem.GlobalVRVortek;
                case "vortekv3":
                case "vortek v3":
                case "globalvrvortekv3":
                case "global vr vortek v3":
                    return RedumpSystem.GlobalVRVortekV3;
                case "ice":
                case "icepc":
                case "ice pc":
                case "ice pc-based hardware":
                    return RedumpSystem.ICEPCHardware;
                case "ite":
                case "iteagle":
                case "eagle":
                case "incredible technologies eagle":
                    return RedumpSystem.IncredibleTechnologiesEagle;
                case "itpc":
                case "incredible technologies pc-based systems":
                    return RedumpSystem.IncredibleTechnologiesVarious;
                case "jvlitouch":
                case "jvl itouch":
                    return RedumpSystem.JVLiTouch;
                case "kea":
                case "eamusement":
                case "e-amusement":
                case "konamieamusement":
                case "konami eamusement":
                case "konamie-amusement":
                case "konami e-amusement":
                    return RedumpSystem.KonamieAmusement;
                case "kfb":
                case "firebeat":
                case "konamifirebeat":
                case "konami firebeat":
                    return RedumpSystem.KonamiFireBeat;
                case "ksgv":
                case "gvsystem":
                case "gv system":
                case "konamigvsystem":
                case "konami gv system":
                case "systemgv":
                case "system gv":
                case "konamisystemgv":
                case "konami system gv":
                    return RedumpSystem.KonamiSystemGV;
                case "km2":
                case "konamim2":
                case "konami m2":
                    return RedumpSystem.KonamiM2;
                case "python":
                case "konamipython":
                case "konami python":
                    return RedumpSystem.KonamiPython;
                case "python2":
                case "python 2":
                case "konamipython2":
                case "konami python 2":
                    return RedumpSystem.KonamiPython2;
                case "ks573":
                case "system573":
                case "system 573":
                case "konamisystem573":
                case "konami system 573":
                    return RedumpSystem.KonamiSystem573;
                case "kt":
                case "twinkle":
                case "konamitwinkle":
                case "konami twinkle":
                    return RedumpSystem.KonamiTwinkle;
                case "konamipc":
                case "konami pc":
                case "konami pc-based systems":
                    return RedumpSystem.KonamiVarious;
                case "boardwalk":
                case "meritindustriesboardwalk":
                case "merit industries boardwalk":
                    return RedumpSystem.MeritIndustriesBoardwalk;
                case "megatouchforce":
                case "megatouch force":
                case "meritindustriesmegatouchforce":
                case "merit industries megatouch force":
                    return RedumpSystem.MeritIndustriesMegaTouchForce;
                case "megatouchion":
                case "megatouch ion":
                case "meritindustriesmegatouchion":
                case "merit industries megatouch ion":
                    return RedumpSystem.MeritIndustriesMegaTouchION;
                case "megatouchmaxx":
                case "megatouch maxx":
                case "meritindustriesmegatouchmaxx":
                case "merit industries megatouch maxx":
                    return RedumpSystem.MeritIndustriesMegaTouchMaxx;
                case "megatouchxl":
                case "megatouch xl":
                case "meritindustriesmegatouchxl":
                case "merit industries megatouch xl":
                    return RedumpSystem.MeritIndustriesMegaTouchXL;
                case "ns246":
                case "system246":
                case "system 246":
                case "namcosystem246":
                case "namco system 246":
                case "capcomsystem246":
                case "capcom system 246":
                case "taitosystem246":
                case "taito system 246":
                case "namco / capcom / taito system 246":
                case "system256":
                case "system 256":
                case "supersystem256":
                case "super system 256":
                case "namcosystem256":
                case "namco system 256":
                case "namcosupersystem256":
                case "namco super system 256":
                case "capcomsystem256":
                case "capcom system 256":
                case "capcomsupersystem256":
                case "capcom super system 256":
                case "namco / capcom system 256/super system 256":
                case "namco system 246 / system 256":
                    return RedumpSystem.NamcoSystem246256;
                case "triforce":
                case "namcotriforce":
                case "namco triforce":
                case "segatriforce":
                case "sega triforce":
                case "nintendotriforce":
                case "nintendo triforce":
                case "namco / sega / nintendo triforce":
                case "Namco · Sega · Nintendo Triforce":
                    return RedumpSystem.NamcoSegaNintendoTriforce;
                case "ns12":
                case "system12":
                case "system 12":
                case "namcosystem12":
                case "namco system 12":
                    return RedumpSystem.NamcoSystem12;
                case "newjatrecdi":
                case "new jatre cdi":
                case "new jatre cd-i":
                    return RedumpSystem.NewJatreCDi;
                case "hrs":
                case "highratesytem":
                case "high rate system":
                case "nichibutsuhrs":
                case "nichibutsu hrs":
                case "nichibutsu high rate system":
                    return RedumpSystem.NichibutsuHighRateSystem;
                case "supercd":
                case "super cd":
                case "nichibutsuscd":
                case "nichibutsu scd":
                case "nichibutsusupercd":
                case "nichibutsu supercd":
                case "nichibutsu super cd":
                    return RedumpSystem.NichibutsuSuperCD;
                case "xrs":
                case "xratesystem":
                case "x-rate system":
                case "nichibutsuxrs":
                case "nichibutsu xrs":
                case "nichibutsu x-rate system":
                    return RedumpSystem.NichibutsuXRateSystem;
                case "m2":
                case "panasonicm2":
                case "panasonic m2":
                    return RedumpSystem.PanasonicM2;
                case "photoplay":
                case "photoplaypc":
                case "photoplay pc":
                case "photoplay pc-based systems":
                    return RedumpSystem.PhotoPlayVarious;
                case "rawthrills":
                case "raw thrills":
                case "raw thrills pc-based systems":
                    return RedumpSystem.RawThrillsVarious;
                case "alls":
                case "segaalls":
                case "sega alls":
                    return RedumpSystem.SegaALLS;
                case "chihiro":
                case "segachihiro":
                case "sega chihiro":
                    return RedumpSystem.SegaChihiro;
                case "europar":
                case "europa-r":
                case "segaeuropar":
                case "sega europar":
                case "sega europa-r":
                    return RedumpSystem.SegaEuropaR;
                case "lindbergh":
                case "segalindbergh":
                case "sega lindbergh":
                    return RedumpSystem.SegaLindbergh;
                case "naomi":
                case "seganaomi":
                case "sega naomi":
                    return RedumpSystem.SegaNaomi;
                case "naomi2":
                case "naomi 2":
                case "seganaomi2":
                case "sega naomi 2":
                    return RedumpSystem.SegaNaomi2;
                case "nu":
                case "seganu":
                case "sega nu":
                    return RedumpSystem.SegaNu;
                case "sre":
                case "ringedge":
                case "segaringedge":
                case "sega ringedge":
                    return RedumpSystem.SegaRingEdge;
                case "sre2":
                case "ringedge2":
                case "ringedge 2":
                case "segaringedge2":
                case "sega ringedge 2":
                    return RedumpSystem.SegaRingEdge2;
                case "ringwide":
                case "segaringwide":
                case "sega ringwide":
                    return RedumpSystem.SegaRingWide;
                case "stv":
                case "titanvideo":
                case "titan video":
                case "segatitanvideo":
                case "sega titan video":
                    return RedumpSystem.SegaTitanVideo;
                case "system32":
                case "system 32":
                case "segasystem32":
                case "sega system 32":
                    return RedumpSystem.SegaSystem32;
                case "cats":
                case "seibucats":
                case "seibu cats":
                case "seibu cats system":
                    return RedumpSystem.SeibuCATSSystem;
                case "quizard":
                case "tabaustriaquizard":
                case "tab-austria quizard":
                    return RedumpSystem.TABAustriaQuizard;
                case "tsumo":
                case "tsunamitsumo":
                case "tsunami tsumo":
                case "tsunami tsumo multi-game motion system":
                    return RedumpSystem.TsunamiTsuMoMultiGameMotionSystem;
                case "ultracade":
                case "ultracadepc":
                case "ultracade pc":
                case "ultracade pc-based systems":
                    return RedumpSystem.UltraCade;

                #endregion

                #region Others

                case "audio":
                case "audiocd":
                case "audio cd":
                case "audio-cd":
                    return RedumpSystem.AudioCD;
                case "bdvideo":
                case "bd-video":
                case "blurayvideo":
                case "bluray video":
                    return RedumpSystem.BDVideo;
                case "dvda":
                case "dvdaudio":
                case "dvd-audio":
                    return RedumpSystem.DVDAudio;
                case "dvd":
                case "dvdv":
                case "dvdvideo":
                case "dvd-video":
                    return RedumpSystem.DVDVideo;
                case "enhancedcd":
                case "enhanced cd":
                case "enhanced-cd":
                case "enhancedcdrom":
                case "enhanced cdrom":
                case "enhanced cd-rom":
                    return RedumpSystem.EnhancedCD;
                case "hddvd":
                case "hddvdv":
                case "hddvdvideo":
                case "hddvd-video":
                case "hd-dvd-video":
                case "hd dvd-video":
                    return RedumpSystem.HDDVDVideo;
                case "navi21":
                case "naviken":
                case "naviken21":
                case "naviken 2.1":
                case "navisoftnaviken":
                case "navisoft naviken":
                case "navisoftnaviken21":
                case "navisoft naviken 2.1":
                    return RedumpSystem.NavisoftNaviken21;
                case "palm":
                case "palmos":
                case "palm os":
                    return RedumpSystem.PalmOS;
                case "photo":
                case "photocd":
                case "photo cd":
                case "photo-cd":
                    return RedumpSystem.PhotoCD;
                case "psxgs":
                case "gameshark":
                case "psgameshark":
                case "ps gameshark":
                case "playstationgameshark":
                case "playstation gameshark":
                case "playstation gameshark updates":
                    return RedumpSystem.PlayStationGameSharkUpdates;
                case "pocketpc":
                case "pocket pc":
                case "ppc":
                    return RedumpSystem.PocketPC;
                case "rainbow":
                case "rainbowdisc":
                case "rainbow disc":
                    return RedumpSystem.RainbowDisc;
                case "sp21":
                case "pl21":
                case "prologue21":
                case "prologue 21":
                case "segaprologue21":
                case "sega prologue21":
                case "sega prologue 21":
                case "sega prologue 21 multimedia karaoke system":
                    return RedumpSystem.SegaPrologue21MultimediaKaraokeSystem;
                case "electronicbook":
                case "sonyelectronicbook":
                case "sony electronic book":
                    return RedumpSystem.SonyElectronicBook;
                case "sacd":
                case "superaudiocd":
                case "super audio cd":
                    return RedumpSystem.SuperAudioCD;
                case "iktv":
                case "taoiktv":
                case "tao iktv":
                    return RedumpSystem.TaoiKTV;
                case "ksite":
                case "kisssite":
                case "kiss-site":
                case "tomykisssite":
                case "tomy kisssite":
                case "tomy kiss-site":
                    return RedumpSystem.TomyKissSite;
                case "vcd":
                case "videocd":
                case "video cd":
                    return RedumpSystem.VideoCD;

                #endregion

                default:
                    return null;
            }
        }

        #endregion

        #region System Category

        /// <summary>
        /// Get the string representation of the system category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string? LongName(this SystemCategory? category)
            => AttributeHelper<SystemCategory?>.GetAttribute(category)?.LongName;

        #endregion

        #region Yes/No

        /// <summary>
        /// Get the string representation of the YesNo value
        /// </summary>
        /// <param name="yesno"></param>
        /// <returns></returns>
        public static string LongName(this YesNo? yesno)
            => AttributeHelper<YesNo?>.GetAttribute(yesno)?.LongName ?? "Yes/No";

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
        public static YesNo? ToYesNo(string yesno)
        {
            return (yesno?.ToLowerInvariant()) switch
            {
                "no" => YesNo.No,
                "yes" => YesNo.Yes,
                _ => YesNo.NULL,
            };
        }

        #endregion
    }
}
