using System;
using System.Collections.Generic;
using SabreTools.RedumpLib.Attributes;
using SabreTools.RedumpLib.Data;

namespace SabreTools.RedumpLib.RedumpOrg.Data
{
    /// <summary>
    /// Information pertaining to Redump systems
    /// </summary>
#pragma warning disable IDE0010
#pragma warning disable IDE0072
    public static class Extensions
    {
        #region Cross-Enumeration

        /// <summary>
        /// Get a list of valid MediaTypes for a given RedumpSystem
        /// </summary>
        /// <param name="system">RedumpSystem value to check</param>
        /// <returns>MediaTypes, if possible</returns>
        public static List<PhysicalMediaType?> MediaTypes(this RedumpSystem? system)
        {
            var types = new List<PhysicalMediaType?>();

            switch (system)
            {
                #region Consoles

                // https://en.wikipedia.org/wiki/Atari_Jaguar_CD
                case RedumpSystem.AtariJaguarCDInteractiveMultimediaSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Playdia
                case RedumpSystem.BandaiPlaydiaQuickInteractiveSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Apple_Bandai_Pippin
                case RedumpSystem.BandaiPippin:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Amiga_CD32
                case RedumpSystem.CommodoreAmigaCD32:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Commodore_CDTV
                case RedumpSystem.CommodoreAmigaCDTV:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/EVO_Smart_Console
                case RedumpSystem.EnvizionsEVOSmartConsole:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/FM_Towns_Marty
                case RedumpSystem.FujitsuFMTownsMarty:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // http://videogamekraken.com/ion-educational-gaming-system-by-hasbro
                case RedumpSystem.HasbroiONEducationalGamingSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case RedumpSystem.HasbroVideoNow:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case RedumpSystem.HasbroVideoNowColor:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case RedumpSystem.HasbroVideoNowJr:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/VideoNow
                case RedumpSystem.HasbroVideoNowXP:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                case RedumpSystem.MattelFisherPriceiXL:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/HyperScan
                case RedumpSystem.MattelHyperScan:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_(console)
                case RedumpSystem.MicrosoftXbox:
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_360
                case RedumpSystem.MicrosoftXbox360:
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_One
                case RedumpSystem.MicrosoftXboxOne:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/Xbox_Series_X_and_Series_S
                case RedumpSystem.MicrosoftXboxSeriesXS:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/TurboGrafx-16
                case RedumpSystem.NECPCEngineCDTurboGrafxCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PC-FX
                case RedumpSystem.NECPCFXPCFXGA:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/GameCube
                case RedumpSystem.NintendoGameCube:
                    types.Add(PhysicalMediaType.DVD); // Only added here to help users; not strictly correct
                    types.Add(PhysicalMediaType.NintendoGameCubeGameDisc);
                    break;

                // https://en.wikipedia.org/wiki/Super_NES_CD-ROM
                case RedumpSystem.NintendoSonySuperNESCDROMSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Wii
                case RedumpSystem.NintendoWii:
                    types.Add(PhysicalMediaType.DVD); // Only added here to help users; not strictly correct
                    types.Add(PhysicalMediaType.NintendoWiiOpticalDisc);
                    break;

                // https://en.wikipedia.org/wiki/Wii_U
                case RedumpSystem.NintendoWiiU:
                    types.Add(PhysicalMediaType.NintendoWiiUOpticalDisc);
                    break;

                // https://en.wikipedia.org/wiki/3DO_Interactive_Multiplayer
                case RedumpSystem.Panasonic3DOInteractiveMultiplayer:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Philips_CD-i
                case RedumpSystem.PhilipsCDi:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Polymega
                case RedumpSystem.PlaymajiPolymega:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/LaserActive
                case RedumpSystem.PioneerLaserActive:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.LaserDisc);
                    break;

                // https://en.wikipedia.org/wiki/Sega_CD
                case RedumpSystem.SegaMegaCDSegaCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Dreamcast
                case RedumpSystem.SegaDreamcast:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition, MIL-CD
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // https://en.wikipedia.org/wiki/Sega_Saturn
                case RedumpSystem.SegaSaturn:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Neo_Geo_CD
                case RedumpSystem.SNKNeoGeoCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_(console)
                case RedumpSystem.SonyPlayStation:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_2
                case RedumpSystem.SonyPlayStation2:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_3
                case RedumpSystem.SonyPlayStation3:
                    types.Add(PhysicalMediaType.BluRay);
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_4
                case RedumpSystem.SonyPlayStation4:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_5
                case RedumpSystem.SonyPlayStation5:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/PlayStation_Portable
                case RedumpSystem.SonyPlayStationPortable:
                    types.Add(PhysicalMediaType.UMD);
                    types.Add(PhysicalMediaType.CDROM); // Development discs only
                    types.Add(PhysicalMediaType.DVD); // Development discs only
                    break;

                // https://en.wikipedia.org/wiki/Tandy_Video_Information_System
                case RedumpSystem.MemorexVisualInformationSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Nuon_(DVD_technology)
                case RedumpSystem.VMLabsNUON:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/V.Flash
                case RedumpSystem.VTechVFlashVSmilePro:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Game_Wave_Family_Entertainment_System
                case RedumpSystem.ZAPiTGamesGameWaveFamilyEntertainmentSystem:
                    types.Add(PhysicalMediaType.CDROM); // Firmware discs only(?)
                    types.Add(PhysicalMediaType.DVD);
                    break;

                #endregion

                #region Computers

                // https://en.wikipedia.org/wiki/Acorn_Archimedes
                case RedumpSystem.AcornArchimedes:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/Macintosh
                case RedumpSystem.AppleMacintosh:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    types.Add(PhysicalMediaType.HardDisk);
                    break;

                // https://en.wikipedia.org/wiki/Amiga
                case RedumpSystem.CommodoreAmigaCD:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/FM_Towns
                case RedumpSystem.FujitsuFMTownsseries:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/IBM_PC_compatible
                case RedumpSystem.IBMPCcompatible:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.BluRay);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    types.Add(PhysicalMediaType.HardDisk);
                    types.Add(PhysicalMediaType.DataCartridge);
                    break;

                // https://en.wikipedia.org/wiki/PC-8800_series
                case RedumpSystem.NECPC88series:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/PC-9800_series
                case RedumpSystem.NECPC98series:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                // https://en.wikipedia.org/wiki/X68000
                case RedumpSystem.SharpX68000:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.FloppyDisk);
                    break;

                #endregion

                #region Arcade

                // https://www.bigbookofamigahardware.com/bboah/product.aspx?id=36
                case RedumpSystem.AmigaCUBOCD32:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Orbatak
                case RedumpSystem.AmericanLaserGames3DO:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=779
                case RedumpSystem.Atari3DO:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://newlifegames.net/nlg/index.php?topic=22003.0
                // http://newlifegames.net/nlg/index.php?topic=5486.msg119440
                case RedumpSystem.Atronic:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://www.arcade-museum.com/members/member_detail.php?member_id=406530
                case RedumpSystem.AUSCOMSystem1:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://newlifegames.net/nlg/index.php?topic=285.0
                case RedumpSystem.BallyGameMagic:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/CP_System_III
                case RedumpSystem.CapcomCPSystemIII:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.funworldPhotoPlay:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/FuRyu
                case RedumpSystem.FuRyuOmronPurikura:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // UNKNOWN
                case RedumpSystem.GlobalVRVarious:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://service.globalvr.com/troubleshooting/vortek.html
                case RedumpSystem.GlobalVRVortek:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://service.globalvr.com/downloads/v3/040-1001-01c-V3-System-Manual.pdf
                case RedumpSystem.GlobalVRVortekV3:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://www.icegame.com/games
                case RedumpSystem.ICEPCHardware:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://github.com/mamedev/mame/blob/master/src/mame/drivers/iteagle.cpp
                case RedumpSystem.IncredibleTechnologiesEagle:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.IncredibleTechnologiesVarious:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // UNKNOWN
                case RedumpSystem.JVLiTouch:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/E-Amusement
                case RedumpSystem.KonamieAmusement:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=828
                case RedumpSystem.KonamiFireBeat:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=577
                case RedumpSystem.KonamiSystemGV:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=575
                case RedumpSystem.KonamiM2:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=586
                // http://system16.com/hardware.php?id=977
                case RedumpSystem.KonamiPython:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=976
                // http://system16.com/hardware.php?id=831
                case RedumpSystem.KonamiPython2:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=582
                // http://system16.com/hardware.php?id=822
                // http://system16.com/hardware.php?id=823
                case RedumpSystem.KonamiSystem573:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=827
                case RedumpSystem.KonamiTwinkle:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.KonamiVarious:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://www.meritgames.com/Support_Center/manuals/PM0591-01.pdf
                case RedumpSystem.MeritIndustriesBoardwalk:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://www.meritgames.com/Support_Center/Force%20Elite/PM0380-09.pdf
                // http://www.meritgames.com/Support_Center/Force%20Upright/PM0382-07%20FORCE%20Upright%20manual.pdf
                // http://www.meritgames.com/Support_Center/Force%20Upright/PM0383-07%20FORCE%20Upright%20manual.pdf
                case RedumpSystem.MeritIndustriesMegaTouchForce:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://www.meritgames.com/Service%20Center/Ion%20Troubleshooting.pdf
                case RedumpSystem.MeritIndustriesMegaTouchION:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://www.meritgames.com/Support_Center/EZ%20Maxx/Manuals/MAXX%20Elite%20with%20coin.pdf
                // http://www.meritgames.com/Support_Center/EZ%20Maxx/Manuals/MAXX%20Elite.pdf
                // http://www.meritgames.com/Support_Center/manuals/90003010%20Maxx%20TSM_Rev%20C.pdf
                case RedumpSystem.MeritIndustriesMegaTouchMaxx:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://www.meritgames.com/Support_Center/manuals/pm0076_OA_Megatouch%20XL%20Trouble%20Shooting%20Manual.pdf
                // http://www.meritgames.com/Support_Center/MEGA%20XL/manuals/Megatouch_XL_pm0109-0D.pdf
                // http://www.meritgames.com/Support_Center/MEGA%20XL/manuals/Megatouch_XL_Super_5000_manual.pdf
                case RedumpSystem.MeritIndustriesMegaTouchXL:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.NamcoPurikura:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=543
                // http://system16.com/hardware.php?id=546
                // http://system16.com/hardware.php?id=872
                case RedumpSystem.NamcoSystem246256:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=545
                case RedumpSystem.NamcoSegaNintendoTriforce:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // http://system16.com/hardware.php?id=535
                case RedumpSystem.NamcoSystem12:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://www.arcade-history.com/?n=the-yakyuuken-part-1&page=detail&id=33049
                case RedumpSystem.NewJatreCDi:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://blog.system11.org/?p=2499
                case RedumpSystem.NichibutsuHighRateSystem:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://blog.system11.org/?p=2514
                case RedumpSystem.NichibutsuSuperCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://collectedit.com/collectors/shou-time-213/arcade-pcbs-281/x-rate-dvd-series-17-newlywed-life-japan-by-nichibutsu-32245
                case RedumpSystem.NichibutsuXRateSystem:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/Panasonic_M2
                case RedumpSystem.PanasonicM2:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://github.com/mamedev/mame/blob/master/src/mame/drivers/photoply.cpp
                case RedumpSystem.PhotoPlayVarious:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.RawThrillsVarious:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // UNKNOWN
                case RedumpSystem.SegaALLS:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=729
                case RedumpSystem.SegaChihiro:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // http://system16.com/hardware.php?id=907
                case RedumpSystem.SegaEuropaR:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=985
                // http://system16.com/hardware.php?id=731
                // http://system16.com/hardware.php?id=984
                // http://system16.com/hardware.php?id=986
                case RedumpSystem.SegaLindbergh:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=721
                // http://system16.com/hardware.php?id=723
                // http://system16.com/hardware.php?id=906
                // http://system16.com/hardware.php?id=722
                case RedumpSystem.SegaNaomi:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // http://system16.com/hardware.php?id=725
                // http://system16.com/hardware.php?id=726
                // http://system16.com/hardware.php?id=727
                case RedumpSystem.SegaNaomi2:
                    types.Add(PhysicalMediaType.CDROM); // Low density partition
                    types.Add(PhysicalMediaType.GDROM); // High density partition
                    break;

                // https://segaretro.org/Sega_NAOMI#NAOMI_Satellite_Terminal
                case RedumpSystem.SegaNaomiSatelliteTerminalPC:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
                case RedumpSystem.SegaNu:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
                case RedumpSystem.SegaNu11:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Nu
                case RedumpSystem.SegaNu2:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://segaretro.org/Nu_SX
                case RedumpSystem.SegaNuSX:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=910
                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
                case RedumpSystem.SegaRingEdge:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=982
                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
                case RedumpSystem.SegaRingEdge2:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=911
                // https://en.wikipedia.org/wiki/List_of_Sega_arcade_system_boards#Sega_Ring_series
                case RedumpSystem.SegaRingWide:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // http://system16.com/hardware.php?id=711
                case RedumpSystem.SegaTitanVideo:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://system16.com/hardware.php?id=709
                // http://system16.com/hardware.php?id=710
                case RedumpSystem.SegaSystem32:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://github.com/mamedev/mame/blob/master/src/mame/drivers/seibucats.cpp
                case RedumpSystem.SeibuCATSSystem:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://www.tab.at/en/support/support/downloads
                case RedumpSystem.TABAustriaQuizard:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://primetimeamusements.com/product/tsumo-multi-game-motion-system/
                // https://www.highwaygames.com/arcade-machines/tsumo-tsunami-motion-8117/
                case RedumpSystem.TsunamiTsuMoMultiGameMotionSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/UltraCade_Technologies
                case RedumpSystem.UltraCade:
                    types.Add(PhysicalMediaType.CDROM);
                    types.Add(PhysicalMediaType.DVD);
                    break;

                #endregion

                #region Others

                // https://en.wikipedia.org/wiki/Audio_CD
                case RedumpSystem.AudioCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Blu-ray#Player_profiles
                case RedumpSystem.BDVideo:
                    types.Add(PhysicalMediaType.BluRay);
                    break;

                // https://en.wikipedia.org/wiki/DVD-Audio
                case RedumpSystem.DVDAudio:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/DVD-Video
                case RedumpSystem.DVDVideo:
                    types.Add(PhysicalMediaType.DVD);
                    break;

                // https://en.wikipedia.org/wiki/Blue_Book_(CD_standard)
                case RedumpSystem.EnhancedCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/HD_DVD
                case RedumpSystem.HDDVDVideo:
                    types.Add(PhysicalMediaType.HDDVD);
                    break;

                // UNKNOWN
                case RedumpSystem.NavisoftNaviken21:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.PalmOS:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Photo_CD
                case RedumpSystem.PhotoCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.PlayStationGameSharkUpdates:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.PocketPC:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.Psion:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Doors_and_Windows_(EP)
                case RedumpSystem.RainbowDisc:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://segaretro.org/Prologue_21
                case RedumpSystem.SegaPrologue21MultimediaKaraokeSystem:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.SharpZaurus:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // UNKNOWN
                case RedumpSystem.SonyElectronicBook:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Super_Audio_CD
                case RedumpSystem.SuperAudioCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://www.cnet.com/products/tao-music-iktv-karaoke-station-karaoke-system-series/
                case RedumpSystem.TaoiKTV:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // http://ultimateconsoledatabase.com/golden/kiss_site.htm
                case RedumpSystem.TomyKissSite:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                // https://en.wikipedia.org/wiki/Video_CD
                case RedumpSystem.VideoCD:
                    types.Add(PhysicalMediaType.CDROM);
                    break;

                #endregion

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
        public static DiscType? ToDiscType(this PhysicalMediaType? mediaType)
        {
            return mediaType switch
            {
                PhysicalMediaType.BluRay => DiscType.BD50,
                PhysicalMediaType.CDROM => DiscType.CD,
                PhysicalMediaType.DVD => DiscType.DVD9,
                PhysicalMediaType.GDROM => DiscType.GDROM,
                PhysicalMediaType.HDDVD => DiscType.HDDVDSL,
                // PhysicalMediaType.MILCD => DiscType.MILCD, // TODO: Support this?
                PhysicalMediaType.NintendoGameCubeGameDisc => DiscType.NintendoGameCubeGameDisc,
                PhysicalMediaType.NintendoWiiOpticalDisc => DiscType.NintendoWiiOpticalDiscDL,
                PhysicalMediaType.NintendoWiiUOpticalDisc => DiscType.NintendoWiiUOpticalDiscSL,
                PhysicalMediaType.UMD => DiscType.UMDDL,
                _ => null,
            };
        }

        /// <summary>
        /// Convert currently known Redump disc types to master list of all media types
        /// </summary>
        /// <param name="discType">DiscType value to check</param>
        /// <returns>MediaType if possible, null on error</returns>
        public static PhysicalMediaType? ToMediaType(this DiscType? discType)
        {
            return discType switch
            {
                DiscType.BD25
                    or DiscType.BD33
                    or DiscType.BD50
                    or DiscType.BD66
                    or DiscType.BD100
                    or DiscType.BD128 => PhysicalMediaType.BluRay,
                DiscType.CD => PhysicalMediaType.CDROM,
                DiscType.DVD5
                    or DiscType.DVD9 => PhysicalMediaType.DVD,
                DiscType.GDROM => PhysicalMediaType.GDROM,
                DiscType.HDDVDSL
                    or DiscType.HDDVDDL => PhysicalMediaType.HDDVD,
                // DiscType.MILCD => PhysicalMediaType.MILCD, // TODO: Support this?
                DiscType.NintendoGameCubeGameDisc => PhysicalMediaType.NintendoGameCubeGameDisc,
                DiscType.NintendoWiiOpticalDiscSL
                    or DiscType.NintendoWiiOpticalDiscDL => PhysicalMediaType.NintendoWiiOpticalDisc,
                DiscType.NintendoWiiUOpticalDiscSL => PhysicalMediaType.NintendoWiiUOpticalDisc,
                DiscType.UMDSL
                    or DiscType.UMDDL => PhysicalMediaType.UMD,
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

        #region Disc Type

        /// <summary>
        /// Get the Redump longnames for each known disc type
        /// </summary>
        /// <param name="discType"></param>
        /// <returns></returns>
        public static string? LongName(this DiscType discType)
            => ((DiscType?)discType).LongName();

        /// <summary>
        /// Get the Redump longnames for each known disc type
        /// </summary>
        /// <param name="discType"></param>
        /// <returns></returns>
        public static string? LongName(this DiscType? discType)
            => AttributeHelper<DiscType?>.GetHumanReadableAttribute(discType)?.LongName;

        /// <summary>
        /// Get the DiscType enum value for a given string
        /// </summary>
        /// <param name="discType">String value to convert</param>
        /// <returns>DiscType represented by the string, if possible</returns>
        public static DiscType? ToDiscType(this string? discType)
        {
            // No value means no match
            if (discType is null || discType.Length == 0)
                return null;

            discType = discType.ToLowerInvariant();
            var discTypes = (DiscType[])Enum.GetValues(typeof(DiscType));

            // Check long names
            int index = Array.FindIndex(discTypes, s => discType == s.LongName()?.ToLowerInvariant()
                || discType == s.LongName()?.Replace(" ", string.Empty)?.ToLowerInvariant()
                || discType == s.LongName()?.Replace("-", string.Empty)?.ToLowerInvariant()
                || discType == s.LongName()?
                    .Replace(" ", string.Empty)?
                    .Replace("-", string.Empty)?
                    .ToLowerInvariant());
            if (index > -1)
                return discTypes[index];

            // Special cases
            return (discType?.ToLowerInvariant()) switch
            {
                "bd"
                    or "bdrom"
                    or "bd-rom"
                    or "bluray"
                    or "blu-ray" => DiscType.BD25,
                "cdrom"
                    or "cd-rom" => DiscType.CD,
                "dvd"
                    or "dvd-rom" => DiscType.DVD5,
                "gd" => DiscType.GDROM,
                "hddvd"
                    or "hd-dvd" => DiscType.HDDVDSL,
                "umd" => DiscType.UMDSL,

                _ => null,
            };
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

        #region Region

        /// <summary>
        /// Get the Redump longnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? LongName(this Region region)
            => AttributeHelper<Region>.GetHumanReadableAttribute(region)?.LongName;

        /// <summary>
        /// Get the Redump longnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? LongName(this Region? region)
            => AttributeHelper<Region?>.GetHumanReadableAttribute(region)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortName(this Region region)
            => AttributeHelper<Region>.GetHumanReadableAttribute(region)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string? ShortName(this Region? region)
            => AttributeHelper<Region?>.GetHumanReadableAttribute(region)?.ShortName;

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
            return siteCode switch
            {
                SiteCode.PCMacHybrid => true,
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
                SiteCode.SteamSimSidDepotID => true,
                SiteCode.SteamCsmCsdDepotID => true,
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
            => AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system)?.LongName;

        /// <summary>
        /// Get the Redump longnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? LongName(this RedumpSystem? system)
            => AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system)?.LongName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this RedumpSystem system)
            => AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system)?.ShortName;

        /// <summary>
        /// Get the Redump shortnames for each known system
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public static string? ShortName(this RedumpSystem? system)
            => AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system)?.ShortName;

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this RedumpSystem system)
            => ((RedumpSystem?)system).GetCategory();

        /// <summary>
        /// Determine the category of a system
        /// </summary>
        public static SystemCategory GetCategory(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.Category ?? SystemCategory.NONE;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is available in Redump yet
        /// </summary>
        public static bool IsAvailable(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.Available ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system is restricted to dumpers
        /// </summary>
        public static bool IsBanned(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.IsBanned ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a CUE pack
        /// </summary>
        public static bool HasCues(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasCues ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a DAT
        /// </summary>
        public static bool HasDat(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDat ?? false;

        /// <summary>
        /// Determine if a system has a decrypted keys pack
        /// </summary>
        public static bool HasDkeys(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDkeys ?? false;

        /// <summary>
        /// Determine if a system has a decrypted keys pack
        /// </summary>
        public static bool HasDkeys(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasDkeys ?? false;

        /// <summary>
        /// Determine if a system has a GDI pack
        /// </summary>
        public static bool HasGdi(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasGdi ?? false;

        /// <summary>
        /// Determine if a system has a GDI pack
        /// </summary>
        public static bool HasGdi(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasGdi ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has a keys pack
        /// </summary>
        public static bool HasKeys(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasKeys ?? false;

        /// <summary>
        /// Determine if a system has an LSD pack
        /// </summary>
        public static bool HasLsd(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasLsd ?? false;

        /// <summary>
        /// Determine if a system has an LSD pack
        /// </summary>
        public static bool HasLsd(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasLsd ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this RedumpSystem system)
            => (AttributeHelper<RedumpSystem>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasSbi ?? false;

        /// <summary>
        /// Determine if a system has an SBI pack
        /// </summary>
        public static bool HasSbi(this RedumpSystem? system)
            => (AttributeHelper<RedumpSystem?>.GetHumanReadableAttribute(system) as SystemAttribute)?.HasSbi ?? false;

        /// <summary>
        /// Get the RedumpSystem enum value for a given string
        /// </summary>
        /// <param name="system">String value to convert</param>
        /// <returns>RedumpSystem represented by the string, if possible</returns>
        public static RedumpSystem? ToRedumpSystem(this string? system)
        {
            // No value means no match
            if (system is null || system.Length == 0)
                return null;

            system = system.ToLowerInvariant();
            var redumpSystems = (RedumpSystem[])Enum.GetValues(typeof(RedumpSystem));

            // Check short names
            int index = Array.FindIndex(redumpSystems, s => system == s.ShortName()?.ToLowerInvariant());
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
#pragma warning restore IDE0010
#pragma warning restore IDE0072
}
