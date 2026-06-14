using SabreTools.RedumpLib.Attributes;

namespace SabreTools.RedumpLib.RedumpInfo.Data
{
    /// <summary>
    /// List of all disc subpaths
    /// </summary>
    public enum DiscSubpath
    {
        [HumanReadable(LongName = "Cuesheet", ShortName = "cue")]
        Cuesheet,

        [HumanReadable(LongName = "Edit", ShortName = "edit")]
        Edit,

        // Placeholder for the linked queue history page, not an actual subpath
        [HumanReadable(LongName = "History", ShortName = "history")]
        History,
    }

    /// <summary>
    /// Dump status
    /// </summary>
    public enum DumpStatus
    {
        // TODO: Verify new naming
        [HumanReadable(LongName = "Unknown", ShortName = "grey")]
        UnknownGrey = 1,

        // TODO: Verify new naming
        [HumanReadable(LongName = "Bad Dump", ShortName = "red")]
        BadDumpRed = 2,

        [HumanReadable(LongName = "Questionable", ShortName = "yellow")]
        Questionable,

        [HumanReadable(LongName = "Unverified", ShortName = "blue")]
        Unverified,

        [HumanReadable(LongName = "Verified", ShortName = "green")]
        Verified,
    }

    /// <summary>
    /// List of all media types
    /// </summary>
    public enum MediaType
    {
        NONE = 0,

        [HumanReadable(LongName = "BD-100", ShortName = "bd100")]
        BD100,

        [HumanReadable(LongName = "BD-25", ShortName = "bd25")]
        BD25,

        [HumanReadable(LongName = "BD-50", ShortName = "bd50")]
        BD50,

        [HumanReadable(LongName = "BD-66", ShortName = "bd66")]
        BD66,

        [HumanReadable(LongName = "CD", ShortName = "cd")]
        CD,

        [HumanReadable(LongName = "DVD-5", ShortName = "dvd5")]
        DVD5,

        [HumanReadable(LongName = "DVD-9", ShortName = "dvd9")]
        DVD9,

        [HumanReadable(LongName = "GD-ROM", ShortName = "gdrom")]
        GDROM,

        [HumanReadable(LongName = "HD DVD (DL)", ShortName = "hdvd30")]
        HDDVDDL,

        [HumanReadable(LongName = "HD DVD (SL)", ShortName = "hdvd15")]
        HDDVDSL,

        // TODO: Figure out how to mark this as debug-only
        [HumanReadable(LongName = "Max Test (4-layer)", ShortName = "test4l")]
        MaxTest4Layer,

        [HumanReadable(LongName = "Nintendo GameCube Game Disc", ShortName = "dvd5gc")]
        NintendoGameCubeGameDisc,

        [HumanReadable(LongName = "UMD (DL)", ShortName = "umd2")]
        UMDDL,

        [HumanReadable(LongName = "UMD (SL)", ShortName = "umd1")]
        UMDSL,

        [HumanReadable(LongName = "Wii Optical Disc (DL)", ShortName = "dvd9wii")]
        WiiOpticalDiscDL,

        [HumanReadable(LongName = "Wii Optical Disc (SL)", ShortName = "dvd5wii")]
        WiiOpticalDiscSL,

        [HumanReadable(LongName = "Wii U Optical Disc (SL)", ShortName = "bd25wiiu")]
        WiiUOpticalDiscSL,
    }
}
